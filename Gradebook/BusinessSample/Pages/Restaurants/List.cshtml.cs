using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.BL;
using AppCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BusinessSample.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, IRestaurantData restaurantData, ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }
        public void OnGet()
        {
            logger.LogError("Executing ListModel");
            Message = config["Message"];
            Restaurants = restaurantData.GetResturantsByName(SearchTerm);
        }
    }
}