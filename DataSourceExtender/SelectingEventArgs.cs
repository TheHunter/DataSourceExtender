using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSourceExtender.Exceptions;
using DataSourceExtender.Impl;

namespace DataSourceExtender
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectingEventArgs
        : SourceEventArgs
    {
        private readonly int startRowIndex;
        private readonly int maximumRows;
        private readonly List<ISourceParameter> parameters = new List<ISourceParameter>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="parameters"></param>
        /// <param name="sourceType"></param>
        public SelectingEventArgs(DataSourceSelectArguments arguments, IDictionary parameters, Type sourceType)
            : base(sourceType)
        {
            if (arguments == null)
                throw new ControlParameterException("The arguments for the SelectingEventArgs instance cannot be null.", "arguments");
            
            this.startRowIndex = arguments.StartRowIndex;
            this.maximumRows = arguments.MaximumRows;

            if (parameters != null && parameters.Count > 0)
            {
                this.parameters.AddRange(parameters.Keys.Cast<string>().Select<string, ISourceParameter>(n => new SourceParameter(n, parameters[n])));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartRowIndex
        {
            get { return this.startRowIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumRows
        {
            get { return this.maximumRows; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddParameter(string name, object value)
        {
            if (this.parameters.Any(n => n.Name.Equals(name)))
                return false;

            this.parameters.Add(new SourceParameter(name, value));
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveParameter(string name)
        {
            if (name == null)
                return false;

            var parameter = this.parameters.FirstOrDefault(n => n.Name.Equals(name));
            if (parameter == null)
                return false;

            return this.parameters.Remove(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ISourceParameter> Parameters
        {
            get { return this.parameters; }
        }
    }
}
