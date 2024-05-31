using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Services
{
    public class RentItemService : IRentItemService
    {
        private readonly UnitOfWork _unit;
        public RentItemService(UnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<RentItem> Get(string id)
        {
            var result = await _unit.RentItemRepository.Get(id);
            return result;
        }

        public async Task<bool> Add(RentItem rentItem)
        {
            rentItem.Id = Guid.NewGuid().ToString();
            await _unit.RentItemRepository.Add(rentItem);
            _unit.Save();
            return true;
        }


        public async Task<List<RentItem>> GetAll()
        {
            var result = await _unit.RentItemRepository.GetAll();
            return result;
        }

        public async Task<List<RentItem>> GetAllForCompany(string companyId)
        {
            var result = await _unit.RentItemRepository.GetRentItemsForCompany(companyId);
            return result;
        }
    }
}
