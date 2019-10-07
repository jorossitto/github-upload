using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Data
{
    public class PieRepository : IPieRepository
    {
        private readonly BusinessDBContext db;
        public PieRepository(BusinessDBContext businessDBContext)
        {
            db = businessDBContext;
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return db.Pies.Include(c => c.Category);
            }
        }


        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return db.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return db.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
