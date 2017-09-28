using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMWAssessmentWS
{
    public class FileStructure
    {
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime DateModified { get; set; }
    }
}