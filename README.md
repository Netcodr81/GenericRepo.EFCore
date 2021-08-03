# GenericRepo.EFCore

## Getting Started
You can install the latest package via Nuget package manager just search for *GenericRepo.EFCore*. You can also install via powershell using the following command.

```powershell
Install-Package GenericRepo.EFCore -Version 1.0.0
```
Or via the donet CLI

```bash
dotnet add package GenericRepo.EFCore --version 1.0.0
```

## Using the package

1. Create a DbContext and its entities

```csharp
  public class GenericDbContext : DbContext
    {
        public GenericDbContext()
        {
        }

        public GenericDbContext(DbContextOptions<GenericDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
}
```
2. Create a repository class that inherits GenericRepository<TEntity, TDbContext>

```csharp
 public class CarsRepository : GenericRepository<Car, GenericDbContext>
    {
        public CarsRepository(GenericDbContext dataContext) : base(dataContext)
        {

        }
    }
```
Thats it! This generic repository will give you access to the following generic methods:

- Get(object id)
- GetAsync(object id)
- Get(filter, orderBy, includedProperties)
- GetAsync(filter, orderBy, includedProperties)
- GetAll
- GetAllAsync
- Delete
- DeleteAsync
- Insert
- InsertAsync
- Update
- UpdateAsync
