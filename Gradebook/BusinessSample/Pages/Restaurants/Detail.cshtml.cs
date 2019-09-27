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
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        [TempData]
        public string Message { get; set; }
        public Restaurant Restaurant { get; set; }

        public DetailModel (IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}