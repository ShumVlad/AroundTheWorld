using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRouteService _routeService;

        public RouteController(IMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
            _routeService = routeService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create(CreateRouteViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("model");
            }
            var routeDTO = _mapper.Map<CreateRouteDTO>(model);
            bool result = await _routeService.Create(routeDTO);
            return result;
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("model");
            }
            bool result = await _routeService.Delete(id);
            return result;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<AroundTheWorld_Persistence.Models.Route> Get(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            AroundTheWorld_Persistence.Models.Route result = await _routeService.Get(id);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<bool> Update(CreateRouteDTO route)
        {
            if (route == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _routeService.Update(route);
            return result;
        }

        [HttpGet]
        [Route("GetUserRoutes")]
        public async Task<List<GetRouteViewModel>> GetUserRoutes(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException();
            }
            List<GetRouteDto> getRoutesDto = await _routeService.GetUserRoutes(userId);
            List<GetRouteViewModel> result = _mapper.Map<List<GetRouteViewModel>>(getRoutesDto);
            return result;
        }
        [HttpGet]
        [Route("GetCompanyRoutes")]
        public async Task<List<GetRouteViewModel>> GetCompanyRoutes(string companyId)
        {
            if (companyId == null)
            {
                throw new ArgumentNullException();
            }
            List<GetRouteDto> getRoutesDto = await _routeService.GetCompanyRoutes(companyId);
            List<GetRouteViewModel> result = _mapper.Map<List<GetRouteViewModel>>(getRoutesDto);
            return result;
        }
    }
}