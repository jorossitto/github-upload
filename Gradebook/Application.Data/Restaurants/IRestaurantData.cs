using ACM.BL;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
