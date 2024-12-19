using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentItemController : ControllerBase
    {
        private readonly IRentItemService _rentItemService;
        public RentItemController(IRentItemService rentItemService)
        {
            _rentItemService = rentItemService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<RentItem> Get(string id)
        {
            if (id == null)
            {
                throw new Exception(nameof(id));
            }
            RentItem result = await _rentItemService.Get(id);
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<bool> Add(RentItem rentItem)
        {
            if (rentItem == null)
            {
                throw new ArgumentNullException(nameof(rentItem));
            }
            var result = await _rentItemService.Add(rentItem);
            return result;
        }

        [HttpGet]
        [Route("GetRentItemsForCompany")]
        public async Task<List<RentItem>> GetPaginated(string companyId)
        {
            var rentItems = await _rentItemService.GetAllForCompany(companyId);
            return rentItems;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<GetRentItemDto>> GetAll()
        {
            var retItems = await _rentItemService.GetAll();
            return retItems;
        }

        [HttpPut]
        [Route("RentItem")]
        public async Task<bool> RentItem(GetRentItemDto rentItem)
        {
            if (rentItem == null)
            {
                throw new Exception(nameof(rentItem));
            }
            bool result = await _rentItemService.RentItem(rentItem);
            return result;
        }

        [HttpPut]
        [Route("StopRenting")]
        public async Task<bool> StopRenting(GetRentItemDto rentItem)
        {
            if (rentItem == null)
            {
                throw new Exception(nameof(rentItem));
            }
            bool result = await _rentItemService.StopRenting(rentItem);
            return result;
        }
    }
}
