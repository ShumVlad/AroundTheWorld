﻿using AroundTheWorld.ViewModels;
using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route = AroundTheWorld_Persistence.Models.Route;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationRouteController : ControllerBase
    {
        private readonly ILocationRouteService _locationRouteService;
        private readonly IMapper _mapper;

        public LocationRouteController(ILocationRouteService locationRouteService, IMapper mapper)
        {
            _locationRouteService = locationRouteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<bool> Create(LocationRouteViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            LocationRouteDTO locationRouteDTO = _mapper.Map<LocationRouteDTO>(model);
            var result = await _locationRouteService.AddLocationRoute(locationRouteDTO);
            return result;
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = await _locationRouteService.DeleteLocationRoute(id);
            return result;
        }

        [HttpGet]
        [Route("GetLocationsFromRoute")]
        public async Task<List<GetLocationFromRouteDto>> GetLocationsFromRoute(string routeId)
        {
            if (routeId == null)
            {
                throw new ArgumentNullException(nameof(routeId));
            }
            List<GetLocationFromRouteDto> locations= await _locationRouteService.GetLocationsInRoute(routeId);
            return locations;
        }

        [HttpGet]
        [Route("GetRoutesWithLocation")]
        public async Task<List<Route>> GetRoutesWithLocation(string locationId)
        {
            if (locationId == null)
            {
                throw new ArgumentNullException(nameof(locationId));
            }
            List<AroundTheWorld_Persistence.Models.Route> routes = await _locationRouteService.GetRoutesWithLocation(locationId);
            return routes;
        }
    }
}
