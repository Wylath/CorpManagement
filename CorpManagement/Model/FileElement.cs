using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class FileElement
    {
        public string FilesPath { get; set; }
        public string Name { get; set; }

        public FileElement(string FilesPath, string name)
        {
            this.FilesPath = FilesPath;
            this.Name = name;
        }
    }
}
