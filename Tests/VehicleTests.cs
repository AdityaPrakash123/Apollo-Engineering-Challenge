using ApolloEngineeringChallenge.Controllers;
using ApolloEngineeringChallenge.DTO;
using ApolloEngineeringChallenge.Models;
using ApolloEngineeringChallenge.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ApolloEngineeringChallenge.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void GetAllVehicles_ReturnsOkResult_WithListOfVehicles()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            context.Vehicles.AddRange(new List<Vehicle>
            {
                new Vehicle { VIN = "VIN12345678901234", ManufacturerName = "Toyota", ModelName = "Corolla", ModelYear = 2020, HorsePower = "150", PurchasePrice = 20000, FuelType = "Petrol", Description = "Compact car" },
                new Vehicle { VIN = "VIN12345678901235", ManufacturerName = "Honda", ModelName = "Civic", ModelYear = 2021, HorsePower = "160", PurchasePrice = 22000, FuelType = "Petrol", Description = "Sedan" }
            });
            context.SaveChanges();
            var controller = new VehicleController(context);

            // Act
            var result = controller.GetAllVehicles() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.True(response.Success);
            var vehicles = Assert.IsAssignableFrom<IEnumerable<Vehicle>>(response.Data);
            Assert.Equal(2, vehicles.Count());
        }

        [Fact]
        public void CreateVehicle_WithValidVehicle_ReturnsCreatedResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);
            var newVehicleDto = new VehicleDTO
            {
                ManufacturerName = "Ford",
                ModelName = "Fiesta",
                ModelYear = 2022,
                HorsePower = "120",
                PurchasePrice = 18000,
                FuelType = "Petrol",
                Description = "Hatchback"
            };

            // Act
            var result = controller.CreateVehicle(newVehicleDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.True(response.Success);
            var responseData = response.Data as dynamic;
            Assert.NotNull(responseData);
            Assert.NotNull(responseData.VIN);
        }

        [Fact]
        public void CreateVehicle_WithNullDto_ReturnsUnprocessableEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);

            // Act
            var result = controller.CreateVehicle(null) as UnprocessableEntityObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(422, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public void GetVehicle_WithValidVin_ReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);
            var newVehicleDto = new VehicleDTO
            {
                ManufacturerName = "Toyota",
                ModelName = "Corolla",
                ModelYear = 2020,
                HorsePower = "150",
                PurchasePrice = 20000,
                FuelType = "Petrol",
                Description = "Compact car"
            };
            var createResult = controller.CreateVehicle(newVehicleDto) as CreatedAtActionResult;
            var createResponse = createResult.Value as APIResponse;
            var createdVehicleData = createResponse.Data as dynamic;
            string vin = createdVehicleData.VIN;

            // Act
            var result = controller.GetVehicle(vin) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.True(response.Success);
            var vehicleData = response.Data as Vehicle;
            Assert.NotNull(vehicleData);
            Assert.Equal(vin, vehicleData.VIN);
        }

        [Fact]
        public void GetVehicle_WithInvalidVin_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);

            // Act
            var result = controller.GetVehicle("INVALIDVIN1234567") as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public void UpdateVehicle_WithValidVinAndDto_ReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);
            var newVehicleDto = new VehicleDTO
            {
                ManufacturerName = "Honda",
                ModelName = "Civic",
                ModelYear = 2021,
                HorsePower = "150",
                PurchasePrice = 22000,
                FuelType = "Petrol",
                Description = "Sedan"
            };
            var createResult = controller.CreateVehicle(newVehicleDto) as CreatedAtActionResult;
            var createResponse = createResult.Value as APIResponse;
            var createdVehicleData = createResponse.Data as dynamic;
            string vin = createdVehicleData.VIN;
            var updatedVehicleDto = new VehicleDTO
            {
                ManufacturerName = "Honda Updated",
                ModelName = "Accord",
                ModelYear = 2023,
                Description = "Updated Sedan",
                HorsePower = "180",
                PurchasePrice = 25000,
                FuelType = "Hybrid"
            };

            // Act
            var result = controller.UpdateVehicle(vin, updatedVehicleDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var responseObj = result.Value as APIResponse;
            Assert.NotNull(responseObj);
            Assert.True(responseObj.Success);
            var vehicleData = responseObj.Data as Vehicle;
            Assert.NotNull(vehicleData);
            Assert.Equal("Accord", vehicleData.ModelName);
        }

        [Fact]
        public void DeleteVehicle_WithValidVin_ReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);
            var newVehicleDto = new VehicleDTO
            {
                ManufacturerName = "Toyota",
                ModelName = "Corolla",
                ModelYear = 2020,
                HorsePower = "150",
                PurchasePrice = 20000,
                FuelType = "Petrol",
                Description = "Compact car"
            };
            var createResult = controller.CreateVehicle(newVehicleDto) as CreatedAtActionResult;
            var createResponse = (APIResponse)createResult.Value;
            var createdVehicleData = (Vehicle)createResponse.Data;
            string vin = createdVehicleData.VIN;

            // Act
            var result = controller.DeleteVehicle(vin) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.True(response.Success);
        }

        [Fact]
        public void DeleteVehicle_WithInvalidVin_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new VehicleContext(options);
            var controller = new VehicleController(context);

            // Act
            var result = controller.DeleteVehicle("INVALIDVIN1234567") as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            var response = result.Value as APIResponse;
            Assert.NotNull(response);
            Assert.False(response.Success);
        }
    }
}