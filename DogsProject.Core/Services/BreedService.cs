using DogsProject.Core.Contracts;
using DogsProject.Infrastructure;
using DogsProject.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DogsProject.Core.Services
{
    public class BreedService : IBreedService
    {
        private readonly ApplicationDbContext _context;

        public BreedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Breed?> GetBreedByIdAsync(int id)
        {
            return await _context.Breeds.FindAsync(id);
        }

        public async Task<IEnumerable<Breed>> GetBreedsAsync()
        {
            var breeds = await _context.Breeds.ToListAsync();
            return breeds;
        }

        public async Task<IEnumerable<Dog>> GetDogsByBreedAsync(int breedId)
        {
            return await _context.Dogs
                .Where(x => x.BreedId == breedId).ToListAsync();
        }
    }
}
