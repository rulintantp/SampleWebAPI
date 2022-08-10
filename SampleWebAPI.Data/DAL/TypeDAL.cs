using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class TypeDAL : IType
    {
        private readonly SamuraiContext _context;
        public TypeDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteType = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
                if (deleteType == null)
                    throw new Exception($"Data type dengan id {id} tidak ditemukan");
                _context.Types.Remove(deleteType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<SampleWebAPI.Domain.Type>> GetAll()
        {
            var results = await _context.Types.OrderBy(t => t.Name).ToListAsync();
            return results;
        }

        public async Task<SampleWebAPI.Domain.Type> GetById(int id)
        {
            var result = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (result == null) throw new Exception($"Data type dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<SampleWebAPI.Domain.Type> Insert(SampleWebAPI.Domain.Type obj)
        {
            try
            {
                _context.Types.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<SampleWebAPI.Domain.Type> Update(SampleWebAPI.Domain.Type obj)
        {
            try
            {
                var updateType = await _context.Types.FirstOrDefaultAsync(t => t.Id == obj.Id);
                if (updateType == null)
                    throw new Exception($"Data type dengan id {obj.Id} tidak ditemukan");

                updateType.Name = obj.Name;
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
