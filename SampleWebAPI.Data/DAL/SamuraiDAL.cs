using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SamuraiDAL : ISamurai
    {
        private readonly SamuraiContext _context;
        public SamuraiDAL(SamuraiContext context)
        {
            _context = context;
        }


        public async Task Delete(int id)
        {
            try
            {
                var deleteSamurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSamurai == null)
                    throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
                _context.Samurais.Remove(deleteSamurai);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            var results = await _context.Samurais.OrderBy(s => s.Name).ToListAsync();
            return results;
        }

        public async Task<Samurai> GetById(int id)
        {
            var result = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Samurai>> GetByName(string name)
        {
            var samurais = await _context.Samurais.Where(s => s.Name.Contains(name))
                .OrderBy(s=>s.Name).ToListAsync();
            return samurais;
        }

        public async Task<IEnumerable<Samurai>> GetSamuraiWithQuotes()
        {
            var samurais = await _context.Samurais.Include(s => s.Quotes)
                .OrderBy(s => s.Name).AsNoTracking().ToListAsync();
            return samurais;
        }

        public async Task<IEnumerable<Samurai>> GetSamuraiWithSwordElementType()
        {
            var samurais = await _context.Samurais.Include(s => s.Swords).ThenInclude( s => s.Elements)
                                 .Include(s => s.Swords).ThenInclude(s => s.Type).AsNoTracking().ToListAsync();
            return samurais;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            try
            {
                _context.Samurais.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
            
        }

        public async Task<Samurai> Update(Samurai obj)
        {
            try
            {
                var updateSamurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSamurai == null) 
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");

                updateSamurai.Name = obj.Name;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
