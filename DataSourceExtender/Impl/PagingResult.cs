using System;
using System.Collections;

namespace DataSourceExtender.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class PagingResult
        : QueryResult, IPagingResult
    {
        private readonly int maximumRows;
        private readonly int startRowIndex;
        private int totalRowCount;
        private IEnumerable result;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="queryException"></param>
        public PagingResult(Type sourceType , Exception queryException)
            : base(sourceType, queryException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        public PagingResult(Type sourceType, int startRowIndex, int maximumRows)
            : base(sourceType)
        {
            this.startRowIndex = startRowIndex;
            this.maximumRows = maximumRows;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumRows
        {
            get { return maximumRows; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartRowIndex
        {
            get { return startRowIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalRowCount
        {
            get { return totalRowCount; }
            set { this.totalRowCount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable Result
        {
            get { return result; }
            set { this.result = value; }
        }

    }
}
