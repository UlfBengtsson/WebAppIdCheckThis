using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppIdCheck.Models;
using WebAppIdCheck.Models.Services;
using WebAppIdCheck.Models.ViewModels;

namespace WebAppIdCheck.Controllers
{
    [Authorize]
    public class DogsController : Controller
    {
        IDogsService _dogsService;
        public DogsController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }

        // GET: Dogs
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_dogsService.All());
        }

        // GET: Dogs/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            return View(_dogsService.Find(id));
        }

        // GET: Dogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DogViewModel dog)
        {
            if (ModelState.IsValid)
            {
                Dog doggy = _dogsService.Create(dog.Name, dog.Breed);

                if (doggy == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to create.");
                    return View(dog);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(dog);
            }
        }

        // GET: Dogs/Edit/5
        public IActionResult Edit(int id)
        {
            return View(_dogsService.Find(id));
        }

        // POST: Dogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DogViewModel dog)
        {
            if (ModelState.IsValid)
            {
                Dog doggy = _dogsService.Edit(id, dog);

                if (doggy == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes.");
                    return View(_dogsService.ViewModelToDog(id, dog));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(_dogsService.ViewModelToDog(id, dog));
            }
        }

        // GET: Dogs/Delete/5
        public IActionResult Delete(int id)
        {
            Dog dog = _dogsService.Find(id);

            if (dog == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(dog);
        }

        // POST: Dogs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            _dogsService.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}