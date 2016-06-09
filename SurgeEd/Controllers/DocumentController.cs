using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurgeEd.Models;
using SurgeEd.ViewModels;

namespace SurgeEd.Controllers
{
    public class DocumentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var dbContext = new MyStoreContext())
            {
                //don't include the contents field, makes the query too slow.
                var docs = (dbContext.Document
                    .OrderBy(u => u.UploadDate)
                    .Select(d => new DocumentViewModel
                    {
                        DocumentId = d.DocumentId,
                        Title = d.Title,
                        UploadDate = d.UploadDate,
                        UploadUserId = d.UploadUserId
                    }));

                DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
                {
                    Documents = docs.ToList()
                };
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, DocumentDetailsViewModel docViewModel)
        {
            using (var dbContext = new MyStoreContext())
            {
                if (file != null && file.ContentLength > 0)
                    try
                    {

                        Document doc = new Document()
                        {
                            Title = docViewModel.Title,
                            Contents = new byte[file.ContentLength],
                            UploadDate = DateTime.Now,
                            UploadUserId = "BobBob91@gmail.com"

                        };

                        dbContext.Document.Add(doc);
                        dbContext.SaveChanges();


                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                //go back to index and the new doc should show
                var docs = (dbContext.Document
                               .OrderBy(u => u.UploadDate)
                               .Select(d => new DocumentViewModel
                               {
                                   DocumentId = d.DocumentId,
                                   Title = d.Title,
                                   UploadDate = d.UploadDate,
                                   UploadUserId = d.UploadUserId
                               }));

                DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
                {
                    Documents = docs.ToList()
                };
                return View(vm);
            }
        }

        public ActionResult FileDownLoad(int documentId)
        {
            using (var dbContext = new MyStoreContext())
            {
                var fileId = documentId;
                var myFile = dbContext.Document.SingleOrDefault(x => x.DocumentId == fileId);

                if (myFile != null)
                {
                    byte[] fileBytes = myFile.Contents.ToArray();
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, myFile.Title);
                }
                else return null;

            }
        }
    }
}