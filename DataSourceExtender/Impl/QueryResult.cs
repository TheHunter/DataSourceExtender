using System;

namespace DataSourceExtender.Impl
{
    public class QueryResult
        : IQueryResult
    {
        private readonly Exception queryException;
        private readonly Type sourceType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        public QueryResult(Type sourceType)
        {
            this.sourceType = sourceType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="queryException"></param>
        public QueryResult(Type sourceType, Exception queryException)
        {
            this.sourceType = sourceType;
            this.queryException = queryException;
        }

        /// <summary>
        /// 
        /// </summary>
        public Exception QueryException
        {
            get { return queryException; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type SourceType
        {
            get { return sourceType; }
        }
    }
}
