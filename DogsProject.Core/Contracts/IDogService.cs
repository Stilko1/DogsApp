using DogsProject.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DogsProject.Core.Contracts
{
    public interface IDogService
    {
        Task<bool> CreateAsync(string name, int age, int breedId, string? picture);
        Task<bool> UpdateDog(int dogId, string name, int age, int breedId, string? picture);

        Task<IEnumerable<Dog>> GetDogsAsync();
        Task<Dog> GetDogByIdAsync(int dogId);
        Task<bool> RemoveByIdAsync(int dogId);
        Task<IEnumerable<Dog>> GetDogsAsync(string searchStringBreed, string searchStringName);
    }
}
