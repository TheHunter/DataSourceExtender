using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryResultEventArgs
        : EventArgs
    {
        private readonly IQueryResult queryResult;

        /// <summary>
        /// 
        /// </summary>
        protected QueryResultEventArgs()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryResult"></param>
        public QueryResultEventArgs(IQueryResult queryResult)
        {
            if (queryResult == null)
                throw new ArgumentNullException("queryResult", "The queryResult object cannot be null.");

            this.queryResult = queryResult;
        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryResult QueryResult { get { return this.queryResult; } }
    }
}
