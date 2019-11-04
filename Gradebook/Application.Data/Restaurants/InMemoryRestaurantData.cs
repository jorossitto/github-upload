using ACM.BL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = CreateDefaultResturants();
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.ID == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.ID = restaurants.Max(r => r.ID) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.ID == updatedRestaurant.ID);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Address.Country = updatedRestaurant.Address.Country;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetResturantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public List<Restaurant> CreateDefaultResturants()
        {
            var resturants = new List<Restaurant>()
            {
                new Restaurant{ID = 1, Name = "Scott's Pizza", Address = AddressRepository.Retrieve(1),
                Cuisine = CuisineType.Italian},
                new Restaurant{ID = 2, Name = "IshMil's Indian", Address = AddressRepository.Retrieve(1),
                Cuisine = CuisineType.Indian},
                new Restaurant{ID = 3, Name = "Juan's Burritos", Address = AddressRepository.Retrieve(1),
                Cuisine = CuisineType.Mexican}

            };

            return resturants;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.ID == id);

            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;

        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
