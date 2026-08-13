# School Management System

## Features

### Auth
- LoginAsync(LoginDto dto)
- LogoutAsync()

### Settings
- IsDbConnected()
  
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
