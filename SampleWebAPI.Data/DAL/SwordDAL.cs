using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SwordDAL : ISword
    {
        private readonly SamuraiContext _context;
        public SwordDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                    throw new Exception($"Data sword dengan id {id} tidak ditemukan");
                _context.Swords.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<IEnumerable<Sword>> GetAll()
        {
            var results = await _context.Swords.OrderBy(s => s.Weight).ToListAsync();
            return results;
        }

        public async Task<Sword> GetById(int id)
        {
            var result = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Sword>> GetByName(string name)
        {
            var swords = await _context.Swords.Where(s => s.Name.Contains(name))
                 .OrderBy(s => s.Name).ToListAsync();
            return swords;
        }

        public async Task<Sword> Insert(Sword obj)
        {
            try
            {
                _context.Swords.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> Update(Sword obj)
        {
            try
            {
                var updateSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data sword dengan id {obj.Id} tidak ditemukan");

                updateSword.Name = obj.Name;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<ElementSword> AddElementSword(ElementSword obj)
        {
            try
            {
                ElementSword elementSword = new ElementSword();

                var element = await _context.Elements.Where(s => s.Id == obj.ElementsId).FirstOrDefaultAsync();
                var sword = await _context.Swords.Where(s => s.Id == obj.SwordsId).FirstOrDefaultAsync();

                if (element != null && sword != null)
                {
                    element.Swords.Add(sword);
                    await _context.SaveChangesAsync();
                    elementSword = await _context.ElementSword.Where(es => es.SwordsId == obj.SwordsId && es.ElementsId == obj.ElementsId).FirstOrDefaultAsync();
                }

                return elementSword;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task DeleteElementFromSword(int swordId)
        {
            try
            {
                var elementSword = await _context.ElementSword.Where(es => es.SwordsId == swordId)
                .ToListAsync();
                _context.ElementSword.RemoveRange(elementSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<IEnumerable<Sword>> GetSwordWithType()
        {
            var swords = await _context.Swords.Include(s => s.Type)
                .OrderBy(s => s.Name).AsNoTracking().ToListAsync();
            return swords;
        }

    }
}


