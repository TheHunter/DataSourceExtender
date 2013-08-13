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
    public class SavingEventArgs
        : SourceEventArgs
    {
        private readonly IDictionary values;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="values"></param>
        public SavingEventArgs(Type sourceType, IDictionary values)
            : base(sourceType)
        {
            this.values = values;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary Values
        {
            get { return this.values; }
        }

    }
}
