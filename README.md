# Profile API - HNG13 Stage 0

A simple .NET Web API that returns profile information along with a dynamic cat fact fetched from an external API.

## Features

- RESTful GET endpoint at `/me`
- Returns profile information with dynamic timestamps
- Integrates with Cat Facts API for random cat facts
- Error handling with fallback messages
- CORS enabled for development

## Tech Stack

- .NET 9.0 (or compatible version)
- ASP.NET Core Minimal APIs
- C#

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)

## Installation

1. Clone the repository:
```bash
git clone https://github.com/herdeybayor/hng13-stage0-backend.git
cd hng13-stage0-backend
```

2. Restore dependencies:
```bash
dotnet restore
```

## Running Locally

1. Start the application:
```bash
dotnet run
```

2. The API will be available at `http://localhost:5131` (or the port shown in your terminal)

3. Test the endpoint:
```bash
curl http://localhost:5131/me
```

## API Documentation

### GET /me

Returns profile information with a random cat fact.

**Response Format:**
```json
{
  "status": "success",
  "user": {
    "email": "herdeybayor4real@gmail.com",
    "name": "Sherifdeen Adebayo",
    "stack": ".NET/ASP.NET Core"
  },
  "timestamp": "2025-10-17T12:34:56.789Z",
  "fact": "Random cat fact from Cat Facts API"
}
```

**Response Fields:**
- `status`: Always returns "success"
- `user.email`: Profile email address
- `user.name`: Full name
- `user.stack`: Backend technology stack
- `timestamp`: Current UTC time in ISO 8601 format
- `fact`: Random cat fact from https://catfact.ninja/fact

**Status Codes:**
- `200 OK`: Successful request
- `Content-Type`: `application/json; charset=utf-8`

## Project Structure

```
.
├── Program.cs              # Main application file with endpoint logic
├── ProfileApi.csproj       # Project configuration
├── appsettings.json        # Application settings
├── Properties/
│   └── launchSettings.json # Launch configuration
└── README.md              # This file
```

## Deployment
This project is deployed on PXXL App.

## Testing

Run the application and test with curl:

```bash
# Test the endpoint
curl http://localhost:5131/me

# Test with formatted output (requires jq)
curl http://localhost:5131/me | jq .

# Check headers
curl -I http://localhost:5131/me
```

## Error Handling

- If the Cat Facts API is unavailable, a fallback message is returned
- API calls timeout after 5 seconds
- Errors are logged to the console for debugging

## License

This project is part of the HNG13 Backend Developer Track.
