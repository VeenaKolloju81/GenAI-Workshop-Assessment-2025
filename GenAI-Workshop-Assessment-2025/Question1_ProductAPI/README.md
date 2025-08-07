# Product RESTful API (ASP.NET Core 8)

This project provides a simple RESTful API for managing "Product" entities using ASP.NET Core 8 Web API. It demonstrates CRUD operations, input validation, error handling, and proper HTTP status codes using an in-memory list for storage.

## Features

- **CRUD Operations:** Create, Read, Update, Delete products
- **Input Validation:** Ensures required fields and valid data
- **Error Handling:** Returns meaningful error messages and status codes
- **HTTP Status Codes:** Uses 200, 201, 204, 400, 404, 500 as appropriate
- **Async Methods:** All endpoints are asynchronous

## Product Model

- `Id` (int): Product identifier (auto-incremented)
- `Name` (string): Required
- `Description` (string): Optional
- `Price` (decimal): Required, must be greater than zero

## API Endpoints

| Method | Endpoint           | Description                |
|--------|--------------------|----------------------------|
| GET    | `/api/Product`     | Get all products           |
| GET    | `/api/Product/{id}`| Get product by ID          |
| POST   | `/api/Product`     | Create a new product       |
| PUT    | `/api/Product/{id}`| Update an existing product |
| DELETE | `/api/Product/{id}`| Delete a product           |

## Example Request (POST)

```json
POST /api/Product
Content-Type: application/json

{
  "name": "Sample Product",
  "description": "A sample item",
  "price": 19.99
}
```

## Running the Project

1. **Clone the repository**
2. **Open in Visual Studio or VS Code**
3. **Build and run the project**
4. **Use tools like Postman or curl to test the endpoints**

## Notes

- This API uses an in-memory list for demonstration and does not persist data between runs.
- No database setup is required.

---
