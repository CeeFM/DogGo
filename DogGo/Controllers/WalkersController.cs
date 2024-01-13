using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        // GET: WalkersController
        public ActionResult Index()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();

            return View(walkers);
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetWalksByWalker(id);


            ProfileViewModel vm = new ProfileViewModel()
            {
                Walks = walks,
                Walker = walker
            };

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
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

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalksRepository _walksRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IOwnerRepository ownerRepo, IDogRepository dogRepo, IWalksRepository walksRepo)
        {
            _walkerRepo = walkerRepository;
            _ownerRepo = ownerRepo;
            _dogRepo = dogRepo;
            _walksRepo = walksRepo;
        }
    }
}
