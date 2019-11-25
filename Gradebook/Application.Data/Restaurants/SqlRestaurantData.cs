﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppCore.Entities;

namespace AppCore.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly BusinessDBContext db;

        public SqlRestaurantData(BusinessDBContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
            //return db.Restaurants.Include(a => a.Address);
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetResturantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby name
                        select r;

            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
