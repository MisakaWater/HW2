using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW2.Models
{
    public class UploadFile
    {
        public long Id { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Uri { get; set; }
        public string Describe { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
