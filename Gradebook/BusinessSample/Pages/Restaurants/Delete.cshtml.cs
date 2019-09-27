using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.BL;
using Application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessSample.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);
            if(Restaurant == null)
            {
                return RedirectToPage(config.PageNotFound);
            }
            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {

            var restaurant = restaurantData.Delete(restaurantId);
            restaurantData.Commit();

            if(restaurant == null)
            {
                return RedirectToPage(config.PageNotFound);
            }

            TempData[config.MESSAGE] = $"{restaurant.Name} deleted";
            return RedirectToPage(config.ListPage);
        }
    }
}