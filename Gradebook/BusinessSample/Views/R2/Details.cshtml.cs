﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppCore.Data;
using AppCore.Entities;

namespace BusinessSample.Pages.R2
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessDBContext _context;

        public DetailsModel(BusinessDBContext context)
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
