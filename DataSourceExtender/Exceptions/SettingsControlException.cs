using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DataSourceExtender.Exceptions
{
    /// <summary>
    /// Rappresents error when web Controls haven't been initialized correctly.
    /// </summary>
    public class SettingsControlException
        : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SettingsControlException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SettingsControlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
