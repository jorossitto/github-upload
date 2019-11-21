using AppCore.Data;
using AppCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Repository
{
    public class AuthorRepository : IDisposable
    {
        private BusinessDBContext context;
        public AuthorRepository(BusinessDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return context.Authors.ToList();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
