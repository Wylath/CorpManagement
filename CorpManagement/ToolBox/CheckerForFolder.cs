using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.ToolBox
{
    class CheckerForFolder
    {
        /// <summary>
        /// Check if the folder for the current element selected exist
        /// </summary>
        /// <returns></returns>
        public bool CheckIfFolderExist(string directoryFolder, int idFolder)
        {
            if (string.IsNullOrEmpty(directoryFolder) || idFolder == 0)
                return false;

            string directory = ConfigurationManager.AppSettings[directoryFolder].ToString();

            if (!string.IsNullOrEmpty(directory))
                if (Directory.Exists(directory))
                {
                    string directoryElement = directory + @"\" + idFolder;

                    if (!string.IsNullOrEmpty(directoryElement))
                        if (Directory.Exists(directoryElement))
                        {
                            DirectoryInfo directoryElem = new DirectoryInfo(directoryElement);
                            FileInfo[] files = directoryElem.GetFiles();
                            // Check if folder don't empty
                            if (files.Any())
                                return true;
                        }
                }
            return false;
        }
    }
}
