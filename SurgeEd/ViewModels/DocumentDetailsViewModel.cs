using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SurgeEd.Models;

namespace SurgeEd.ViewModels
{
    public class DocumentDetailsViewModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public Byte[] Contents { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUserId { get; set; }
        public List<Document> Documents { get; set; }
    }
}