# GenericRepo.EFCore

## Getting Started
You can install the latest package via Nuget package manager just search for *GenericRepo.EFCore*. You can also install via powershell using the following command.

```powershell
Install-Package GenericRepo.EFCore -Version 2.0.0
```
Or via the donet CLI

```bash
dotnet add package GenericRepo.EFCore --version 2.0.0
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
        public DbSet<Model> Models { get;set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Owner> Owners {get;set; }
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
In version 2.0.0 + the ability to include related properties in the GetAll() and GetAllAsync() methods was added. The use is similar to that of the Get() and GetAsync() methods:

```csharp
public class TestClass {
   var repo = new CarsRepo();
   
   var carQuery = repo.Cars.GetAll(x => x.Owners);

}
```

This is how you can use the Get and GetAsync methods to include properties in version 1.2.0 +:
```csharp
public class TestClass {
   var repo = new CarsRepo();
   
   var carQuery = repo.Get(x => x.OwnerName == "Smith", x => x.Make, x => x.Model);

}
```

In version 1.0.0 you use the Get and GetAsync methods like this:

```csharp
public class TestClass {
   var repo = new CarsRepo();
   
   var carQuery = repo.Get(x => x.OwnerName == "Smith", includedProperties: "Car.Make");

}
```
Thats it! This generic repository will give you access to the following generic methods:

- Get(object id)
- GetAsync(object id)
- Get(filter, includedProperties)
- GetAsync(filter, includedProperties)
- GetAll(includedProperties)
- GetAllAsync(includedProperties)
- Delete
- DeleteAsync
- Insert
- InsertAsync
- Update
- UpdateAsync

*Special note: All methods are marked virtual. If you need to modify them for any reason, just override them*
