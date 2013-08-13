using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DataSourceExtender.Exceptions;

namespace DataSourceExtender
{
    /// <summary>
    /// Binds the value of a property
    /// </summary>
    public class InnerControlParameter
        : ControlParameter
    {
        private string innerControlID;
        private string innerPropertyName;

        /// <summary>
        /// 
        /// </summary>
        public InnerControlParameter()
        {
            this.ExcludeNullValue = true;
        }

        [Description("The ID of inner control to find.")]
        [Category("Control")]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.All)]
        public string InnerControlID
        {
            get { return this.innerControlID; }
            set { this.innerControlID = value == null ? null : value.Trim(); }
        }

        [Description("The property name of inner control to use for binding the current value.")]
        [Category("Control")]
        [DefaultValue("")]
        public string InnerPropertyName
        {
            get { return this.innerPropertyName; }
            set { this.innerPropertyName = value == null ? null : value.Trim(); }
        }

        [Description("Indicates if the current parameter will be excluded when the inner control property value is equals to default value.")]
        [Category("Control")]
        [DefaultValue(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public bool ExcludeNullValue { get; set; }

        protected override object Evaluate(HttpContext context, Control control)
        {
            object res = base.Evaluate(context, control);
            Control current = res as Control;
            if (current != null)
            {
                if (string.IsNullOrEmpty(this.InnerControlID))
                    throw new SettingsControlException("The ID of inner control cannot be null or empty.");
                
                Control innerControl = current.FindControl(this.InnerControlID);

                //if (innerControl == null)
                //    throw new ControlNotFoundException("The given control id wasn't found.", this.InnerControlID);

                if (innerControl != null)
                {
                    if (string.IsNullOrEmpty(this.InnerPropertyName))
                        throw new SettingsControlException("The inner property name of inner control parameter cannot be null or empty.");

                    PropertyInfo info = innerControl.GetType().GetProperty(this.InnerPropertyName);
                    return info.GetValue(innerControl, null);
                }
            }
            return null;
        }
    }
}
