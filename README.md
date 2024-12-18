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
| Method                           | NumberOfRows | Mean        | Error     | StdDev      | Median      | Gen0      | Gen1     | Gen2     | Allocated   |
|--------------------------------- |------------- |------------:|----------:|------------:|------------:|----------:|---------:|---------:|------------:|
| **QueryWithEnableDetailedErrors**    | **10**           |    **753.1 μs** |  **39.10 μs** |   **114.06 μs** |    **714.8 μs** |    **2.9297** |        **-** |        **-** |    **19.99 KB** |
| QueryWithoutEnableDetailedErrors | 10           |    721.4 μs |  25.04 μs |    72.24 μs |    700.8 μs |    1.9531 |        - |        - |    19.87 KB |
| **QueryWithEnableDetailedErrors**    | **1000**         |  **2,315.4 μs** |  **65.92 μs** |   **193.32 μs** |  **2,258.6 μs** |  **191.4063** |  **62.5000** |        **-** |  **1189.73 KB** |
| QueryWithoutEnableDetailedErrors | 1000         |  2,437.9 μs |  73.21 μs |   212.39 μs |  2,342.0 μs |  191.4063 |  62.5000 |        - |  1189.61 KB |
| **QueryWithEnableDetailedErrors**    | **10000**        | **25,745.1 μs** | **508.36 μs** |   **890.35 μs** | **25,346.5 μs** | **2062.5000** | **906.2500** | **281.2500** | **12017.91 KB** |
| QueryWithoutEnableDetailedErrors | 10000        | 26,711.4 μs | 531.04 μs | 1,435.69 μs | 26,383.3 μs | 2062.5000 | 906.2500 | 281.2500 | 12017.19 KB |
