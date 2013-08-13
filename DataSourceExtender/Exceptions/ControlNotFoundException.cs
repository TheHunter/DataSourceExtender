using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSourceExtender.Exceptions
{
    public class ControlNotFoundException
        : Exception
    {
        private readonly string controlID;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="controlID"></param>
        public ControlNotFoundException(string message, string controlID)
            : base(message)
        {
            this.controlID = controlID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="controlID"></param>
        /// <param name="innerException"></param>
        public ControlNotFoundException(string message, string controlID, Exception innerException)
            : base(message, innerException)
        {
            this.controlID = controlID;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ControlID { get { return this.controlID; } }
    }
}
