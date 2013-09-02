using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSourceExtender;

namespace WebAppExample
{
    public partial class DefaultPage
        : Page
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryExecutor Executor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnEditingRow(object sender, EventArgs e)
        {
            var row = GetViewRow(sender);
            var grid = GetGridView(row);

            if (grid != null)
            {
                grid.EditIndex = row.RowIndex;
                grid.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCancellingRow(object sender, EventArgs e)
        {
            var row = GetViewRow(sender);
            var grid = GetGridView(row);

            if (grid != null) grid.DeleteRow(row.RowIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCancelEditingRow(object sender, EventArgs e)
        {
            var row = GetViewRow(sender);
            var grid = GetGridView(row);

            if (grid != null) grid.EditIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnUpdatingRow(object sender, EventArgs e)
        {
            var row = GetViewRow(sender);
            var grid = GetGridView(row);

            if (grid != null)
            {
                int index = grid.EditIndex;
                grid.UpdateRow(index, false);
                grid.EditIndex = -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private static GridViewRow GetViewRow(object sender)
        {
            Control btn = (Control)sender;
            return (GridViewRow)btn.BindingContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static GridView GetGridView(GridViewRow row)
        {
            return row.BindingContainer as GridView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomDataSource_Selecting(object sender, SelectingEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomDataSource_Updating(object sender, UpdatingEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickFilter(object sender, EventArgs e)
        {
            this.GridConsultans.DataBind();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnInitDataSource(object sender, EventArgs e)
        {
            BusinessDataSource ds = sender as BusinessDataSource;

            if (ds != null)
            {
                ds.SelectFunction = Executor.SelectFunction;
                ds.UpdateFunction = Executor.UpdateFunction;
                ds.DeleteFunction = Executor.DeleteFunction;
                ds.SaveFunction = Executor.SaveFunction;
                ds.IdentifierFunction = dictionary => dictionary.Values.Cast<object>().FirstOrDefault();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomDataSource_ExecutionQueryError(object sender, QueryResultEventArgs e)
        {
            if (e.QueryResult.QueryException != null)
                txtTest.Text = e.QueryResult.QueryException.Message;
        }
    }

}