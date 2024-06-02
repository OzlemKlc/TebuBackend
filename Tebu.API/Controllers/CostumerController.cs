using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tebu.API.Controllers.DTO;
using Tebu.API.Data.Models;
using Tebu.API.Repository;
using Tebu.API.Service;
using Tebu.API.Service.DTO;
using Tebu.API.Service.Mappers;

namespace Tebu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumerController : ControllerBase
    {

        private AddressRepository addressRepository;
        private VehicleRepository vehicleRepository;
        private CurrentUserService currentUserService;

        public CostumerController(AddressRepository addressRepository, VehicleRepository vehicleRepository, CurrentUserService currentUserService)
        {
            this.addressRepository = addressRepository;
            this.vehicleRepository = vehicleRepository;
            this.currentUserService = currentUserService;
        }
        
        //todo take these to services. we have some time management issues here. it will be more time consuming in the long run.

        [HttpPost("add-address")]
        [Authorize(Roles = "Customer")]
        public AddressDTO CreateWorker([FromBody] AddAddressRequest request)
        {
            var address = new Address
            {
                FullAdress = request.FullAdress,
                City = request.City,
                District = request.District,
                Name = request.Name,
                UserId = currentUserService.User.Id
            };

            addressRepository.Add(address);

            addressRepository.SaveChanges();

            return address.ConvertToAddresDTO();
        }

        [HttpGet("get-addresses")]
        [Authorize(Roles = "Customer")]
        public List<AddressDTO> GetAddresses()
        {
            return addressRepository.FindBy(s => s.UserId == currentUserService.User.Id).ToList().Select(s => s.ConvertToAddresDTO()).ToList();
        }

        [HttpPost("add-vehicle")]
        [Authorize(Roles = "Customer")]
        public VehicleDTO AddVehicle([FromBody] AddVehicleRequest request)
        {
            var vehicle = new Vehicle
            {
                Brand = request.Brand,
                Model = request.Model,
                Name = request.Name,
                UserId = currentUserService.User.Id
            };

            vehicleRepository.Add(vehicle);
            vehicleRepository.SaveChanges();


            return vehicle.ConvertToVehicleDTO();
        }

        [HttpGet("get-vehicles")]
        [Authorize(Roles = "Customer")]
        public List<VehicleDTO> GetVehicles()
        {
            return vehicleRepository.FindBy(s => s.UserId == currentUserService.User.Id).ToList().Select(s => s.ConvertToVehicleDTO()).ToList();
        }
    }
}
