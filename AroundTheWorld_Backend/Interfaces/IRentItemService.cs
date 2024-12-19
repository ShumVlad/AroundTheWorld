using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IRentItemService
    {
        Task<RentItem> Get(string id);

        Task<bool> Add(RentItem rentItem);

        Task<List<GetRentItemDto>> GetAll();

        Task<List<RentItem>> GetAllForCompany(string companyId);
        Task<bool> RentItem(GetRentItemDto rentItemDto);
        Task<bool> StopRenting(GetRentItemDto rentItemDto);
    }
}
