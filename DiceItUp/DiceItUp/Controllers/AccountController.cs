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
        private DiceItUpEntities db = new DiceItUpEntities();

        // GET: Account
        public ActionResult Register(int? id) {
            return View();
        }

        // POST: Account/RegisterLogin
        [HttpPost]
        public ActionResult RegisterLogin([Bind(Include = "email, password")] PlayerLogin playerLogin)
        {
            if (ModelState.IsValid)
            {
                db.PlayerLogins.Add(playerLogin);
                db.SaveChanges();
                return RedirectToAction("Register", new { id = playerLogin.player_id });
            }
            
            return View();
        }

        // POST: Account/RegisterProfile/Id
        [HttpPost]
        public ActionResult RegisterProfile([Bind(Include = "first_name, last_name, gender")] PlayerProfile playerProfile)
        {
            if (ModelState.IsValid)
            {
                playerProfile.player_id = Int32.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                
                db.PlayerProfiles.Add(playerProfile);
                db.SaveChanges();
                return RedirectToAction("Details", "PlayerProfiles", new { id = playerProfile.player_id});
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
    }
}