# VehicleReservation

Abstract
The goal of this REST Api implementation is to provide user options, to do CRUD operations for vehicles, as well as to do reservations for specific vehicles.

Tech Stack
Solution was made in .net6 by request with proper tests, and in Onion(Clean) Architecture
Also there is implementation of Mediator pattern (Mediatr) as well as implementation of FluentValidation and AutoMapper.

API Endpoints

Vehicle
GET /Vehicle


POST /Vehicle
Body example: 
{
  "model": "Pegout",
  "maker": "307",
  "uniqueId": "C5"
}


PUT /Vehicle
Body example: 
{
  "model": "Peugeot",
  "maker": "307",
  "uniqueId": "C5"
}

DELETE /Vehicle/{id}
Id: string => Mandatory

Reservation

GET /Reservation

POST/Reservation

Body example:
{
  "startFrom": "2023-01-20T21:38:31.687Z",
  "endTo": "2023-01-20T21:38:31.687Z",
   "vehicleId": "C1"
}

