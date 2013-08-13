using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueryResult
    {
        Type SourceType { get; }

        /// <summary>
        /// 
        /// </summary>
        Exception QueryException { get; }
    }
}
