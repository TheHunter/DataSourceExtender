using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlParameterException
        : Exception
    {
        private readonly string parameterName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameterName"></param>
        public ControlParameterException(string message, string parameterName)
            : base(message)
        {
            this.parameterName = parameterName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameterName"></param>
        /// <param name="innerException"></param>
        public ControlParameterException(string message, string parameterName, Exception innerException)
            : base(message, innerException)
        {
            this.parameterName = parameterName;
        }
    }
}
