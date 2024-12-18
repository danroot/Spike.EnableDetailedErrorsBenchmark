# Enable Detailed Errors Benchmark
This benchmark demonstrates the performance difference between an EFCore context with and without using ["EnableDetailedErrors()"](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptionsbuilder.enabledetailederrors?view=efcore-9.0). See [Consider making errors always detailed #23038
](https://github.com/dotnet/efcore/issues/23038)

## Prereqs:
This uses [TestContainers](https://testcontainers.com/) to launch SQL Server.  As-is you will need Docker installed on the machine running the benchmark.  This could also affect performance, so may be a factor in understanding the results.  Allowing for scenarios using a localdb or other SQL instance should be considered.

## Usage: 
`dotnet run -c Release`

On MacOS, you may need to run with 'sudo' to allow priority threads, otherwise there will be a warning from BenchmarkDotNet:

`sudo dotnet run -c Release`


## Results:
```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.2.1 (23C71) [Darwin 23.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                           | NumberOfRows | Mean        | Error     | StdDev    | Median      | Gen0      | Gen1     | Gen2     | Allocated   |
|--------------------------------- |------------- |------------:|----------:|----------:|------------:|----------:|---------:|---------:|------------:|
| **QueryWithEnableDetailedErrors**    | **10**           |    **639.0 μs** |  **12.70 μs** |  **17.80 μs** |    **636.4 μs** |    **2.9297** |        **-** |        **-** |    **19.99 KB** |
| QueryWithoutEnableDetailedErrors | 10           |    707.6 μs |  25.00 μs |  72.14 μs |    677.3 μs |    2.9297 |        - |        - |    19.99 KB |
| **QueryWithEnableDetailedErrors**    | **1000**         |  **2,121.8 μs** |  **41.27 μs** |  **36.59 μs** |  **2,122.1 μs** |  **191.4063** |  **70.3125** |        **-** |  **1189.69 KB** |
| QueryWithoutEnableDetailedErrors | 1000         |  2,090.0 μs |  41.21 μs |  76.38 μs |  2,057.2 μs |  191.4063 |  62.5000 |        - |  1189.76 KB |
| **QueryWithEnableDetailedErrors**    | **10000**        | **25,699.0 μs** | **507.43 μs** | **774.91 μs** | **25,419.8 μs** | **2062.5000** | **906.2500** | **281.2500** | **12018.03 KB** |
| QueryWithoutEnableDetailedErrors | 10000        | 25,959.9 μs | 515.26 μs | 786.86 μs | 25,700.2 μs | 2062.5000 | 906.2500 | 281.2500 | 12018.03 KB |
