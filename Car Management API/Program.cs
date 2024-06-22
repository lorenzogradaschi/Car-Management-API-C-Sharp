using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Models
public class Auto
{
    [Key]
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }

    public Auto(string brand, string model, double price)
    {
        Brand = brand;
        Model = model;
        Price = price;
    }

    public Auto() { }

    public override string ToString()
    {
        return $"Auto [Brand={Brand}, Model={Model}, Price={Price}]";
    }

    public string ToCSV()
    {
        return $"{Brand};{Model};{Price}";
    }
}

public class Customer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public Customer() { }

    public override string ToString()
    {
        return $"Customer [Name={Name}, Email={Email}]";
    }
}

public class Purchase
{
    [Key]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int AutoId { get; set; }
    public DateTime PurchaseDate { get; set; }

    public Purchase(int customerId, int autoId, DateTime purchaseDate)
    {
        CustomerId = customerId;
        AutoId = autoId;
        PurchaseDate = purchaseDate;
    }

    public Purchase() { }

    public override string ToString()
    {
        return $"Purchase [CustomerId={CustomerId}, AutoId={AutoId}, PurchaseDate={PurchaseDate}]";
    }
}

// DbContext
public class CarDbContext : DbContext
{
    public DbSet<Auto> Autos { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
}

// Repositories
public class CarArchive
{
    private readonly CarDbContext _context;

    public CarArchive(CarDbContext context)
    {
        _context = context;
    }

    public async Task<List<Auto>> ReadAllAutosAsync()
    {
        return await _context.Autos.ToListAsync();
    }

    public async Task AddAutoAsync(Auto car)
    {
        _context.Autos.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAutoAsync(Auto car)
    {
        _context.Autos.Remove(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAutoAsync(Auto car)
    {
        _context.Autos.Update(car);
        await _context.SaveChangesAsync();
    }
}

public class CustomerArchive
{
    private readonly CarDbContext _context;

    public CustomerArchive(CarDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> ReadAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }
}

public class PurchaseArchive
{
    private readonly CarDbContext _context;

    public PurchaseArchive(CarDbContext context)
    {
        _context = context;
    }

    public async Task<List<Purchase>> ReadAllPurchasesAsync()
    {
        return await _context.Purchases.ToListAsync();
    }

    public async Task AddPurchaseAsync(Purchase purchase)
    {
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePurchaseAsync(Purchase purchase)
    {
        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePurchaseAsync(Purchase purchase)
    {
        _context.Purchases.Update(purchase);
        await _context.SaveChangesAsync();
    }
}

// Services
public class CarList
{
    private readonly CarArchive _carArchive;

    public CarList(CarArchive carArchive)
    {
        _carArchive = carArchive;
    }

    public async Task Add(Auto newCar)
    {
        await _carArchive.AddAutoAsync(newCar);
    }

    public async Task Delete(Auto car)
    {
        await _carArchive.DeleteAutoAsync(car);
    }

    public async Task<List<Auto>> GetCars()
    {
        return await _carArchive.ReadAllAutosAsync();
    }
}

public class CustomerList
{
    private readonly CustomerArchive _customerArchive;

    public CustomerList(CustomerArchive customerArchive)
    {
        _customerArchive = customerArchive;
    }

    public async Task Add(Customer newCustomer)
    {
        await _customerArchive.AddCustomerAsync(newCustomer);
    }

    public async Task Delete(Customer customer)
    {
        await _customerArchive.DeleteCustomerAsync(customer);
    }

    public async Task<List<Customer>> GetCustomers()
    {
        return await _customerArchive.ReadAllCustomersAsync();
    }
}

public class PurchaseList
{
    private readonly PurchaseArchive _purchaseArchive;

    public PurchaseList(PurchaseArchive purchaseArchive)
    {
        _purchaseArchive = purchaseArchive;
    }

    public async Task Add(Purchase newPurchase)
    {
        await _purchaseArchive.AddPurchaseAsync(newPurchase);
    }

    public async Task Delete(Purchase purchase)
    {
        await _purchaseArchive.DeletePurchaseAsync(purchase);
    }

    public async Task<List<Purchase>> GetPurchases()
    {
        return await _purchaseArchive.ReadAllPurchasesAsync();
    }
}

// Controllers
[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CarList _carList;

    public CarsController(CarList carList)
    {
        _carList = carList;
    }

    [HttpGet]
    public async Task<ActionResult<List<Auto>>> GetAllCars()
    {
        return await _carList.GetCars();
    }

    [HttpPost]
    public async Task<ActionResult> AddCar([FromBody] Auto car)
    {
        await _carList.Add(car);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCar(int id)
    {
        var car = new Auto { Id = id };
        await _carList.Delete(car);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCar([FromBody] Auto car)
    {
        await _carList.Add(car);
        return Ok();
    }
}

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerList _customerList;

    public CustomersController(CustomerList customerList)
    {
        _customerList = customerList;
    }

    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAllCustomers()
    {
        return await _customerList.GetCustomers();
    }

    [HttpPost]
    public async Task<ActionResult> AddCustomer([FromBody] Customer customer)
    {
        await _customerList.Add(customer);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        var customer = new Customer { Id = id };
        await _customerList.Delete(customer);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        await _customerList.Add(customer);
        return Ok();
    }
}

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly PurchaseList _purchaseList;

    public PurchasesController(PurchaseList purchaseList)
    {
        _purchaseList = purchaseList;
    }

    [HttpGet]
    public async Task<ActionResult<List<Purchase>>> GetAllPurchases()
    {
        return await _purchaseList.GetPurchases();
    }

    [HttpPost]
    public async Task<ActionResult> AddPurchase([FromBody] Purchase purchase)
    {
        await _purchaseList.Add(purchase);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePurchase(int id)
    {
        var purchase = new Purchase { Id = id };
        await _purchaseList.Delete(purchase);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdatePurchase([FromBody] Purchase purchase)
    {
        await _purchaseList.Add(purchase);
        return Ok();
    }
}

// Startup
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddDbContext<CarDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<CarArchive>();
        services.AddScoped<CarList>();
        services.AddScoped<CustomerArchive>();
        services.AddScoped<CustomerList>();
        services.AddScoped<PurchaseArchive>();
        services.AddScoped<PurchaseList>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

// Program
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

// Configuration in appsettings.json
/*
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
*/

