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

        public IEnumerable<Author> GetAuthors(int pageNumber = 1, int pageSize = 5)
        {
            return context.Authors.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Author GetAuthor(Guid authorId)
        {
            if(authorId == Guid.Empty)
            {
                throw new ArgumentException(nameof(authorId));
            }
            return context.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public void AddAuthor(Author author)
        {
            try
            {
                if(author.CountryId == null)
                {
                    author.CountryId = "BE";
                }

                context.Authors.Add(author);
            }
            catch(Exception)
            {
                //potentially handled exception, log
                throw;
            }
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public void Dispose()
        {
        }

    }
}
