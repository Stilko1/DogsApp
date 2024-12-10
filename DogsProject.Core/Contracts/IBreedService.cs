using DogsProject.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Core.Contracts
{
    public interface IBreedService
    {
        Task<IEnumerable<Breed>> GetBreedsAsync();
        Task<Breed?> GetBreedByIdAsync(int id);
        Task<IEnumerable<Dog>> GetDogsByBreedAsync(int breedId);
    }
}
