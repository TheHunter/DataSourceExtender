using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPagingResult
        : IQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        int MaximumRows { get; }

        /// <summary>
        /// 
        /// </summary>
        int StartRowIndex { get; }

        /// <summary>
        /// 
        /// </summary>
        int TotalRowCount { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable Result { get; }

    }
}
