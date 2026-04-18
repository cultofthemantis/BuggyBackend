# Library System API - Bug Documentation

This is a buggy Library Management System API built with .NET and n-tier architecture. The system contains **20 intentional bugs** across different layers (Models, Repositories, Services, Controllers, and Configuration).

## Architecture

The application follows n-tier architecture:
- **Models Layer**: Data models (Book, Member, Transaction)
- **Repository Layer**: Data access logic
- **Service Layer**: Business logic
- **Controller Layer**: API endpoints
- **Program.cs**: Dependency injection configuration

---

## Please List all of the bugs you find & where you found it


### EXMPLE **Bug #1: Wrong HTTP Request method in MemberController**
**Location**: `Controllers/MemberController.cs` - Line 38  
**Type**: API Design Error  

**Bug**:
```csharp
    [HttpDelete("{id}")]
    public ActionResult<Member> Update(int id, [FromBody] Member member)
```


Bug #1: Swagger dependency version conflict

Location: Project dependencies (.csproj)
Type: Runtime / Dependency Error

Bug:

System.TypeLoadException:
Method 'GetSwagger' ... does not have an implementation.
Could not load type 'Microsoft.OpenApi.Models.OpenApiDocument'

Cause:
Incompatible versions of Swagger-related packages caused missing method implementations at runtime.

Fix:

dotnet remove package Swashbuckle.AspNetCore
dotnet remove package Microsoft.OpenApi
dotnet add package Swashbuckle.AspNetCore --version 6.5.0



Bug #2: Missing dependency injection configuration

Location: Program.cs
Type: Backend Architecture Error

Bug:

Unable to resolve service for type 'IBookRepository'

Cause:
Required services and repositories were not registered in the dependency injection container.

Fix:

builder.Services.AddScoped<IBookRepository, BookRepository>();



Bug #3: Possible null reference return in repositories

Location: Repositories/BookRepository.cs (and similar files)
Type: Null Safety Warning

Bug:

return _context.Books.FirstOrDefault(b => b.Id == id);

Error:

CS8603: Possible null reference return

Cause:
Method allows returning null but the return type is non-nullable.

Fix:

public Book? GetById(int id)
{
    return _context.Books.FirstOrDefault(b => b.Id == id);
}




Bug #4
public ActionResult<Book> NewMethod(Book book)

Location: Controllers/BooksController.cs Line 63

Fix: private ActionResult<Book> NewMethod(Book book)
    changed public to private


Bug #5

Location: Program.cs lines 11-13


Bug:

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

Fix:
builder.Services.AddSingleton<IMemberRepository, MemberRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();



Changed services from scoped to singleton so the data persists



Bug #6-7
CORS Policy fix in Program.cs, added AllowAnyOrigin() and used CORS policy at the bottom













