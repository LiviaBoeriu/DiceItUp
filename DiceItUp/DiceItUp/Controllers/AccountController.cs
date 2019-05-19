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
    public class AccountController : Controller
    {
        private DiceItUpEntities1 db = new DiceItUpEntities1();

        // GET: Account/Register
        public ActionResult Register(int? id) {
            ViewBag.Locations = db.Locations.ToList(); 
            return View();
        }

        // POST: Account/RegisterLogin
        [HttpPost]
        public ActionResult RegisterLogin([Bind(Include = "email, password")] PlayerLogin playerLogin)
        {
            if (ModelState.IsValid)
            {
                // set token
                db.PlayerLogins.Add(playerLogin);
                db.SaveChanges();
                return RedirectToAction("Register", new { id = playerLogin.player_id });
            }
            
            return View();
        }

        // POST: Account/RegisterProfile/Id
        [HttpPost]
        public ActionResult RegisterProfile([Bind(Include = "first_name, last_name, gender, location_id")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                playerProfile.player_id = Int32.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                playerProfile.profile_level = 1;
                playerProfile.avatar_path = "sandy.png";

                db.PlayerProfiles.Add(playerProfile);
                db.SaveChanges();
                return RedirectToAction("Details", "PlayerProfiles", new { id = playerProfile.player_id });
            }

            return View();
        }

        public PartialViewResult LoginRegister()
        {
            return PartialView("LoginRegister", new PlayerLogin());
        }

        public PartialViewResult ProfileRegister()
        {
            return PartialView("ProfileRegister", new PlayerProfile());
        }

        [HttpPost]
        public int Login(String email, String password)
        {
            // check if there is an user with those credetials

            // IF TRUE => 
            // set token
            // RedirectToAction(Details, PlayerProfiles, new id)

            // IF FALSE => RedirectToAction(Register, Account)
            var user = db.PlayerLogins.FirstOrDefault(row => row.email == email && row.password == password);

            if (user != null)
            {
                return user.player_id;
            }
            else {
                return 0;
            }
        }
    }
}