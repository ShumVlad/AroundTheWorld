using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public RentItemService(UnitOfWork unit, UserManager<ApplicationUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
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

        public async Task<bool> RentItem(GetRentItemDto rentItemDto)
        {
            RentItem rentItem = await _unit.RentItemRepository.Get(rentItemDto.Id);
            ApplicationUser user = await _userManager.FindByNameAsync(rentItemDto.UserName);
            rentItem.IsRented = true;
            rentItem.UserId = user.Id;
            await _unit.RentItemRepository.Update(rentItem);
            _unit.Save();
            return true;
        }

        public async Task<bool> StopRenting(GetRentItemDto rentItemDto)
        {
            RentItem rentItem = await _unit.RentItemRepository.Get(rentItemDto.Id);
            ApplicationUser user = await _userManager.FindByIdAsync(rentItem.UserId);
            rentItem.IsRented = false;
            rentItem.UserId = user.Id;
            rentItem.Name = rentItemDto.Name;
            await _unit.RentItemRepository.Update(rentItem);
            _unit.Save();
            return true;
        }

        public async Task<List<GetRentItemDto>> GetAll()
        {
            List<RentItem> rentItems = await _unit.RentItemRepository.GetAll();
            List<GetRentItemDto> result = new List<GetRentItemDto>();
            Sensor sensor = new Sensor();
            foreach (var item in rentItems)
            {
                sensor = await _unit.SensorRepository.GetSensorForRentItem(item.Id);
                string userName = String.Empty;
                if (item.IsRented)
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(item.UserId);
                    userName = user.UserName;
                }
                GetRentItemDto getRentItem = new GetRentItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    IsRented = item.IsRented,
                    UserId = item.UserId,
                    CompanyId = item.CompanyId,
                    Latitude = sensor.Latitude,
                    Longitude = sensor.Longitude,
                    ImageLink = item.ImageLink,
                    Price = item.Price,
                    UserName = userName
            };
                
                result.Add(getRentItem);
            }
            return result;
        }

        public async Task<List<RentItem>> GetAllForCompany(string companyId)
        {
            var result = await _unit.RentItemRepository.GetRentItemsForCompany(companyId);
            return result;
        }
    }
}
