using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppIdCheck.Models.ViewModels;

namespace WebAppIdCheck.Models.Services
{
    public interface IDogsService
    {
        //CRUD

        Dog Create(string name, string breed);

        Dog Find(int id);

        List<Dog> All();

        Dog Edit(int id, DogViewModel dog);

        bool Delete(int id);

        DogViewModel DogToViewModel(Dog dog);

        Dog ViewModelToDog(int id, DogViewModel dog);
    }
}
