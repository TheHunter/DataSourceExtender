using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletingEventArgs
        : SourceEventArgs
    {
        private readonly object key;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="key"></param>
        public DeletingEventArgs(Type sourceType, object key)
            : base(sourceType)
        {
            this.key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Key
        {
            get { return this.key; }
        }
    }
}
