using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppIdCheck.Models.Repos;
using WebAppIdCheck.Models.ViewModels;

namespace WebAppIdCheck.Models.Services
{
    public class DogsService : IDogsService
    {
        private readonly IDogsRepository _dogsRepository;
        public DogsService(IDogsRepository dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public List<Dog> All()
        {
            return _dogsRepository.All();
        }

        public Dog Create(string name, string breed)
        {
            return _dogsRepository.Create(new Dog() { Name = name, Breed = breed });
        }

        public bool Delete(int id)
        {
            Dog dog = Find(id);

            if (dog == null)
            {
                return false;
            }
            else
            {
                return _dogsRepository.Delete(dog);
            }
        }

        public Dog Edit(int id, DogViewModel dog)
        {
            return _dogsRepository.Edit(new Dog() { Id= id, Name = dog.Name, Breed = dog.Breed });
        }

        public Dog Find(int id)
        {
            return _dogsRepository.Find(id);
        }

        public Dog ViewModelToDog(int id, DogViewModel dog)
        {
            return new Dog() { Id = id, Name = dog.Name, Breed = dog.Breed };
        }

        public DogViewModel DogToViewModel(Dog dog)
        {
            return new DogViewModel() { Name = dog.Name, Breed = dog.Breed };
        }
    }
}
