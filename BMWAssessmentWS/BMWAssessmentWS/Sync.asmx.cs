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
        static List<FolderSync> AllSyncs = new List<FolderSync>();

        [WebMethod]
        public string SetUpFolderSync(string strSourceFolder, string strDestinationFolder)
        {
            FolderSync folderSync = new FolderSync();
            folderSync.DestinationFolder = strDestinationFolder;
            folderSync.SourceFolder = strSourceFolder;
            folderSync.ID = folderSync.SyncFolders();
            AllSyncs.Add(folderSync);
            return folderSync.ID;
        }
        [WebMethod]
        public Progress GetProgress(string id)
        {
            FolderSync sync = new FolderSync();
            var prog = from tb in AllSyncs
                       where tb.ID == id
                       select tb;
            if (prog.Count() > 0)
                return prog.ToList()[0].CopyProgress;
            return null;
        }
        [WebMethod]
        public List<FolderSync> GetAllThreads()
        {
            return AllSyncs;
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
