using System;

namespace DataSourceExtender.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class PersisterResult
        : IPersisterResult
    {
        private readonly int rowsAffected;
        private readonly Exception queryException;
        private readonly Type sourceType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="rowsAffected"></param>
        public PersisterResult(Type sourceType, int rowsAffected)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType", "The source type cannot be null.");

            this.sourceType = sourceType;
            this.rowsAffected = rowsAffected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="queryException"></param>
        public PersisterResult(Type sourceType, Exception queryException)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType", "The source type cannot be null.");

            this.sourceType = sourceType;
            this.queryException = queryException;
            this.rowsAffected = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public int RowsAffected
        {
            get { return this.rowsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Exception QueryException
        {
            get { return this.queryException; }
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
