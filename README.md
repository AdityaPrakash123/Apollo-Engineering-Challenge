# Apollo Engineering Challenge


# Overview
This project implements a web service with CRUD-style API access to a vehicle database. The service is built to handle vehicle data, identified uniquely by VINs (Vehicle Identification Numbers), and is capable of responding with appropriate HTTP status codes and JSON-formatted responses.

# API Endpoints


# 1. Get All Vehicles
* Method: GET
* URI: /vehicle
* Response: JSON array of all vehicle records
* Status Code: 200 OK
# 2. Create a Vehicle
* Method: POST
* URI: /vehicle
* Request Body: JSON object representing the new vehicle
* Response: JSON object of the created vehicle
* Status Code: 201 Created
# 3. Get a Vehicle by VIN
* Method: GET
* URI: /vehicle/{vin}
* Response: JSON object of the specified vehicle
* Status Code: 200 OK
# 4. Update a Vehicle by VIN
* Method: PUT
* URI: /vehicle/{vin}
* Request Body: JSON object representing the updated vehicle
* Response: JSON object of the updated vehicle
* Status Code: 200 OK
# 5. Delete a Vehicle by VIN
* Method: DELETE
* URI: /vehicle/{vin}
* Response: No content
* Status Code: 204 No Content
# Error Handling
* 400 Bad Request: Returned when the server cannot parse the JSON input.
* 422 Unprocessable Entity: Returned when the JSON input is valid but fails validation due to malformed or null attributes.

# Installation and Build Instructions
1) Install Dependencies: Run the following command to install required dependencies:
   * dotnet restore
2) Build the Project:
   * dotnet build
3) Run the Application:
   * dotnet run
4) dotnet test

# Add Migrations

* dotnet ef migrations add Initial-Migrations

# Apply the Migration

* dotnet ef database update




curl http://localhost:5196/vehicle

curl -X POST http://localhost:5196/vehicle -H "Content-Type: application/json" -d '{
  "VIN": "1HGBH41JXMN109186",
  "ManufacturerName": "Toyota",
  "Description": "Compact sedan",
  "HorsePower": "130",
  "ModelName": "Corolla",
  "ModelYear": 2021,
  "PurchasePrice": 20000,
  "FuelType": "Petrol"
}'

curl -X PUT http://localhost:5196/vehicle/1HGBH41JXMN109186 -H "Content-Type: application/json" -d '{
  "ManufacturerName": "Toyota Updated",
  "Description": "Updated sedan",
  "HorsePower": "140",
  "ModelName": "Corolla Updated",
  "ModelYear": 2022,
  "PurchasePrice": 21000,
  "FuelType": "Hybrid"
}'

curl -X DELETE http://localhost:5196/vehicle/1HGBH41JXMN109186


