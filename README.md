# Housing-Society-Management-API
A RESTful Web API to manage housing societies, residents, and service requests.

## Overview
This API provides CRUD operations for societies and residents, plus service request tracking with status transitions. It follows a layered architecture with clear separation between API, application services, domain models, and infrastructure (EF Core + PostgreSQL). It follows best coding practices like SOLID, DRY, YAGNI, etc., and OOP principles like abstraction, encapsulation, etc.  

## Features
- Manage societies and residents
- Raise and track service requests
- Filter service requests by status, type, society, or resident
- Swagger UI for API documentation
- Compatible with Postman for testing APIs

## Tech Stack
- ASP.NET Core
- C#
- Entity Framework Core
- Swagger (Swashbuckle)
- .NET 10 target framework
- Postman
- Git and GitHub
- PostgreSQL

## Project Structure
- HousingSociety (API host)
- HousingSociety.Application (services, interfaces)
- HousingSociety.Domain (entities, enums)
- HousingSociety.Infrastructure (EF Core, repositories, migrations)
- HousingSociety.Contracts (DTOs)

## Prerequisites
- .NET 10 SDK (preview)
- PostgreSQL 14+ (or compatible)

## Configuration
Update the connection string in [HousingSociety/appsettings.json](HousingSociety/appsettings.json):

```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Host=localhost;Port=5432;Database=HousingDb;Username=user_name;Password=your_password"
	}
}
```



## Database Setup
Migrations are already included in [HousingSociety.Infrastructure/Migrations](HousingSociety.Infrastructure/Migrations).

Run the migration:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project HousingSociety.Infrastructure --startup-project HousingSociety
```

## Run the API
From the repository root:

```bash
dotnet run --project HousingSociety
```

Swagger UI is available in Development at:

```
http://localhost:5000/swagger
```

Note: the exact port can vary. Check the console output


## API Endpoints
Base path: `/api`

### Societies
- `GET /api/societies`
- `GET /api/societies/{id}`
- `POST /api/societies`
- `PUT /api/societies/{id}`
- `DELETE /api/societies/{id}`

### Residents
- `GET /api/residents`
- `GET /api/residents/{id}`
- `GET /api/societies/{societyId}/residents`
- `POST /api/residents`
- `PUT /api/residents/{id}`
- `DELETE /api/residents/{id}`

### Service Requests
- `GET /api/servicerequests`
	<!-- - Optional query params: `status`, `type`, `societyId`, `residentId` -->
- `GET /api/servicerequests/{id}`
- `GET /api/residents/{residentId}/servicerequests`
- `POST /api/servicerequests`
- `PUT /api/servicerequests/{id}` (updates status)
- `DELETE /api/servicerequests/{id}`

<!-- ## Enums
Service request enums are defined in [HousingSociety.Domain/Enums.cs](HousingSociety.Domain/Enums.cs):

### RequestType
- `Plumbing`
- `Electrical`
- `Security`
- `Other`

### RequestStatus
- `Open`
- `InProgress`
- `Resolved`
- `Closed`
- `Cancelled`

Enum values can be sent as strings or integers in query/body. -->

## Service Request Status Transitions
Valid transitions enforced by the service layer:

- `Open` -> `InProgress` or `Cancelled`
- `InProgress` -> `Resolved` or `Cancelled`
- `Resolved` -> `Closed`

Closed or Cancelled requests cannot transition further.

<!-- ## Sample Requests

Create a society:

```bash
curl -X POST http://localhost:5000/api/societies \
	-H "Content-Type: application/json" \
	-d '{
		"name": "Green Meadows",
		"address": "123 Lake Road",
		"city": "Pune",
		"pincode": "411001"
	}'
```

Create a resident:

```bash
curl -X POST http://localhost:5000/api/residents \
	-H "Content-Type: application/json" \
	-d '{
		"societyId": "00000000-0000-0000-0000-000000000000",
		"fullName": "Aarav Kulkarni",
		"email": "aarav@example.com",
		"phone": "+91-99999-11111",
		"flatNumber": "A-203"
	}'
```

Create a service request:

```bash
curl -X POST http://localhost:5000/api/servicerequests \
	-H "Content-Type: application/json" \
	-d '{
		"residentId": "00000000-0000-0000-0000-000000000000",
		"type": "Plumbing",
		"title": "Bathroom leak",
		"description": "Leak near the sink"
	}'
```

Update a service request status:

```bash
curl -X PUT http://localhost:5000/api/servicerequests/00000000-0000-0000-0000-000000000000 \
	-H "Content-Type: application/json" \
	-d '{ "status": "InProgress" }'
``` -->

## Notes
- Password has been redacted from connection string for security reasons
- Swagger port configuration has been set to different ports but not mentioned here for security reasons

