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
    public class DetailsModel : PageModel
    {
        private readonly Application.Data.BusinessDBContext _context;

        public DetailsModel(Application.Data.BusinessDBContext context)
        {
            _context = context;
        }

        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurants.FirstOrDefaultAsync(m => m.ID == id);

            if (Restaurant == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
