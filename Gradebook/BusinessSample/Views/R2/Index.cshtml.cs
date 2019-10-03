using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ACM.BL;
using Application.Data;

namespace BusinessSample.Pages.R2
{
    public class IndexModel : PageModel
    {
        private readonly Application.Data.BusinessDBContext _context;

        public IndexModel(Application.Data.BusinessDBContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get;set; }

        public async Task OnGetAsync()
        {
            Restaurant = await _context.Restaurants.ToListAsync();
        }
    }
}
