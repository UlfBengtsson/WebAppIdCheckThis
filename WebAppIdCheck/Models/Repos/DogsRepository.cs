using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppIdCheck.Data;

namespace WebAppIdCheck.Models.Repos
{
    public class DogsRepository : IDogsRepository
    {
        private readonly ApplicationDbContext _db;
        public DogsRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        public List<Dog> All()
        {
            return _db.Dogs.ToList();
        }

        public Dog Create(Dog dog)
        {
            var result = _db.Dogs.Add(dog);

            if (result.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                _db.SaveChanges();
                return dog;
            }

            return null;
        }

        public bool Delete(Dog dog)
        {
            var result = _db.Dogs.Remove(dog);

            if (result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
            {
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public Dog Edit(Dog dog)
        {
            var result = _db.Dogs.Update(dog);

            if (result.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
            {
                _db.SaveChanges();
                return dog;
            }

            return null;
        }

        public Dog Find(int id)
        {
            return _db.Dogs.SingleOrDefault(d => d.Id == id);
        }
    }
}
