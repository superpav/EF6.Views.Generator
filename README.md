# EF6.Views.Generator

Set of *.exe files to generate EF6 views. Inspired by this [Visual Studio extension](https://github.com/ErikEJ/EntityFramework6PowerTools).

## Prerequisites

* Entity Framework 6 (other versions are not supported)
* User defined `DbContext` should have public modifier

## Parameters

1. `dllPath` - path to a dll with user-defined `DbContext`
2. `contextName` - name of the user defined context
3. `outputPath` - folder where to store generated views

## Examples
### net 4.7.2
```
.\EF6.Views.Generator.NET472.exe dllPath DAL.dll contextName CustomContext outputPath .\Views
```
### net6
```
.\EF6.Views.Generator.NET6.exe dllPath DAL.dll contextName CustomContext outputPath .\Views
```

## Known issues

* Custom data providers (for ex. Npgsql) are not supported