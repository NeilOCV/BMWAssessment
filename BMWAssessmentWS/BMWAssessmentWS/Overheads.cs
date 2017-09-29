﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BMWAssessmentWS
{
    public class Overheads
    {
        public List<string> GetAllDriveLetters()
        {
            List<string> result = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                string drive = d.Name;
                result.Add(drive);
            }
            return result;
        }
        public List<string> GetAllFoldersOnDrive(string drive)
        {
            List<string> directories = new List<string>();
            try
            {
                directories = new List<string>(Directory.EnumerateDirectories(drive, "*.*", SearchOption.AllDirectories));
                //string[] dirs= Directory.EnumerateDirectories(drive, "*.*", SearchOption.AllDirectories);
            }
            catch (UnauthorizedAccessException UAEx)
            {
                string s = "!";
            }
            catch
            {
            }
            return directories;
        }
    }
}