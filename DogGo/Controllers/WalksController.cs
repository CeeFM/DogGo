using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        // GET: WalksController
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();

            return View(walks);
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();
            List<Dog> dogs = _dogRepo.GetAllDogs();

            WalkFormViewModel vm = new WalkFormViewModel()
            {
                Walk = new Walks(),
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walks walk)
        {
            int i = 0;

            try
            {
                while (i < walk.SelectedIDs.Count)
                {
                    walk.Dog = _dogRepo.GetDogById(walk.SelectedIDs[i]);
                    walk.DogId = walk.Dog.Id;
                    _walksRepo.AddWalk(walk);
                    i++;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walk);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
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

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            Walks walk = _walksRepo.GetWalksById(id);

            return View(walk);
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walks walk)
        {
            try
            {
                _walksRepo.DeleteWalk(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(walk);
            }
        }

        public ActionResult DeleteMultiple()
        {
            List<Walks> walks = _walksRepo.GetAllWalks();

            DeleteWalkViewModel vm = new DeleteWalkViewModel()
            {
                Walk = new Walks(),
                Walks = walks
            };

            return View(vm);
        }

        // POST: Walks/DeleteMultiple
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMultiple(Walks walk)
        {
            int i = walk.SelectedIDs.Count;
            try
            {
                while (i > 0)
                {
                    _walksRepo.DeleteWalk(walk.SelectedIDs[i - 1]);
                    i--;
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View(walk);
            }
        }

        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalksRepository _walksRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalksController(IWalkerRepository walkerRepository, IOwnerRepository ownerRepo, IDogRepository dogRepo, IWalksRepository walksRepo)
        {
            _walkerRepo = walkerRepository;
            _ownerRepo = ownerRepo;
            _dogRepo = dogRepo;
            _walksRepo = walksRepo;
        }
    }
}
