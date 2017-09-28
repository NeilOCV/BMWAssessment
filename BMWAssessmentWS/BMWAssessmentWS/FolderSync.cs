using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BMWAssessmentWS
{
    public class FolderSync
    {
        public string SourceFolder { get; set; }
        public string DestinationFolder { get; set; }
        public string ID { get; set; }
        /// <summary>
        /// Get ALL the children folders
        /// Stick 'em in a List and return said List to whomever wants it
        /// </summary>
        /// <param name="pathToInspect"></param>
        /// <returns></returns>
        private List<string> GetSubdirectories(string pathToInspect)
        {
            return Directory.EnumerateDirectories(pathToInspect,"*.*",SearchOption.AllDirectories).ToList();
        }
        /// <summary>
        /// This method does the actual heavy lifting
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private string CopyFile(string source, string destination)
        {
            int array_length = (int)Math.Pow(2, 19);
            byte[] dataArray = new byte[array_length];
            using (FileStream fsread = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.None, array_length))
            {
                using (BinaryReader bwread = new BinaryReader(fsread))
                {
                    using (FileStream fswrite = new FileStream
                    (destination, FileMode.Create, FileAccess.Write, FileShare.None, array_length))
                    {
                        using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                        {
                            for (; ; )
                            {
                                int read = bwread.Read(dataArray, 0, array_length);
                                if (0 == read)
                                    break;
                                bwwrite.Write(dataArray, 0, read);
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Compare what is and what is to be copied and what is to be deleted.
        /// </summary>
        /// <param name="lstSource"></param>
        /// <param name="lstDestination"></param>
        /// <returns></returns>
        private string SyncFiles(List<FileStructure> lstSource, List<FileStructure> lstDestination)
        {
            string result = string.Empty;

            List<FileStructure> filesToCopy = new List<FileStructure>();
            //Get a list of the new files first
            foreach(FileStructure file in lstSource)
            {
                string pathWeAreInterestedIn = DestinationFolder + file.Path.Substring(SourceFolder.Length, file.Path.Length - SourceFolder.Length);
                var prog = from tb in lstDestination
                           where (tb.Path == pathWeAreInterestedIn)
                           select tb;
                if(prog.Count()==0)
                {
                    CopyFile(file.Path, pathWeAreInterestedIn);
                }
                else
                {
                    FileInfo destinationInfo = new FileInfo(pathWeAreInterestedIn);
                    FileInfo sourceInfo = new FileInfo(file.Path);
                    if((destinationInfo.Length!=sourceInfo.Length)||(sourceInfo.LastWriteTime != file.DateModified))
                    {
                        File.Delete(pathWeAreInterestedIn);
                        CopyFile(file.Path, pathWeAreInterestedIn);
                    }
                }
            }
                     

            return result;
        }
        /// <summary>
        /// Get a list of files and folders in the source and destination directory directory
        /// If the destination directory does not exist, create it.
        /// 
        /// 
        /// </summary>
        public string SyncFolders()
        {
            List<string> allSourceDirectories = new List<string>();
            List<string> allSourceFiles = new List<string>();

            List<string> allDestinationFiles = new List<string>();
            List<string> allDestinationDirectories = new List<string>();

            List<FileStructure> lstDetailsOfFilesOnSource = new List<FileStructure>();
            List<FileStructure> lstDetailsOfFilesOnDestination = new List<FileStructure>();

            try
            {
                try
                {
                    allSourceFiles = Directory.GetFiles(SourceFolder, "*.*", SearchOption.AllDirectories).ToList();
                    foreach(string file in allSourceFiles)
                    {
                        FileStructure sourceFile = new FileStructure();
                        sourceFile.Path = file;
                        FileInfo fileInfo = new FileInfo(file);
                        sourceFile.DateModified = fileInfo.LastWriteTime;
                        sourceFile.Size = fileInfo.Length;
                        lstDetailsOfFilesOnSource.Add(sourceFile);

                    }
                    allSourceDirectories = GetSubdirectories(SourceFolder);
                }
                catch (Exception)
                {
                    return "<p>Source directory not found</p>";
                }
                try
                {
                    allDestinationFiles = Directory.GetFiles(DestinationFolder, "*.*", SearchOption.AllDirectories).ToList();
                    foreach (string file in allDestinationFiles)
                    {
                        FileStructure destinationFile = new FileStructure();
                        destinationFile.Path = file;
                        FileInfo fileInfo = new FileInfo(file);
                        destinationFile.DateModified = fileInfo.LastWriteTime;
                        destinationFile.Size = fileInfo.Length;
                        lstDetailsOfFilesOnDestination.Add(destinationFile);

                    }
                    allDestinationDirectories = GetSubdirectories(DestinationFolder);
                }
                catch (Exception)
                {
                    //Destiation directory not found.  Attempting to create one
                    try
                    {
                        Directory.CreateDirectory(DestinationFolder);
                    }
                    catch (Exception ex)
                    {
                        return "<p><destination>" + ex.Message + "</destination></p>";
                    }
                }

                #region Sync folders
                #region Make sure destination folders at least match source folders
                //Raait!  Now we've got a list of files (full detail) in the source folder, a list of subfolders from the source folder, 
                //a list of files (full detail) from the destination folder and a list of subfloders from the destination folder.
                //Mix it up.

                //First, make the directories the same.
                //Check what is in the source but not in the destination
                
                
                //NB!!!
                //THIS STEP IS NOT STRICTLY NECCESARY AS C# IS CLEVER ENOUGH TO NOT BREAK IF YOU TRY TO CREATE A FOLDER THAT ALREADY EXISTS.
                //I JUST ADD THIS STEP TO BE POLITE.
                //NB!!!
                
                List<string> lstDirectoriesToCreateOnDestination = new List<string>();
                foreach (string sourceFolder in allSourceDirectories)
                {
                    string pathWeAreInterestedIn = DestinationFolder + sourceFolder.Substring(SourceFolder.Length, sourceFolder.Length - SourceFolder.Length);
                    if (!allDestinationDirectories.Contains(pathWeAreInterestedIn))
                        lstDirectoriesToCreateOnDestination.Add(pathWeAreInterestedIn);
                }

                //Ok  Now we know what new folders there are on the source.  Create these folders on the destination.  All the time, keeping a watchful eye out for IO errors.
                foreach (string directoryToBeCreatedOnDestination in lstDirectoriesToCreateOnDestination)
                {
                    try
                    {
                        Directory.CreateDirectory(directoryToBeCreatedOnDestination);
                    }
                    catch (Exception ex)
                    {
                        return "<p><CreatingDestinationSubfolders>" + ex.Message + "</CreatingDestinationSubfolders></p>";
                    }
                }
                #endregion
                #region Remove unnecessary folders
                //Ok.  Now the destination has all the folders the source has.  It might have more.
                //According to the spec, if there are folders in the destination that is not in the source, it should be killed dead untill it dies.


                List<string> lstDirectoriesToDeleteFromDestination = new List<string>();
                
                foreach (string directoryToCheck in allDestinationDirectories)
                {
                    string pathWeAreInterestedIn = SourceFolder + directoryToCheck.Substring(DestinationFolder.Length, directoryToCheck.Length - DestinationFolder.Length);
                    try
                    {
                        if (!allSourceDirectories.Contains(pathWeAreInterestedIn))
                            Directory.Delete(directoryToCheck, true);
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                    
                }

                SyncFiles(lstDetailsOfFilesOnSource, lstDetailsOfFilesOnDestination);
                
                #endregion
                #endregion
                this.ID = Guid.NewGuid().ToString();
                return this.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}