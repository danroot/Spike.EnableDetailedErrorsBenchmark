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
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                           | NumberOfRows | Mean        | Error     | StdDev    | Median      | Gen0      | Gen1      | Gen2     | Allocated  |
|--------------------------------- |------------- |------------:|----------:|----------:|------------:|----------:|----------:|---------:|-----------:|
| **QueryWithEnableDetailedErrors**    | **10**           |    **875.5 μs** |  **43.67 μs** | **127.40 μs** |    **819.0 μs** |   **13.6719** |         **-** |        **-** |   **83.69 KB** |
| QueryWithoutEnableDetailedErrors | 10           |    777.0 μs |  14.24 μs |  32.14 μs |    766.0 μs |   13.6719 |         - |        - |   90.49 KB |
| **QueryWithEnableDetailedErrors**    | **1000**         |  **2,541.1 μs** |  **49.48 μs** | **119.50 μs** |  **2,495.6 μs** |  **218.7500** |  **101.5625** |        **-** | **1352.99 KB** |
| QueryWithoutEnableDetailedErrors | 1000         |  2,338.4 μs |  42.02 μs |  35.08 μs |  2,332.7 μs |  218.7500 |  105.4688 |        - | 1359.81 KB |
| **QueryWithEnableDetailedErrors**    | **10000**        | **26,644.7 μs** | **501.31 μs** | **823.66 μs** | **26,346.7 μs** | **2266.6667** | **1000.0000** | **400.0000** | **13007.7 KB** |
| QueryWithoutEnableDetailedErrors | 10000        | 27,101.8 μs | 502.74 μs | 721.02 μs | 26,812.7 μs | 2281.2500 | 1093.7500 | 437.5000 | 13013.4 KB |
