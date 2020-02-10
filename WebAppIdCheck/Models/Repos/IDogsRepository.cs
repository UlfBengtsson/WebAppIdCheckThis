using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppIdCheck.Models.Repos
{
    public interface IDogsRepository
    {
        //CRUD

        Dog Create(Dog dog);

        Dog Find(int id);

        List<Dog> All();

        Dog Edit(Dog dog);

        bool Delete(Dog dog);
    }
}
