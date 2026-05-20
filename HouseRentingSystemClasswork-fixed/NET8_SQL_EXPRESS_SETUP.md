# .NET 8 SQL Express setup

This version uses:

```json
"Server=DESKTOP-6E804HH\\SQLEXPRESS;Database=HouseRentingSystem;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
```

Run from the solution folder:

```powershell
dotnet restore
dotnet ef database update --project .\House_renting_system-Project.Data --startup-project .\House_renting_system-Project
dotnet run --project .\House_renting_system-Project
```

If the database exists but is broken:

```powershell
dotnet ef database drop --force --project .\House_renting_system-Project.Data --startup-project .\House_renting_system-Project
dotnet ef database update --project .\House_renting_system-Project.Data --startup-project .\House_renting_system-Project
```
