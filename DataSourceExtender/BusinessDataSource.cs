using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataSourceExtender
{
    [PersistChildren(false)]
    [ParseChildren(true)]
    public class BusinessDataSource
        : DataSourceControl
    {
        //private string hqlWhere;
        //private string hqlSort;
        private string typeName;
        private Type sourceType;
        //private readonly ParameterCollection identifiers;
        private readonly ParameterCollection selectParameters;
        private readonly BusinessDataSourceView dataView;
        static private readonly string[] ViewNames = { "DefaultView" };

        public BusinessDataSource()
        {
            this.dataView = new BusinessDataSourceView(this, ViewNames[0]);
            //this.identifiers = new ParameterCollection();
            this.selectParameters = new ParameterCollection();
        }

        [Category("Data")]
        [Browsable(false)]
        [DefaultValue(null)]
        public Func<SelectingEventArgs, IPagingResult> SelectFunction { get; set; }

        [Category("Data")]
        [Browsable(false)]
        public Func<UpdatingEventArgs, IPersisterResult> UpdateFunction { get; set; }

        [Category("Data")]
        [Browsable(false)]
        public Func<SavingEventArgs, IPersisterResult> SaveFunction { get; set; }

        [Category("Data")]
        [Browsable(false)]
        public Func<DeletingEventArgs, IPersisterResult> DeleteFunction { get; set; }

        [Category("Data")]
        [Browsable(false)]
        public Func<IDictionary, object> IdentifierFunction { get; set; }

        [Category("Data"),
        DefaultValue((string)null),
        Description("The type name used for source type.")]
        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

        [Category("Data")]
        [Browsable(false)]
        public Type SourceType
        {
            get
            {
                if (sourceType == null)
                    this.sourceType = BuildManager.GetType(this.TypeName, true, false);

                return sourceType;
            }
        }

        //[Category("Data")]
        //[DefaultValue(null)]
        //[MergableProperty(false)]
        //[Browsable(false)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //[Description("The profile parameters which are associated to the primary key source.")]
        ////[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        //public ParameterCollection Identifiers
        //{
        //    get { return this.identifiers; }
        //}

        [Category("Data")]
        [DefaultValue(null)]
        [MergableProperty(false)]
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The select parameters for the calling business data source.")]
        //[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public ParameterCollection SelectParameters
        {
            get { return this.dataView.SelectParameters; }
        }

        /// <summary>
        /// Occurs when the execution of any query throws an exception
        /// </summary>
        public event EventHandler<QueryResultEventArgs> ExecutionQueryError;
        internal void OnExecutionQueryError(QueryResultEventArgs e)
        {
            if (ExecutionQueryError != null) ExecutionQueryError(this, e);
        }

        /// <summary>
        /// Occurs when a data retrieval operation has completed.
        /// </summary>
        public event EventHandler Selected;
        internal void OnSelected(EventArgs e)
        {
            if (Selected != null) Selected(this, e);
        }

        /// <summary>
        /// Occurs before a data retrieval operation.
        /// </summary>
        public event EventHandler<SelectingEventArgs> Selecting;
        internal void OnSelecting(SelectingEventArgs e)
        {
            if (Selecting != null) Selecting(this, e);
        }

        /// <summary>Occurs when an <see>
        /// <cref>Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update</cref>
        /// </see> operation has completed.</summary>
        public event EventHandler Updated;
        internal void OnUpdated(EventArgs e)
        {
            if (Updated != null) Updated(this, e);
        }

        /// <summary>
        /// Occurs before an <see><cref>Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update</cref></see>
        /// operation.
        /// </summary>
        public event EventHandler<UpdatingEventArgs> Updating;
        internal void OnUpdating(UpdatingEventArgs e)
        {
            if (Updating != null) Updating(this, e);
        }

        /// <summary>Occurs when a <see>
        /// <cref>Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete</cref>
        /// </see> operation has completed.
        /// </summary>
        public event EventHandler Deleted;
        internal void OnDeleted(EventArgs e)
        {
            if (Deleted != null) Deleted(this, e);
        }

        /// <summary>Occurs before a <see>
        /// <cref>Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete</cref>
        /// </see> operation.</summary>
        public event EventHandler<DeletingEventArgs> Deleting;
        internal void OnDeleting(DeletingEventArgs e)
        {
            if (Deleting != null) Deleting(this, e);
        }

        /// <summary>Occurs when an <see>
        /// <cref>Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert</cref>
        /// </see> operation has completed.</summary>
        public event EventHandler<EventArgs> Inserted;
        internal void OnInserted(EventArgs e)
        {
            if (Inserted != null) Inserted(this, e);
        }

        /// <summary>Occurs before an <see>
        /// <cref>Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert</cref>
        /// </see> operation.</summary>
        public event EventHandler<SavingEventArgs> Inserting;
        internal void OnInserting(SavingEventArgs e)
        {
            if (Inserting != null) Inserting(this, e);
        }

        #region DataSourceView members
        protected override DataSourceView GetView(string viewName)
        {
            return this.dataView;
        }

        protected override ICollection GetViewNames()
        {
            return ViewNames;
        }
        #endregion

    }
}
