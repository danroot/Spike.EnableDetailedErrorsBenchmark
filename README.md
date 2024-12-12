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
| Method                           | NumberOfRows | Mean        | Error     | StdDev      | Median      |
|--------------------------------- |------------- |------------:|----------:|------------:|------------:|
| **QueryWithEnableDetailedErrors**    | **10**           |    **759.2 μs** |  **14.47 μs** |    **14.21 μs** |    **757.2 μs** |
| QueryWithoutEnableDetailedErrors | 10           |    124.2 μs |   2.70 μs |     7.78 μs |    123.3 μs |
| **QueryWithEnableDetailedErrors**    | **1000**         |  **2,432.8 μs** |  **48.16 μs** |   **132.64 μs** |  **2,402.4 μs** |
| QueryWithoutEnableDetailedErrors | 1000         |    117.9 μs |   2.14 μs |     4.83 μs |    116.0 μs |
| **QueryWithEnableDetailedErrors**    | **10000**        | **25,883.5 μs** | **477.76 μs** | **1,048.71 μs** | **25,518.5 μs** |
| QueryWithoutEnableDetailedErrors | 10000        |    140.9 μs |   2.81 μs |     6.28 μs |    138.7 μs |