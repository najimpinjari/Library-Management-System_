using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Management_System_.Models;

namespace Library_Management_System_.Controllers
{
    public class tblBooksController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: tblBooks
        public ActionResult Index(string search)
        {
            var books = db.tblBooks.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search) || b.AuthorName.Contains(search) || b.ISBN.Contains(search));
            }

            return View(books.ToList());
        }


        // GET: tblBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // GET: tblBooks/Create
        // GET: tblBooks/Create
        public ActionResult Create()
        {
            ViewBag.CategoryType = new SelectList(db.tblCategories, "CategoryType", "CategoryType");
            return View();
        }

        // POST: tblBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,CategoryType,AuthorName,PublicationName,ISBN,CreatedBy")] tblBook book)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicate ISBN
                if (db.tblBooks.Any(b => b.ISBN == book.ISBN))
                {
                    ModelState.AddModelError("ISBN", "Duplicate ISBN is not allowed.");
                    ViewBag.CategoryType = new SelectList(db.tblCategories, "CategoryType", "CategoryType", book.CategoryType);
                    return View(book);
                }

                // Validate ISBN for Horror category
                if (book.CategoryType == "Horror" && !book.ISBN.StartsWith("978"))
                {
                    ModelState.AddModelError("ISBN", "ISBN for Horror books must start with 978.");
                    ViewBag.CategoryType = new SelectList(db.tblCategories, "CategoryType", "CategoryType", book.CategoryType);
                    return View(book);
                }

                book.CreatedDate = DateTime.Now;
                db.tblBooks.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryType = new SelectList(db.tblCategories, "CategoryType", "CategoryType", book.CategoryType);
            return View(book);
        }



        // GET: tblBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: tblBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,CategoryType,AuthorName,PublicationName,ISBN,CreatedBy,CreatedDate")] tblBook tblBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBook);
        }

        // GET: tblBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: tblBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBook tblBook = db.tblBooks.Find(id);
            db.tblBooks.Remove(tblBook);
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
