using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public class SourceEventArgs
        : CancelEventArgs
    {
        private readonly Type sourceType;

        /// <summary>
        /// 
        /// </summary>
        protected SourceEventArgs() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        public SourceEventArgs(Type sourceType)
        {
            this.sourceType = sourceType;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type SourceType
        {
            get { return this.sourceType; }
        }

    }
}
