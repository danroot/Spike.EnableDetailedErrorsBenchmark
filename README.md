# Enable Detailed Errors Benchmark
This benchmark demonstrates the performance difference between an EFCore context with and without using ["EnableDetailedErrors()"](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptionsbuilder.enabledetailederrors?view=efcore-9.0). See [Consider making errors always detailed #23038
](https://github.com/dotnet/efcore/issues/23038)

## Prereqs:
This uses [TestContainers](https://testcontainers.com/) to launch SQL Server.  As-is you will need Docker installed on the machine running the benchmark.  This could also affect performance, so may be a factor in understanding the results.  Allowing for scenarios using a localdb or other SQL instance should be considered.

## Usage: 
`dotnet run -c Release`

## Results:
```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.2.1 (23C71) [Darwin 23.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                           | NumberOfRows | Mean        | Error     | StdDev    | Median      |
|--------------------------------- |------------- |------------:|----------:|----------:|------------:|
| **QueryWithEnableDetailedErrors**    | **10**           |    **761.4 μs** |  **14.67 μs** |  **16.30 μs** |    **755.3 μs** |
| QueryWithoutEnableDetailedErrors | 10           |    751.7 μs |  14.06 μs |  12.46 μs |    755.0 μs |
| **QueryWithEnableDetailedErrors**    | **1000**         |  **2,411.1 μs** |  **43.63 μs** |  **81.96 μs** |  **2,389.6 μs** |
| QueryWithoutEnableDetailedErrors | 1000         |  2,380.7 μs |  47.38 μs | 125.66 μs |  2,333.3 μs |
| **QueryWithEnableDetailedErrors**    | **10000**        | **26,925.7 μs** | **522.73 μs** | **697.83 μs** | **27,012.7 μs** |
| QueryWithoutEnableDetailedErrors | 10000        | 27,085.7 μs | 529.46 μs | 724.73 μs | 26,751.9 μs |
