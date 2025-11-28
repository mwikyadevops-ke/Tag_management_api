# Tag Management API

A RESTful ASP.NET Core Web API for managing vehicle tags with support for automatic and manual tag creation, image uploads, and tag closure functionality.

## Features

- **Automatic Tags**: System-generated tags (no user ID required)
- **Manual Tags**: User-created tags with user ID
- **Image Upload**: Support for image file uploads via multipart/form-data
- **Image URL**: Support for existing image URLs
- **Tag Closure**: Close tags with reason and user validation
- **Station Validation**: Tags can only be closed at the station where they were created
- **Search & Filter**: Search tags by various criteria
- **Swagger Documentation**: Interactive API documentation
- **PostgreSQL Support**: Production-ready database storage

## Prerequisites

- .NET 10.0 SDK or later
- PostgreSQL database (version 12 or later)
- Visual Studio 2022, VS Code, or Rider (for development)

## db configs

3. Update database connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=case_management;Username=your_username;Password=your_password"
  }
}
```

4. Run database migrations:
```bash
dotnet ef database update
```


```

## API Endpoints
/api/tags
```
### 1. Create Tag (JSON)
Create a tag using JSON payload. Supports both automatic and manual tags.

**Endpoint:** `POST /api/tags/create`  
**Content-Type:** `application/json`

#### Automatic Tag (System-Created)
```json
{
  "userId": null,
  "vehicleReg": "ABC123",
  "reason": "Automatic detection - overloaded",
  "stationId": 1,
  "eventTimeStamp": "2024-01-15T10:30:00Z",
  "imageUrl": "https://example.com/images/tag123.jpg",
  "notes": "Automatic tag created by system"
}
```

#### Manual Tag (User-Created)
```json
{
  "userId": 5,
  "vehicleReg": "XYZ789",
  "reason": "Manual inspection - violation",
  "stationId": 2,
  "eventTimeStamp": "2024-01-15T11:00:00Z",
  "imageUrl": "https://example.com/images/tag456.jpg",
  "notes": "Inspected by officer John Doe"
}
```

**Response:**
```json
{
  "id": 1,
  "tagType": "AUTO",
  "vehicleReg": "ABC123",
  "reason": "Automatic detection - overloaded",
  "userId": null,
  "stationId": 1,
  "imageUrl": "https://example.com/images/tag123.jpg",
  "createdAt": "2024-01-15T10:30:00Z",
  "eventTimestamp": "2024-01-15T10:30:00Z",
  "status": 1,
  "notes": "Automatic tag created by system"
}
```

### 2. Create Tag with File Upload

Create a tag with image file upload support via multipart/form-data.

**Endpoint:** `POST /api/tags/create`  
**Content-Type:** `multipart/form-data`

**Form Fields:**
- `userId` (optional, int): User ID for manual tags. Leave empty/null for automatic tags
- `vehicleReg` (required, string): Vehicle registration number
- `reason` (required, string): Reason for tagging
- `stationId` (required, int): Station ID
- `eventTimeStamp` (optional, datetime): Event timestamp. Defaults to current UTC time
- `imageUrl` (optional, string): Existing image URL (alternative to file upload)
- `notes` (optional, string): Additional notes
- `Image` (optional, file): Image file to upload (jpg, jpeg, png, gif, webp)

**Example using cURL:**
```bash
curl -X POST "https://localhost:5001/api/tags/create" \
  -H "Content-Type: multipart/form-data" \
  -F "vehicleReg=ABC123" \
  -F "reason=Manual inspection" \
  -F "stationId=1" \
  -F "userId=5" \
  -F "Image=@/path/to/image.jpg"
```

### 3. Get All Tags

Retrieve all tags ordered by creation date (newest first).

**Endpoint:** `GET /api/tags`

**Response:**
```json
[
  {
    "id": 1,
    "tagType": "AUTO",
    "vehicleReg": "ABC123",
    "reason": "Automatic detection",
    "userId": null,
    "stationId": 1,
    "status": 1,
    "createdAt": "2024-01-15T10:30:00Z"
  }
]
```

### 4. Search Tags

Search and filter tags by various criteria.

**Endpoint:** `GET /api/tags/search`

**Query Parameters:**
- `vehicleReg` (optional, string): Filter by vehicle registration
- `tagType` (optional, string): Filter by tag type ("AUTO" or "MANUAL")
- `status` (optional, int): Filter by status (0 = closed, 1 = open)
- `stationId` (optional, int): Filter by station ID
- `userId` (optional, int): Filter by user ID
- `createdFrom` (optional, datetime): Filter tags created from this date
- `createdTo` (optional, datetime): Filter tags created to this date
- `reason` (optional, string): Filter by reason (partial match)

**Example:**
```
GET /api/tags/search?tagType=AUTO&status=1&stationId=1
```

### 5. Close Tag

Close a tag with reason and user information. Tags can only be closed at the station where they were created.

**Endpoint:** `PUT /api/tags/{id}/close`  
**Content-Type:** `application/json`

**Request Body:**
```json
{
  "closeReason": "Issue resolved - vehicle cleared",
  "closedByUserId": 5,
  "closedByStationId": 1,
  "notes": "Additional closing notes"
}
```

**Response:**
```json
{
  "id": 1,
  "tagType": "AUTO",
  "vehicleReg": "ABC123",
  "status": 0,
  "closeReason": "Issue resolved - vehicle cleared",
  "closedByUserId": 5,
  "closedAt": "2024-01-15T14:30:00Z"
}
```

**Validation Rules:**
- Tag must exist
- Tag must not already be closed
- `closedByStationId` must match the tag's `stationId` (tags can only be closed at the station where they were created)

**Error Responses:**
- `404 Not Found`: Tag not found
- `400 Bad Request`: Tag is already closed or invalid data
- `403 Forbidden`: Tag cannot be closed at this station

## Tag Types

### Automatic Tags (AUTO)
- Created by the system automatically
- `userId` is `null`
- `tagType` is set to `"AUTO"`
- No user association

### Manual Tags (MANUAL)
- Created by users manually
- `userId` contains the user ID (value > 0)
- `tagType` is set to `"MANUAL"`
- Associated with the creating user

## Image Handling

The API supports two ways to provide images:

1. **File Upload**: Upload an image file via multipart/form-data
   - Supported formats: jpg, jpeg, png, gif, webp
   - Files are saved to `uploads/tags/` directory
   - Returns a relative URL path

2. **Image URL**: Provide an existing image URL
   - Can be used with both JSON and form-data requests
   - Accepts any valid URL string

## Status Values

- `0` = `closed` - Tag is closed
- `1` = `open` - Tag is open/active

## Error Handling

The API returns standard HTTP status codes:

- `200 OK`: Request successful
- `400 Bad Request`: Validation error or invalid request
- `403 Forbidden`: Station validation failed (closing tag)
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

Error response format:
```json
{
  "error": "Error message",
  "details": "Additional error details (if available)"
}
```
