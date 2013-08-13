using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatingEventArgs
        : SourceEventArgs
    {
        private readonly object key;
        private readonly IDictionary values;
        private readonly IDictionary oldValues;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="oldValues"></param>
        public UpdatingEventArgs(Type sourceType, object key, IDictionary values, IDictionary oldValues)
            :base(sourceType)
        {
            this.key = key;
            this.values = values;
            this.oldValues = oldValues;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Key
        {
            get { return this.key; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary Values
        {
            get { return this.values; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary OldValues
        {
            get { return this.oldValues; }
        }
    }
}
