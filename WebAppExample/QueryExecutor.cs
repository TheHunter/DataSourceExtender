using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataSourceExtender;
using DataSourceExtender.Impl;
using NHibernate;
using NHibernate.Criterion;
using PersistentLayer;
using PersistentLayer.Exceptions;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;

namespace WebAppExample
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryExecutor
        : IQueryExecutor
    {

        private readonly INhPagedDAO customDAO;

        /// <summary>
        /// 
        /// </summary>
        public INhPagedDAO CustomDAO
        {
            get { return this.customDAO; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customDAO"></param>
        public QueryExecutor(INhPagedDAO customDAO)
        {
            this.customDAO = customDAO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IPagingResult SelectFunction(SelectingEventArgs parameter)
        {
            lock (customDAO)
            {
                try
                {
                    DetachedCriteria criteria = DetachedCriteria.For(parameter.SourceType);
                    parameter.Parameters.All
                        (
                            sourceParameter =>
                                {
                                    criteria.Add(Restrictions.Eq(sourceParameter.Name, sourceParameter.Value));
                                    return true;
                                }
                        );

                    var res = customDAO.GetPagedResult(parameter.StartRowIndex, parameter.MaximumRows, criteria);
                    return new PagingResult(parameter.SourceType, res.StartIndex, res.Size) { Result = res.Result, TotalRowCount = res.Counter };
                }
                catch (Exception ex)
                {
                    return new PagingResult(parameter.SourceType, new ExecutionQueryException("Error on executing the select function.", "SelectFunction", ex));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IPersisterResult UpdateFunction(UpdatingEventArgs parameter)
        {
            lock (customDAO)
            {
                object objectToUpdate = customDAO.FindBy(parameter.SourceType, parameter.Key, LockMode.Upgrade);
                Dictionary<string, object> dic = parameter.Values.Keys.Cast<object>().ToDictionary(current => current.ToString(), current => parameter.Values[current]);

                ITransactionProvider tr = customDAO.GetTransactionProvider();

                try
                {
                    tr.BeginTransaction("UpdateFunction", IsolationLevel.ReadCommitted);

                    var metadata = customDAO.GetPersistentClassInfo(objectToUpdate.GetType());
                    
                    metadata.SetPropertyValues(objectToUpdate, dic, EntityMode.Poco);

                    tr.CommitTransaction();

                    return new PersisterResult(parameter.SourceType, 1);
                }
                catch (Exception ex)
                {
                    tr.RollbackTransaction(ex);
                    return new PersisterResult(parameter.SourceType, new ExecutionQueryException("Error on executing the update function.", "UpdateFunction", ex));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IPersisterResult DeleteFunction(DeletingEventArgs parameter)
        {
            ITransactionProvider tr = customDAO.GetTransactionProvider();
            lock (customDAO)
            {
                try
                {
                    tr.BeginTransaction("DeleteFunction", IsolationLevel.ReadCommitted);

                    int res = customDAO.MakeHQLQuery(string.Format("delete {0} cls where cls.id= :key", parameter.SourceType.Name))
                                    .SetParameter("key", parameter.Key)
                                    .ExecuteUpdate();

                    tr.CommitTransaction();

                    return new PersisterResult(parameter.SourceType, res);
                    
                }
                catch (Exception ex)
                {
                    tr.RollbackTransaction(ex);
                    return new PersisterResult(parameter.SourceType, new ExecutionQueryException("Error on executing the delete function.", "DeleteFunction", ex));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IPersisterResult SaveFunction(SavingEventArgs parameter)
        {
            ITransactionProvider tr = customDAO.GetTransactionProvider();
            lock (customDAO)
            {
                try
                {
                    tr.BeginTransaction("SaveFunction", IsolationLevel.ReadCommitted);
                    
                    var metadata = customDAO.GetPersistentClassInfo(parameter.SourceType);

                    var obj = Activator.CreateInstance(parameter.SourceType);
                    var dic = parameter.Values.Keys.Cast<object>().ToDictionary(current => current.ToString(), current => parameter.Values[current]);
                    metadata.SetPropertyValues(obj, dic, EntityMode.Poco);
                    customDAO.MakePersistent(obj);

                    tr.CommitTransaction();

                    return new PersisterResult(parameter.SourceType, 1);
                }
                catch (Exception ex)
                {
                    tr.RollbackTransaction(ex);
                    return new PersisterResult(parameter.SourceType, new ExecutionQueryException("Error on executing the saving function.", "SaveFunction", ex));
                }
            }
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IQueryExecutor
    {
        /// <summary>
        /// 
        /// </summary>
        INhPagedDAO CustomDAO { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IPagingResult SelectFunction(SelectingEventArgs parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IPersisterResult UpdateFunction(UpdatingEventArgs parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IPersisterResult DeleteFunction(DeletingEventArgs parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IPersisterResult SaveFunction(SavingEventArgs parameter);

    }
}