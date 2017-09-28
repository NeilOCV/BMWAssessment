using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BMWAssessmentWS
{
    /// <summary>
    /// Summary description for Sync
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Sync : System.Web.Services.WebService
    {

        [WebMethod]
        public string SetUpFolderSync(string strSourceFolder, string strDestinationFolder)
        {
            FolderSync folderSync = new FolderSync();
            folderSync.DestinationFolder = strDestinationFolder;
            folderSync.SourceFolder = strSourceFolder;
            return folderSync.SyncFolders();
        }

        //[WebMethod]
        //public List<string> GetAllDrivesOnTheServer()
        //{
        //    Overheads overheads = new Overheads();
        //    return overheads.GetAllDriveLetters();
        //}
        //[WebMethod]
        //public List<string> GetAllFoldersForDrive(string drive)
        //{
        //    Overheads overheads = new Overheads();
        //    return overheads.GetAllFoldersOnDrive(drive);
        //}

    }
}
