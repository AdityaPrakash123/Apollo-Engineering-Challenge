using ApolloEngineeringChallenge.DTO;
using ApolloEngineeringChallenge.Models;
using ApolloEngineeringChallenge.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApolloEngineeringChallenge.Controllers
{
    [ApiController]
    [Route("/")]
    public class VehicleController : ControllerBase // Changed to ControllerBase for API controllers
    {
        private readonly VehicleContext _vehicleContext;

        public VehicleController(VehicleContext vehicleContext)
        {
            _vehicleContext = vehicleContext;
        }

        [HttpGet]
        [Route("vehicle")]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _vehicleContext.Vehicles.ToList();
            var response = new APIResponse(true, "Vehicles retrieved successfully.", vehicles);
            return Ok(response);
        }

        [HttpPost]
        [Route("vehicle")]
        public IActionResult CreateVehicle(VehicleDTO vehicleDTO)
        {

            APIResponse response;
            if (vehicleDTO == null)
            {
                response = new APIResponse(false, "No vehicle details provided.");
                return UnprocessableEntity(response);
            }

            VINGenerator vinGenerator = new VINGenerator();

            Vehicle vehicle = new Vehicle
            {
                VIN = vinGenerator.createVIN(),
                ManufacturerName = vehicleDTO.ManufacturerName,
                Description = vehicleDTO.Description,
                HorsePower = vehicleDTO.HorsePower,
                ModelName = vehicleDTO.ModelName,
                ModelYear = vehicleDTO.ModelYear,
                PurchasePrice = vehicleDTO.PurchasePrice,
                FuelType = vehicleDTO.FuelType ?? "Petrol"
            };

            _vehicleContext.Vehicles.Add(vehicle);
            _vehicleContext.SaveChanges();

            response = new APIResponse(true, "Vehicle created successfully.", vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { vin = vehicle.VIN }, response);
        }

        [HttpGet]
        [Route("vehicle/{vin}")]
        public IActionResult GetVehicle(string vin)
        {
            APIResponse response;
            if (string.IsNullOrEmpty(vin) || vin.Length != 17)
            {
                response = new APIResponse(false, "Invalid VIN provided.");
                return BadRequest(response);
            }

            var vehicle = _vehicleContext.Vehicles.FirstOrDefault(x => x.VIN.ToLower() == vin.ToLower());

            if (vehicle == null)
            {
                response = new APIResponse(false, "Vehicle not found.");
                return NotFound(response);
            }

            var responseData = vehicle;
            response = new APIResponse(true, "Vehicle retrieved successfully.", responseData);
            return Ok(response);
        }

        [HttpPut]
        [Route("vehicle/{vin}")]
        public IActionResult UpdateVehicle(string vin, VehicleDTO updatedVehicle)
        {
            APIResponse response;
            if (string.IsNullOrEmpty(vin) || updatedVehicle == null)
            {
                response = new APIResponse(false, "Invalid VIN or vehicle data provided.");
                return BadRequest(response);
            }

            var existingVehicle = _vehicleContext.Vehicles.FirstOrDefault(x => x.VIN.ToLower() == vin.ToLower());

            if (existingVehicle == null)
            {
                response = new APIResponse(false, "Vehicle not found.");
                return NotFound(response);
            }

            existingVehicle.ManufacturerName = updatedVehicle.ManufacturerName;
            existingVehicle.Description = updatedVehicle.Description;
            existingVehicle.HorsePower = updatedVehicle.HorsePower;
            existingVehicle.ModelName = updatedVehicle.ModelName;
            existingVehicle.ModelYear = updatedVehicle.ModelYear;
            existingVehicle.PurchasePrice = updatedVehicle.PurchasePrice;
            existingVehicle.FuelType = updatedVehicle.FuelType;

            _vehicleContext.SaveChanges();

            var responseData = existingVehicle;
            response = new APIResponse(true, "Vehicle updated successfully.", responseData);
            return Ok(response);
        }

        [HttpDelete]
        [Route("vehicle/{vin}")]
        public IActionResult DeleteVehicle(string vin)
        {
            APIResponse response;
            if (string.IsNullOrEmpty(vin))
            {
                response = new APIResponse(false, "Invalid VIN provided.");
                return BadRequest(response);
            }

            var vehicle = _vehicleContext.Vehicles.FirstOrDefault(x => x.VIN.ToLower() == vin.ToLower());

            if (vehicle == null)
            {
                response = new APIResponse(false, "Vehicle not found.");
                return NotFound(response);
            }

            _vehicleContext.Vehicles.Remove(vehicle);
            _vehicleContext.SaveChanges();

            response = new APIResponse(true, "Vehicle deleted successfully.");
            return Ok(response);
        }
    }
}
