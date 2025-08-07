# question
1.RESTful API Generation
Use Case: Create a RESTful API controller/Screen for a "Product" entity in a web application. 

Requirement:
Generate a controller/Screen with CRUD operations/buttons (GET, POST, PUT, DELETE) 
Include proper HTTP status codes and response handling 
Add input validation and error handling 
Include appropriate HTTP method annotations 
Support both individual item and collection operations 

Evaluation Points: 
Prompt clarity and specificity 
Complete CRUD implementation 
Proper HTTP conventions 
Error handling implementation 


# Prompts

# GenAI Workshop Assessment 2025
1.Create a C# class Product with Id, Name, Description, and Price properties.

2.Create an ASP.NET Core API controller named ProductController with CRUD methods (GET all products, GET product by ID, POST to create a product, PUT to update a product, DELETE to delete a product). Use an in-memory list to store products. Include proper HTTP method annotations and return appropriate HTTP status codes.

3.Add input validation in the ProductController so that POST and PUT requests check that the product’s Name is not empty and Price is greater than zero. Return 400 Bad Request with a meaningful message if validation fails. Also, handle cases where a product is not found and return 404 Not Found. Wrap methods in try-catch blocks to handle unexpected errors and return 500 Internal Server Error if exceptions occur.

4.Convert all CRUD methods in ProductController to async methods using Task<IActionResult>. Use async/await when accessing the in-memory product list or database. Ensure that all HTTP responses and error handling are preserved in the async versions.

# commands used
dotnet new webapi -n GenAI-Workshop-Assessment-2025 --use-controllers


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
