# Apollo Engineering Challenge


# Overview
This project implements a web service with CRUD-style API access to a vehicle database. The service is built to handle vehicle data, identified uniquely by VINs (Vehicle Identification Numbers), and is capable of responding with appropriate HTTP status codes and JSON-formatted responses.

# API Endpoints


# 1. Get All Vehicles
* **Method:** GET
* **URI:** /vehicle
* **Response:** JSON array of all vehicle records
* **Status Code:** 200 OK
# 2. Create a Vehicle
* **Method:** POST
* **URI:** /vehicle
* **Request Body:** JSON object representing the new vehicle
* **Response:** JSON object of the created vehicle
* **Status Code:** 201 Created
# 3. Get a Vehicle by VIN
* **Method:** GET
* **URI:** /vehicle/{vin}
* **Response:** JSON object of the specified vehicle
* **Status Code:** 200 OK
# 4. Update a Vehicle by VIN
* **Method:** PUT
* **URI:** /vehicle/{vin}
* **Request Body:** JSON object representing the updated vehicle
* **Response:** JSON object of the updated vehicle
* **Status Code:** 200 OK
# 5. Delete a Vehicle by VIN
* **Method:** DELETE
* **URI:** /vehicle/{vin}
* **Response:** No content
* **Status Code:** 204 No Content

# Base URL
**Localhost:** http://localhost:5196/vehicle

**Azure:** https://apolloengineeringchallenge-facpatbjdxgdb4dz.canadacentral-01.azurewebsites.net/vehicle







# Error Handling
* 400 Bad Request: Returned when the server cannot parse the JSON input.
* 422 Unprocessable Entity: Returned when the JSON input is valid but fails validation due to malformed or null attributes.

# Install .NET
**1) Install .NET 7**
      
      brew link --force --overwrite dotnet-sdk@7

**2) Check if .NET has been installed**     

      dotnet --version

# Installation and Build Instructions
**1) CD into the Project Directory**
      
      cd /path/to/your/project
      
**2) Install Dependencies: Run the following command to install required dependencies:**
      
      dotnet restore

**3) Build the Project:**
      
      dotnet build

**4) Run the Application:**
      
      dotnet run

**5) Run the tests:**
      
      dotnet test


# GET all vehicles
**Localhost:** 

      curl http://localhost:5196/vehicle

**Azure:** 

      curl https://apolloengineeringchallenge-facpatbjdxgdb4dz.canadacentral-01.azurewebsites.net/vehicle

# POST to Create a vehicle
**Localhost:** 

      curl -X POST http://localhost:5196/vehicle -H "Content-Type: application/json" -d '{
        "ManufacturerName": "Toyota",
        "Description": "Compact sedan",
        "HorsePower": "130",
        "ModelName": "Corolla",
        "ModelYear": 2021,
        "PurchasePrice": 20000,
        "FuelType": "Petrol"
      }'



**Azure:** 

      curl -X POST https://apolloengineeringchallenge-facpatbjdxgdb4dz.canadacentral-01.azurewebsites.net/vehicle \
      -H "Content-Type: application/json" \
      -d '{
        "VIN": "1HGBH41JXMN109187",
        "ManufacturerName": "Toyota",
        "Description": "Compact sedan",
        "HorsePower": "150",
        "ModelName": "Corolla",
        "ModelYear": 2021,
        "PurchasePrice": 25000,
        "FuelType": "Petrol"
      }'

# GET a Vehicle by VIN:

**Localhost:** 

      curl http://localhost:5196/vehicle/VIN

**Azure:** 
      
      curl https://apolloengineeringchallenge-facpatbjdxgdb4dz.canadacentral-01.azurewebsites.net/vehicle/VIN


# PUT to a Vehicle by VIN:

**Localhost:** 

      curl -X PUT http://localhost:5196/vehicle/VIN \
      -H "Content-Type: application/json" \
      -d '{
        "ManufacturerName": "Toyota Updated",
        "Description": "Updated sedan",
        "HorsePower": "150",
        "ModelName": "Corolla Updated",
        "ModelYear": 2022,
        "PurchasePrice": 26000,
        "FuelType": "Hybrid"
      }'




**Azure:** 

      curl -X PUT https://apolloengineeringchallenge-facpatbjdxgdb4dz.canadacentral-01.azurewebsites.net/vehicle/VIN \
      -H "Content-Type: application/json" \
      -d '{
        "ManufacturerName": "Toyota Updated",
        "Description": "Updated sedan",
        "HorsePower": "150",
        "ModelName": "Corolla Updated",
        "ModelYear": 2022,
        "PurchasePrice": 26000,
        "FuelType": "Hybrid"
      }'




# DELETE a Vehicle by VIN:

**Localhost:** 

      curl -X DELETE http://localhost:5196/vehicle/VIN

**Azure:** 
      
      curl -X DELETE https://apolloengineeringchallenge-facpatbjdxgdb4dz.canadacentral-01.azurewebsites.net/vehicle/VIN

      
