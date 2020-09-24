﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserProfileRepository _userRepo;


        public UserController(IUserProfileRepository userRepository)
        {
            _userRepo = userRepository;
        }
        // GET: UserController
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userRepo.GetAllUserProfiles();
            return View(userProfiles);
        }
        public ActionResult DeactivatedIndex()
        {
            List<UserProfile> userProfiles = _userRepo.GetAllDeactivatedUserProfiles();
            return View(userProfiles);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var user = _userRepo.GetUserProfileById(id);
            if (user != null)
            {
                return View(user);
                
            }
            else
            {
                return NotFound();
            }
           
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Deactivate(int id)
        {
            var user = _userRepo.GetUserProfileById(id);
            if (user != null)
            {
                return View(user);

            }
            else
            {
                return NotFound();
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(UserProfile profile )
        {
            try
            {
                _userRepo.DeactivateProfile(profile.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(profile);
            }
        }

        public ActionResult Reactivate(int id)
        {
            var user = _userRepo.GetUserProfileById(id);
            if (user != null)
            {
                return View(user);

            }
            else
            {
                return NotFound();
            }
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(UserProfile profile)
        {
            try
            {
                _userRepo.ReactivateProfile(profile.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(profile);
            }
        }
    }
}
