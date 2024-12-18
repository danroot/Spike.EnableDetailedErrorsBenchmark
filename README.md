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
| Method                           | NumberOfRows | Mean        | Error     | StdDev      | Median      | Gen0      | Gen1     | Gen2     | Allocated   |
|--------------------------------- |------------- |------------:|----------:|------------:|------------:|----------:|---------:|---------:|------------:|
| **QueryWithEnableDetailedErrors**    | **10**           |    **612.1 μs** |  **11.99 μs** |    **16.81 μs** |    **604.9 μs** |    **2.9297** |        **-** |        **-** |    **19.99 KB** |
| QueryWithoutEnableDetailedErrors | 10           |    611.7 μs |  11.28 μs |    23.29 μs |    603.1 μs |    2.9297 |        - |        - |    19.96 KB |
| **QueryWithEnableDetailedErrors**    | **1000**         |  **2,292.0 μs** |  **49.12 μs** |   **144.06 μs** |  **2,220.0 μs** |  **191.4063** |  **62.5000** |        **-** |   **1189.7 KB** |
| QueryWithoutEnableDetailedErrors | 1000         |  2,313.5 μs |  52.19 μs |   153.06 μs |  2,249.9 μs |  191.4063 |  62.5000 |        - |  1189.76 KB |
| **QueryWithEnableDetailedErrors**    | **10000**        | **25,187.3 μs** | **464.28 μs** | **1,009.30 μs** | **24,883.2 μs** | **2000.0000** | **769.2308** | **230.7692** | **12017.51 KB** |
| QueryWithoutEnableDetailedErrors | 10000        | 26,041.1 μs | 513.04 μs |   950.96 μs | 25,802.2 μs | 2062.5000 | 906.2500 | 281.2500 | 12017.98 KB |
