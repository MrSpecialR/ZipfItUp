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
            WordInfo words = new WordInfo(document.Words.OrderByDescending(x => x.Occurances).Take(30).Select(x=> new Word()
            {
                WordString = x.Word.WordString,
                Occurances = x.Occurances
            }).ToList());
            ViewBag.WordsJSON = new MvcHtmlString(words.WordsJSON);
            ViewBag.OccurancesJSON = new MvcHtmlString(words.OccurancesJSON);
            ViewBag.EstimatedOccurancesJSON = new MvcHtmlString(words.EstimatedOccurancesJSON);
            DocumentWord MostUsedWord = Query.Document.GetMostUsedWord(document, db);
            long OnceUsedWords = Query.Document.NumberOfWordsUsedJustOnce(document, db);
            long TotalCount = document.Words.Count();
            ViewBag.MostUsedWord = $"{MostUsedWord.Word.WordString} - makes up for {(100*MostUsedWord.Occurances/ document.WordCount):f2}%";
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
        public ActionResult Create([Bind(Include = "Name,DocumentType,UploadedFile")] FileUpload document)
        {

            if (ModelState.IsValid)
            {
                if (document.UploadedFile != null)
                {
                    string fileName = document.UploadedFile.FileName;
                    string path = Server.MapPath($"~/UploadedFiles/{fileName}");
                    document.UploadedFile.SaveAs(path);
                    DatabaseInsert.UploadFileToDatabase(path);
                    return RedirectToAction("Index");
                }
            }
            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,DocumentType,FilePath,WordCount,DateUploaded")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
    }
}
