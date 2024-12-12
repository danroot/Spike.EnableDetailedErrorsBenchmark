// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DotNet.Testcontainers.Builders;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

 var summary = BenchmarkRunner.Run<EnableDetailedErrorsSqlServerBenchmarks>();


public class EnableDetailedErrorsSqlServerBenchmarks
{

    private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder()
      .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
      .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("/opt/mssql-tools18/bin/sqlcmd", "-C", "-Q", "SELECT 1;"))
      .Build();

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        await msSqlContainer.StartAsync();
        var optionsBuilder = new DbContextOptionsBuilder<BenchmarkDbContext>();
        var options = optionsBuilder.UseSqlServer(msSqlContainer.GetConnectionString()).Options;
        using var db = new BenchmarkDbContext(options);
        await db.Database.EnsureCreatedAsync();
        for (var i = 0; i < NumberOfRows; i++)
            await db.AddAsync(new Customer
            {
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                Address = $"Address{i}",
                City = $"City{i}",
                State = $"State{i}",
                ZipCode = $"ZipCode{i}"
            });

        await db.SaveChangesAsync();

    }

    [GlobalCleanup]
    public async Task GlobalCleanup()
    {
        await msSqlContainer.DisposeAsync();
    }
   
    [Params(10, 1000, 10000)]
    public int NumberOfRows;

    [Benchmark]
    public async Task QueryWithEnableDetailedErrors()
    {
        var optionsBuilder = new DbContextOptionsBuilder<BenchmarkDbContext>();
        var options = optionsBuilder
            .UseSqlServer(msSqlContainer.GetConnectionString())
            .EnableDetailedErrors()
            .Options;
        using var db = new BenchmarkDbContext(options);
        var allCustomers = await db.Customers.ToListAsync();
    }
    [Benchmark]
    public async Task QueryWithoutEnableDetailedErrors()
    {
        var optionsBuilder = new DbContextOptionsBuilder<BenchmarkDbContext>();
        var options = optionsBuilder
            .UseSqlServer(msSqlContainer.GetConnectionString())
            .Options;
        using var db = new BenchmarkDbContext(options);
        var allCustomers = db.Customers.ToListAsync();
    }

}

public class BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(100)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(20)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? ZipCode { get; set; }
}