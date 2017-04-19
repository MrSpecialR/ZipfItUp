using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZipfItUp.Data;
using ZipfItUp.DataTranfer;
using ZipfItUp.ModelHelper;
using ZipfItUp.Models;
using ZipfItUp.TextManipulator;

namespace ZipfItUp.Controllers
{
    public class DocumentsController : Controller
    {
        private OccuranceContext db = new OccuranceContext();

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.Documents.OrderByDescending(x=>x.DateUploaded).ThenByDescending(x=>x.WordCount).ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            WordInfo words = new WordInfo(document.Words.OrderByDescending(x => x.Occurances).Take(50).Select(x=> new Word()
            {
                WordString = x.Word.WordString,
                Occurances = x.Occurances
            }).ToList());
            DocumentWord MostUsedWord = Query.Document.GetMostUsedWord(document, db);
            long OnceUsedWords = Query.Document.NumberOfWordsUsedJustOnce(document, db);
            long TotalCount = document.Words.Count();


            ViewBag.WordsJSON = new MvcHtmlString(words.WordsJSON);
            ViewBag.OccurancesJSON = new MvcHtmlString(words.OccurancesJSON);
            ViewBag.EstimatedOccurancesJSON = new MvcHtmlString(words.EstimatedOccurancesJSON);
            ViewBag.MostUsedWord = new MvcHtmlString($"<strong>{MostUsedWord.Word.WordString}</strong> - makes up for {(100*MostUsedWord.Occurances/ document.WordCount):f2}% of all words used.");
            ViewBag.WordUsage = new MvcHtmlString($"[{TotalCount - OnceUsedWords}, {OnceUsedWords}]");

            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,DocType,UploadedFile, UserText")] FileUpload document)
        {

            if (ModelState.IsValid)
            {
                if (document.UploadedFile != null)
                {
                    string fileName = document.UploadedFile.FileName;
                    string path = "";
                    if (string.IsNullOrEmpty(document.Name))
                    {
                        path = Server.MapPath($"~/UploadedFiles/{TextExtractor.GenerateFileName(fileName)}");
                    }
                    else
                    {
                        path = Server.MapPath($"~/UploadedFiles/{TextExtractor.GenerateFileName(fileName, document.Name)}");
                    }
                    document.UploadedFile.SaveAs(path);
                    DatabaseInsert.UploadFileToDatabase(path);
                    return RedirectToAction("Index");
                } else if (!string.IsNullOrEmpty(document.UserText))
                {
                    
                    string path = "";
                    if (string.IsNullOrEmpty(document.Name))
                    {
                        path = Server.MapPath($"~/UploadedFiles/{TextExtractor.GenerateFileName("." + TextExtractor.GetExtensionFromEnum(document.DocType))}");
                    }
                    else
                    {
                        path = Server.MapPath($"~/UploadedFiles/{TextExtractor.GenerateFileName(TextExtractor.GetExtensionFromEnum(document.DocType), document.Name)}");
                    }
                    System.IO.File.WriteAllText(path, document.UserText);
                    DatabaseInsert.UploadFileToDatabase(path);
                    return RedirectToAction("Index");
                }
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            List<DocumentWord> documentWords = document.Words.ToList();
            DatabaseDelete.DeleteDocumentWords(documentWords, db);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListWords(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document doc = db.Documents.First(x => id == x.Id);
            if (doc == null)
            {
                return HttpNotFound();
            }

            if (page == null)
            {
                page = 1;
            }
            int numberOfPages = (int) Math.Ceiling((double)doc.Words.Count()/30);
            if (page > numberOfPages)
            {
                return HttpNotFound();
            }

            var words = doc.Words.OrderByDescending(x=>x.Occurances).Skip((page.Value -1)*30).Take(30).Select(x=> new Word()
            {
                WordString = x.Word.WordString,
                Occurances = x.Occurances
            });

            WordInfo WordInfo = new WordInfo(words.ToList());
            ViewBag.WordsJSON = new MvcHtmlString(WordInfo.WordsJSON);
            ViewBag.OccurancesJSON = new MvcHtmlString(WordInfo.OccurancesJSON);

            ViewBag.Id = id;
            ViewBag.Page = page;
            ViewBag.IsFirstPage = page == 1;
            ViewBag.IsLastPage = page == numberOfPages;

            return View(words);
        }

        public ActionResult DocumentType()
        {
            return View();
        }

        [HttpPost, ActionName("DocumentType")]
        public ActionResult DocumentType([Bind(Include ="DocumentType")] DocumentType doc)
        {
            throw new NotImplementedException();
        }
    }
}
