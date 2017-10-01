using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMWAssessment
{
    public partial class _Default : Page
    {
        private void PopulateGrid()
        {
            BMWAssessmentFolderSyncWS.Sync sync = new BMWAssessmentFolderSyncWS.Sync();
            List<BMWAssessmentFolderSyncWS.FolderSync> folders = new List<BMWAssessmentFolderSyncWS.FolderSync>();
            folders = sync.GetAllThreads().ToList();
            grdActiveThreads.DataSource = folders;
            grdActiveThreads.DataBind();

        }
        /// <summary>
        /// Just because it is irritating to build under the controls itself.  Outsource this work to another method.
        /// </summary>
        private void PageLoad()
        {
            PopulateGrid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PageLoad();
            }
        }

        protected void grdActiveThreads_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                Label lblID = (Label)e.Row.FindControl("lblID");
                UserControls.BarGraph progressBar = (UserControls.BarGraph)e.Row.FindControl("ucBarGraph");
                using(BMWAssessmentFolderSyncWS.Sync sync=new BMWAssessmentFolderSyncWS.Sync())
                {
                    BMWAssessmentFolderSyncWS.Progress progress = sync.GetProgress(lblID.Text);
                    int progressPerc = 100;
                    if (progress.TotalNumberOfFilesToCopy > 0)
                    {
                        progressPerc = (int)((progress.FilesCopiedSoFar * 100) / progress.TotalNumberOfFilesToCopy);
                    }
                    progressBar.Progress = progressPerc;
                }
                
            }
        }

        protected void tmrTimer_Tick(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}