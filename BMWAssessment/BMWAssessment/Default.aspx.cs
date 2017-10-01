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
            try
            {
                BMWAssessmentFolderSyncWS.Sync ws = new BMWAssessmentFolderSyncWS.Sync();
                List<string> drives = ws.GetAllDrivesOnTheServer().ToList();
                foreach (string drive in drives)
                {
                    TreeNode sourceNode = new TreeNode();
                    sourceNode.Text = drive;
                    sourceNode.Value = drive;
                    tvSource.Nodes.Add(sourceNode);

                    TreeNode destinationNode = new TreeNode();
                    destinationNode.Text = drive;
                    destinationNode.Value = drive;
                    tvDestination.Nodes.Add(destinationNode);

                }
            }
            catch (Exception)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<script>");
                builder.Append(@"alert('Server not responding.  Please enter a valid URL in the web.config.');");
                builder.Append("</script>");
                Response.Write(builder.ToString());
            }
        }
        private void PopulateGrid()
        {
            try
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
            catch (Exception)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<script>");
                builder.Append(@"alert('Server not responding.  Please enter a valid URL in the web.config.');");
                builder.Append("</script>");
                Response.Write(builder.ToString());
            }

        }
        private bool TalkingToTheCorrectServer()
        {
            bool result = false;

            try
            {
                BMWAssessmentFolderSyncWS.Sync ws = new BMWAssessmentFolderSyncWS.Sync();
                string serverResponse=ws.KooWee("marco");
                if(serverResponse.Trim().ToUpper()=="POLO")
                    result = true;
            }
            catch (Exception)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<script>");
                builder.Append(@"alert('Server not responding.  Please enter a valid URL in the web.config.');");
                builder.Append("</script>");
                Response.Write(builder.ToString());
                result = false;
            }


            pnlPopUp.Visible = result;
            pnlServiceNotAvailable.Visible = !result;
            return result;
        }
        /// <summary>
        /// Just because it is irritating to build under the controls itself.  Outsource this work to another method.
        /// </summary>
        private void PageLoad()
        {
            if (!TalkingToTheCorrectServer())
                return;
            PopulateGrid();
            PopulateTrees();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PageLoad();

        }

        protected void grdActiveThreads_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblID = (Label)e.Row.FindControl("lblID");
                UserControls.BarGraph progressBar = (UserControls.BarGraph)e.Row.FindControl("ucBarGraph");
                using (BMWAssessmentFolderSyncWS.Sync sync = new BMWAssessmentFolderSyncWS.Sync())
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
        private string FolderNameOnly(string path)
        {
            string result = string.Empty;
            string[] parts = path.Split('\\');
            result = parts[parts.Length - 1];
            return result;

        }
        protected void tvSource_SelectedNodeChanged(object sender, EventArgs e)
        {
            tmrTimer.Enabled = false;
            string strPathClicked = tvSource.SelectedNode.Value;
            txtSourcePath.Text = strPathClicked;
            TreeNode selectedNode = tvSource.SelectedNode;
            tvSource.SelectedNode.Expand();
            tvSource.SelectedNode.Select();

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            tmrTimer.Enabled = true;
            Response.Redirect(Request.RawUrl);
        }

        protected void tvDestination_SelectedNodeChanged(object sender, EventArgs e)
        {
            tmrTimer.Enabled = false;
            string strPathClicked = tvDestination.SelectedNode.Value;
            txtDestination.Text = strPathClicked;
            TreeNode selectedNode = tvDestination.SelectedNode;
            tvDestination.SelectedNode.Expand();
            tvDestination.SelectedNode.Select();

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDestination.Text == "" || txtSourcePath.Text == "")
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<script>");
                builder.Append("Bot the source folder and the destination folder must be selected.  Please select something to get the ball rolling.");
                builder.Append("</script>");
                Response.Write(builder.ToString());
                return;
            }
            else
            {
                BMWAssessmentFolderSyncWS.Sync ws = new BMWAssessmentFolderSyncWS.Sync();
                ws.SetUpFolderSync(txtSourcePath.Text, txtDestination.Text);
                tmrTimer.Enabled = true;
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}