using System;

namespace SurgeEd.ViewModels
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUserId { get; set; }
    }
}