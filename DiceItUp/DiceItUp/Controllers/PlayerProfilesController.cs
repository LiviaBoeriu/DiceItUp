using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiceItUp.Models;

namespace DiceItUp.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private DiceItUpEntities db = new DiceItUpEntities();

        // GET: PlayerProfiles
        public ActionResult Index()
        {
            var playerProfiles = db.PlayerProfiles.Include(p => p.Location).Include(p => p.PlayerLogin).Include(p => p.ProfileTitle);
            return View(playerProfiles.ToList());
        }

        // GET: PlayerProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return HttpNotFound();
            }

            ViewData["Title"] = db.ProfileTitles.FirstOrDefault(row => row.profile_level == playerProfile.profile_level).title;
            ViewData["City"] = db.Locations.FirstOrDefault(row => row.location_id == playerProfile.location_id).city;
            ViewData["Gender"] = playerProfile.gender.ToUpper() == "M" ? "Male" : "Female";
            return View(playerProfile);
        }

        // GET: PlayerProfiles/Create
        public ActionResult Create()
        {
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code");
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email");
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title");
            return View();
        }

        // POST: PlayerProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "player_id,first_name,last_name,date_of_birth,gender,location_id,profile_level,description,feedback")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                db.PlayerProfiles.Add(playerProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", playerProfile.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", playerProfile.player_id);
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title", playerProfile.profile_level);
            return View(playerProfile);
        }

        // GET: PlayerProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "zip_code", playerProfile.location_id);
            ViewBag.player_id = new SelectList(db.PlayerLogins, "player_id", "email", playerProfile.player_id);
            ViewBag.profile_level = new SelectList(db.ProfileTitles, "profile_level", "title", playerProfile.profile_level);
            return View(playerProfile);
        }

        // POST: PlayerProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "profile_level,date_of_birth,location_id,feedback")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                int playerId = Int32.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                playerProfile.player_id = playerId;

                db.Entry(playerProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = playerProfile.player_id});
            }
            
            return View();
        }

        // GET: PlayerProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            if (playerProfile == null)
            {
                return HttpNotFound();
            }
            return View(playerProfile);
        }

        // POST: PlayerProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerProfile playerProfile = db.PlayerProfiles.Find(id);
            db.PlayerProfiles.Remove(playerProfile);
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
