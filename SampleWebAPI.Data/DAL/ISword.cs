using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public interface ISword : ICrud<Sword>
    {
        Task<IEnumerable<Sword>> GetByName(string name);
        Task<ElementSword> AddElementSword(ElementSword obj);
        Task DeleteElementFromSword(int Id);
        Task<IEnumerable<Sword>> GetSwordWithType();
    }
}
