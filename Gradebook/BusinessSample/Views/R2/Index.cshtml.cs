using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppCore.Data;
using AppCore.Entities;

namespace BusinessSample.Pages.R2
{
    public class IndexModel : PageModel
    {
        private readonly BusinessDBContext _context;

        public IndexModel(BusinessDBContext context)
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
