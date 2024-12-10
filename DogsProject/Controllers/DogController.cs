using DogsProject.Core.Contracts;
using DogsProject.Core.Services;
using DogsProject.Infrastructure;
using DogsProject.Infrastructure.Data.Entities;
using DogsProject.Models.Breed;
using DogsProject.Models.Dog;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsProject.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogService _dogService;
        private readonly IBreedService _breedService;

        public DogController(IDogService dogService, IBreedService breedService)
        {
            _dogService = dogService;
            _breedService = breedService;
        }



        // GET: DogController
        public async Task<IActionResult> Index(string searchStringBreed, string searchStringName)
        {
            var dogsFromDb = await _dogService.GetDogsAsync(searchStringBreed, searchStringName);
            var dogs =  dogsFromDb.Select(dogFromDb => new DogAllViewModel
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                BreedName = dogFromDb.Breed.Name,
                Picture = dogFromDb.Picture,
            }).ToList();

            return View(dogs);
        }

        // GET: DogController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {

            Dog? dogFromDb = await _dogService.GetDogByIdAsync(id);
            if (dogFromDb == null)
            {
                return NotFound();
            }
            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                BreedName = dogFromDb.Breed.Name,
                Picture = dogFromDb.Picture,
            };
            return View(dog);
        }

        // GET: DogController/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dog = new DogCreateViewModel();
            var breeds = await _breedService.GetBreedsAsync();
            dog.Breeds = breeds
                .Select(c => new BreedPairViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();

            return View(dog);

        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DogCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var created = await _dogService.CreateAsync(model.Name, model.Age, model.BreedId, model.Picture);

                
                if(created) return RedirectToAction(nameof(Index));

            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        // GET: DogController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            Dog? dogFromDb = await _dogService.GetDogByIdAsync(id);
            if(dogFromDb == null)
            {
                return NotFound();
            }

            DogEditViewModel dog = new DogEditViewModel()
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                Picture = dogFromDb.Picture

            };
            
            var breeds = await _breedService.GetBreedsAsync();
            dog.Breeds = breeds
                .Select(c => new BreedPairViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();

            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DogEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var updated = await _dogService.UpdateDog(id, model.Name, model.Age, model.BreedId, model.Picture);

                if (updated) return RedirectToAction("Index");

            }

            return View(model);
        }

        // GET: DogController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            Dog? dogFromDb = await _dogService.GetDogByIdAsync(id);
            if(dogFromDb == null)
            {
                return NotFound();
            }

            DogDetailsViewModel dog = new DogDetailsViewModel
            {
                Id = dogFromDb.Id,
                Name = dogFromDb.Name,
                Age = dogFromDb.Age,
                BreedName = dogFromDb.Breed.Name,
                Picture = dogFromDb.Picture
            };

            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            
            var deleted = await _dogService.RemoveByIdAsync(id);
            if (deleted)
            {
                return RedirectToAction("Index", "Dog");
            }
            return View();

        }
    }
}
