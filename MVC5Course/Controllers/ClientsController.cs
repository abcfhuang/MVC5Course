﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.ActionFilters;
using PagedList;

namespace MVC5Course.Controllers
{
    [Logger]
    public class ClientsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        //ClientRepository repo = RepositoryHelper.GetClientRepository();

        //OccupationRepository repoOccupation = RepositoryHelper.GetOccupationRepository();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM data)
        {
            return View("LoginResult", data);
        }

        // GET: Clients
        public ActionResult Index(string city, int pageNo = 1)
        {
            //var client = db.Client.Include(c => c.Occupation).Take(10);

            //var client = repo.All().Take(10);

             var client =repo.SearchByCity(city);

             ViewData.Model = client.ToPagedList(pageNo, 10);

            //只搜尋city欄位且city出來
            var cityList = repo.All().Select(p => new { p.City }).Distinct().ToList();

            ViewBag.Cities = new SelectList(cityList, "City", "City", city);

            return View();

            //return View(client.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);

            Client client = repo.Find(id.Value);

            if (client == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderList = client.Order.ToList();

            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            //ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName");

            ViewBag.OccupationId = new SelectList(repoOccupation.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                //db.Client.Add(client);
                //db.SaveChanges();

                repo.Add(client);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            //ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);

            ViewBag.OccupationId = new SelectList(repoOccupation.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);

            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            //ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);

            ViewBag.OccupationId = new SelectList(repoOccupation.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(client).State = EntityState.Modified;
                //db.SaveChanges();

                repo.UnitOfWork.Context.Entry(client).State = EntityState.Modified;
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(repoOccupation.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);

            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Client client = db.Client.Find(id);
            //db.Client.Remove(client);
            //db.SaveChanges();

            Client client = repo.Find(id);
            repo.Delete(client);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repo.UnitOfWork.Context.Dispose();
                repoOccupation.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);

        }
    }
}
