using DogsProject.Core.Contracts;
using DogsProject.Infrastructure;
using DogsProject.Infrastructure.Data.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Core.Services
{
    public class DogService : IDogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBreedService _breedService;

        public DogService(ApplicationDbContext context, IBreedService breedService)
        {
            _context = context;
            _breedService = breedService;
        }

        public async Task<bool> CreateAsync(string name, int age, int breedId, string? picture)
        {
            Dog item = new Dog
            {
                Name = name,
                Age = age,
                Breed = await _breedService.GetBreedByIdAsync(breedId),
                Picture = picture
            };

            await _context.Dogs.AddAsync(item);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<Dog> GetDogByIdAsync(int dogId)
        {
            return await _context.Dogs.FindAsync(dogId);
        }

        public async Task<IEnumerable<Dog>> GetDogsAsync(string searchStringBreed, string searchStringName)
        {
            var dogs = await _context.Dogs.ToListAsync();

            if (!String.IsNullOrEmpty(searchStringBreed) && !String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Breed.Name.Contains(searchStringBreed) && d.Name.Contains(searchStringName)).ToList();
            }

            else if (!String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Name.Contains(searchStringName)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBreed))
            {
                dogs = dogs.Where(d => d.Breed.Name.Contains(searchStringBreed)).ToList();
            }

            return dogs;
        }

        public async Task<IEnumerable<Dog>> GetDogsAsync()
        {
            var dogs = await _context.Dogs.ToListAsync();
            return dogs;
        }

        public async Task<bool> RemoveByIdAsync(int dogId)
        {
            var dog = await GetDogByIdAsync(dogId);
            if (dog == default(Dog))
            {
                return false;
            }

            _context.Remove(dog);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateDog(int dogId, string name, int age, int breedId, string picture)
        {
            var dog = await GetDogByIdAsync(dogId);

            if (dog == default(Dog)) { return false; }

            dog.Name = name;
            dog.Age = age;
            dog.Breed = await _breedService.GetBreedByIdAsync(breedId);
            dog.Picture = picture;
            _context.Update(dog);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}
