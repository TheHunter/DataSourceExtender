using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DelegateNotFoundException
        : Exception
    {
        private readonly string delegateTarget;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateTarget"></param>
        /// <param name="message"></param>
        public DelegateNotFoundException(string delegateTarget, string message)
            : base(message)
        {
            this.delegateTarget = delegateTarget;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateTarget"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DelegateNotFoundException(string delegateTarget, string message, Exception innerException)
            : base(message, innerException)
        {
            this.delegateTarget = delegateTarget;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DelegateTarget { get { return this.delegateTarget; } }
    }
}
