using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPersisterResult
        : IQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        int RowsAffected { get; }
    }
}
