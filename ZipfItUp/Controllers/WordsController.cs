﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZipfItUp.Data;
using ZipfItUp.DataTranfer;
using ZipfItUp.Models;

namespace ZipfItUp.Controllers
{
    public class WordsController : Controller
    {
        private OccuranceContext db = new OccuranceContext();

        // GET: Words
        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageCount = (int)Math.Ceiling((double)db.Words.Count()/30);
            if (page > pageCount)
            {
                return HttpNotFound();
            }

            List<Word> Words = db.Words.OrderByDescending(x => x.Occurances).Skip((page.Value-1)*30).Take(30).ToList();
            WordInfo words = new WordInfo(Words);
            ViewBag.WordsJSON= new MvcHtmlString(words.WordsJSON);
            ViewBag.OccurancesJSON = new MvcHtmlString(words.OccurancesJSON);
            ViewBag.EstimatedOccurancesJSON = new MvcHtmlString(words.EstimatedOccurancesJSON);

            ViewBag.Page = page;
            ViewBag.IsFirstPage = page == 1;
            ViewBag.IsLastPage = page == pageCount;

            return View(Words);
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WordString,Occurances")] Word word)
        {
            if (ModelState.IsValid)
            {
                db.Words.Add(word);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(word);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WordString,Occurances")] Word word)
        {
            if (ModelState.IsValid)
            {
                db.Entry(word).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(word);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = db.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Word word = db.Words.Find(id);
            db.Words.Remove(word);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            ViewBag.NothingFound = true;
            ViewBag.GetAction = true;
            return View();
        }

        [HttpPost, ActionName("Search")]
        public ActionResult Search([Bind(Include = "search")]SearchQuery query)
        {
            string word = new string(query.Search.Where(char.IsLetter).ToArray()).ToLower();
            ViewBag.GetAction = false;
            Word wordObject = db.Words.FirstOrDefault(x => x.WordString == word);
            if (wordObject == null)
            {
                ViewBag.NothingFound = true;
                return View();
            }
            ViewBag.NothingFound = false;
            int overallRank = db.Words.OrderByDescending(x => x.Occurances).ToList().FindIndex(x=>x.WordString==wordObject.WordString) + 1;
            string output = $"<h2>Overall ranking: {overallRank} with {wordObject.Occurances} occurances. </h2><br/>";
            output += $"<h3>Found {wordObject.WordString} in {wordObject.Documents.Count()} documents: </h3>";
            List<WordRanking> rank = new List<WordRanking>();
            foreach (DocumentWord doc in wordObject.Documents.ToList())
            {
                int id = doc
                    .Document
                    .Words
                    .OrderByDescending(x=>x.Occurances)
                    .ToList()
                    .FindIndex(x => x.Word.WordString == doc.Word.WordString) + 1;
                rank.Add(new WordRanking()
                {
                    Rank = id,
                    DocumentId = doc.DocumentId,
                    FileName = doc.Document.FileName,
                    Occurances = doc.Occurances
                });
            }
            ViewBag.Result = new MvcHtmlString(output);
            ViewBag.Documents = rank;
            return View();
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
