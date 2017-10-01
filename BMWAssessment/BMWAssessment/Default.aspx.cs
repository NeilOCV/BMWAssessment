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
        int numberOfFolders = 0;
        private void PopulateTrees()
        {
            BMWAssessmentFolderSyncWS.Sync ws = new BMWAssessmentFolderSyncWS.Sync();
            List<string> drives = ws.GetAllDrivesOnTheServer().ToList();
            foreach(string drive in drives)
            {
                TreeNode node = new TreeNode();
                node.Text = drive;
                node.Value = drive;
                tvSource.Nodes.Add(node);
            }
        }
        private void PopulateGrid()
        {
            BMWAssessmentFolderSyncWS.Sync sync = new BMWAssessmentFolderSyncWS.Sync();
            List<BMWAssessmentFolderSyncWS.FolderSync> folders = new List<BMWAssessmentFolderSyncWS.FolderSync>();
            folders = sync.GetAllThreads().ToList();
            //Only update the grid if absolutely necessary.
            if (numberOfFolders != folders.Count)
            {
                numberOfFolders = folders.Count;
                grdActiveThreads.DataSource = folders;
                grdActiveThreads.DataBind();
            }

        }
        /// <summary>
        /// Just because it is irritating to build under the controls itself.  Outsource this work to another method.
        /// </summary>
        private void PageLoad()
        {
            PopulateGrid();
            PopulateTrees();
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
            //PopulateGrid();
        }
        private string FolderNameOnly(string path)
        {
            string result = string.Empty;
            string[] parts = path.Split('\\');
            result = parts[parts.Length-1];
            return result;

        }
        protected void tvSource_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strPathClicked = tvSource.SelectedNode.Value;
            TreeNode selectedNode = tvSource.SelectedNode;
            BMWAssessmentFolderSyncWS.Sync ws = new BMWAssessmentFolderSyncWS.Sync();
            List<string> subfolders = ws.GetAllChildrenDirectories(strPathClicked).ToList();
            foreach (string subfolder in subfolders)
            {
                TreeNode node = new TreeNode();
                node.Value = subfolder;
                node.Text = FolderNameOnly(subfolder);
                selectedNode.ChildNodes.Add(node);
            }
        }
    }
}