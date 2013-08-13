using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSourceExtender.Exceptions;

namespace DataSourceExtender
{
    public class BusinessDataSourceView
        : DataSourceView
    {
        private readonly BusinessDataSource owner;
        private readonly ParameterCollection selectParameters;


        public BusinessDataSourceView(BusinessDataSource owner, string name)
            : base(owner, name)
        {
            this.owner = owner;
            this.selectParameters = new ParameterCollection();
        }

        public override bool CanSort { get { return true; } }

        public override bool CanInsert { get { return true; } }

        public override bool CanUpdate { get { return true; } }

        public override bool CanDelete { get { return true; } }

        public override bool CanPage { get { return true; } }

        public override bool CanRetrieveTotalRowCount { get { return true; } }

        public ParameterCollection SelectParameters { get { return this.selectParameters; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
            if (owner.SelectFunction == null)
                throw new DelegateNotFoundException("SelectFunction", "The select function doesn't implemented.");

            arguments.RaiseUnsupportedCapabilitiesError(this);

            var eventArg = new SelectingEventArgs(arguments, this.GetSelectParameters(), owner.SourceType);
            owner.OnSelecting(eventArg);
            if (eventArg.Cancel)
                return null;

            var selectActionEvent = new ActionEventArgs<SelectingEventArgs, IPagingResult>(eventArg, owner.SelectFunction);

            IPagingResult paging = selectActionEvent.Execute();
            if (paging.QueryException != null)
            {
                owner.OnExecutionQueryError(new QueryResultEventArgs(paging));
                return null;
            }

            IEnumerable result = paging.Result;
            arguments.TotalRowCount = paging.TotalRowCount;
            owner.OnSelected(EventArgs.Empty);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="oldValues"></param>
        /// <returns></returns>
        protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            if (owner.UpdateFunction == null)
                throw new DelegateNotFoundException("UpdateFunction", "The update function doesn't implemented.");

            if (owner.IdentifierFunction == null)
                throw new DelegateNotFoundException("IdentifierFunction", "The identifier function doesn't implemented.");

            var eventArg = new UpdatingEventArgs(owner.SourceType, owner.IdentifierFunction.Invoke(keys), values, oldValues);
            owner.OnUpdating(eventArg);
            
            if (eventArg.Cancel)
                return 0;

            var updateActionEvent = new ActionEventArgs<UpdatingEventArgs, IPersisterResult>(eventArg, owner.UpdateFunction);

            var result = updateActionEvent.Execute();
            if (result.QueryException != null)
            {
                owner.OnExecutionQueryError(new QueryResultEventArgs(result));
                return -1;
            }

            owner.OnUpdated(EventArgs.Empty);
            OnDataSourceViewChanged(EventArgs.Empty);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="oldValues"></param>
        /// <returns></returns>
        protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
        {
            if (owner.DeleteFunction == null)
                throw new DelegateNotFoundException("DeleteFunction", "The delete function doesn't implemented.");

            if (owner.IdentifierFunction == null)
                throw new DelegateNotFoundException("IdentifierFunction", "The identifier function doesn't implemented.");

            var eventArg = new DeletingEventArgs(owner.SourceType, owner.IdentifierFunction.Invoke(keys));
            owner.OnDeleting(eventArg);

            if (eventArg.Cancel)
                return 0;

            var deletingActionEvent = new ActionEventArgs<DeletingEventArgs, IPersisterResult>(eventArg, owner.DeleteFunction);

            var result = deletingActionEvent.Execute();
            if (result.QueryException != null)
            {
                owner.OnExecutionQueryError(new QueryResultEventArgs(result));
                return -1;
            }

            owner.OnDeleted(EventArgs.Empty);
            OnDataSourceViewChanged(EventArgs.Empty);
            return 1;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected override int ExecuteInsert(IDictionary values)
        {
            if (owner.SaveFunction == null)
                throw new DelegateNotFoundException("SaveFunction", "The insert function doesn't implemented.");

            var eventArg = new SavingEventArgs(owner.SourceType, values);
            owner.OnInserting(eventArg);

            if (eventArg.Cancel)
                return 0;

            var insertingActionEvent = new ActionEventArgs<SavingEventArgs, IPersisterResult>(eventArg, owner.SaveFunction);

            try
            {
                var result = insertingActionEvent.Execute();
                if (result.QueryException != null)
                {
                    owner.OnExecutionQueryError(new QueryResultEventArgs(result));
                    return -1;
                }

                owner.OnInserted(EventArgs.Empty);
                OnDataSourceViewChanged(EventArgs.Empty);
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary GetSelectParameters()
        {
            HttpContext current = HttpContext.Current;
            var values = SelectParameters.GetValues(current, this.owner);

            foreach (var value in values.Keys)
            {
                current.Session[value.ToString()] = values[value];
            }

            var collection = selectParameters.OfType<InnerControlParameter>().Where( n => n.ExcludeNullValue );
            if (collection.Any())
            {
                collection.All
                    (
                        parameter =>
                            {
                                var value = values[parameter.Name];
                                if (value == null)
                                    values.Remove(parameter.Name);
                                
                                return true;
                            }
                    );
            }

            return values;
        }
    }
}
