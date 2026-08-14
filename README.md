# School Management System
A full-stack school management system built with .NET, React, and SQL Server. Authentication is handled via ASP.NET Identity, with Entity Framework Core as the ORM. The system supports a single Admin role with full control over the application, and demonstrates complete CRUD operations for every entity, with an emphasis on performance-optimized code and database-level filtering.


## API Features

### Auth
- `LoginAsync(LoginDto dto)`: Authenticates the user via ASP.NET Identity, using `SignInManager` to sign in with a cookie.
- `LogoutAsync()`: Signs the user out via ASP.NET Identity's `SignOutManager`.

### Settings
- `IsDbConnected()`: Checks whether the database connection is active.
  
### User 
- `GetCurrentUserAsync()`: Retrieves the current user by getting the user ID from `IHttpContextAccessor`.

### Students
- `GetAllStudentsAsync(PaginationParams p, string? status = null)`: Retrieves a paginated, projected list of students in efficient querying. If status filled it will retrive according to the status .
- `GetAllStudentsCountAsync(string? status = null`: Returns the count of students with in Db Level filtering.  If status filled it will retrive according to the status .
- `GetStudentByIdAsync(int id)`: Retrive Student By Id. Returns StudentDto
- `GetStudentByEmailAsync(string email)`: Retrive Student By Email. Returns StudentDto
- `IsEmailExistsAsync(string email)`: Returns boolean. DB has constains to not repeate the mails
-  `AddStudentAsync(AddStudentDto dto)`: To create the student.
-  `UpdateStudentAsync(UpdateStudentDto)`: To update the student.

### Common
- `Result<T> Pattern`: Wraps success/failure state with data or error details, avoiding exception-based flow.
- `PagedList`: Used to retrieve data from the database efficiently, applying pagination based on `PaginationParams`.

### Data
- `DataSeeder`: Seeds fake data for entities using the Bogus library.
---

## Web Features

### Auth
- `RequireAuth`: Guards authorized routes, preventing unauthenticated users from accessing them.
- `LoginForm`: Takes email, password, and isPersistence, it sends them to the backend for validation. Navigates to the dashboard on success.
- `LogoutButton`: Deletes the auth cookie from storage and navigates to the login page.

### App
- `Header`: Uses a ready-made MUI template. Contains the navigation bar. Valid on large screens.
- `TemporaryDrawer`: Uses a ready-made MUI template. Inherits navigation items from `Header`. Valid on small screens.
- `Footer`: Always stays at the bottom of the page. Contains copyright info.
- `Dashboard`: The initial page shown after a user successfully logs in.

### Error
- `NotFound`: Shown when the user enters an invalid URL, handled via React Router.
- `ErrorPage`: Shown in case of any error scenario, handled via React Router.

---

## Run Database Migration Commands

Run these commands from the **solution root**.

**Add a migration:**

```powershell
dotnet ef migrations add Mig_1 `
  --project .\DataAccess\DataAccess.csproj `
  --startup-project .\API\API.csproj
```

**Apply migrations:**

```powershell
dotnet ef database update `
  --project .\DataAccess\DataAccess.csproj `
  --startup-project .\API\API.csproj
```
