using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class QuotesDAL : IQuote
    {
        private readonly SamuraiContext _context;
        public QuotesDAL(SamuraiContext context)
        {
            _context = context;
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Quote>> GetAll()
        {
            var quotes = await _context.Quotes.OrderBy(q => q.Text).ToListAsync();
            return quotes;
        }

        public Task<Quote> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Quote> Insert(Quote obj)
        {
            throw new NotImplementedException();
        }

        public Task<Quote> Update(Quote obj)
        {
            throw new NotImplementedException();
        }
    }
}
