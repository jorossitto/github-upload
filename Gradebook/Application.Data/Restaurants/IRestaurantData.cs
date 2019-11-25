using AppCore.Entities;
using System.Collections.Generic;

namespace AppCore.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetResturantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Delete(int id);
        int GetCountOfRestaurants();
        int Commit();
    }
}
