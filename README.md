# LogicBuilder.App.KendoGrid.Bsl.Business

[![CI](https://github.com/BpsLogicBuilder/LogicBuilder.App.KendoGrid.Bsl.Business/actions/workflows/ci.yml/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.App.KendoGrid.Bsl.Business/actions/workflows/ci.yml)
[![CodeQL](https://github.com/BpsLogicBuilder/LogicBuilder.App.KendoGrid.Bsl.Business/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.App.KendoGrid.Bsl.Business/actions/workflows/github-code-scanning/codeql)
[![codecov](https://codecov.io/gh/BpsLogicBuilder/LogicBuilder.App.KendoGrid.Bsl.Business/graph/badge.svg?token=08HT7RIG9A)](https://codecov.io/gh/BpsLogicBuilder/LogicBuilder.App.KendoGrid.Bsl.Business)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=BpsLogicBuilder_LogicBuilder.App.KendoGrid.Bsl.Business&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=BpsLogicBuilder_LogicBuilder.App.KendoGrid.Bsl.Business)
[![NuGet](https://img.shields.io/nuget/v/LogicBuilder.App.KendoGrid.Bsl.Business.svg)](https://www.nuget.org/packages/LogicBuilder.App.KendoGrid.Bsl.Business)


A .NET Standard 2.0 library that provides request model structures for integrating LogicBuilder services with Kendo UI Grid components. This library defines the data contracts used to communicate grid data operations such as filtering, sorting, paging, grouping, and aggregation between client applications and server-side business logic layers.

## Overview

This library is part of the LogicBuilder ecosystem and serves as a bridge between Kendo UI Grid data sources and LogicBuilder-based services. It provides strongly-typed request models that encapsulate all the necessary information to retrieve and manipulate grid data on the server side.

## Key Features

- **Strongly-typed request models** for Kendo UI Grid data operations
- **JSON serialization support** with `System.Text.Json`
- **Flexible data querying** with support for filtering, sorting, paging, grouping, and aggregation
- **SelectExpand functionality** integration via `LogicBuilder.Structures`
- **.NET Standard 2.0 compatibility** for broad platform support

## Main Components

### KendoGridDataRequest

The primary request model that encapsulates a complete grid data request:
```c#
	public class KendoGridDataRequest 
	{ 
		public string? DataType { get; set; } 
		public string? ModelType { get; set; } 
		public KendoGridDataSourceRequestOptions? Options { get; set; } 
		public SelectExpandDefinitionDescriptor? SelectExpandDefinition { get; set; } 
	}
```

- **DataType**: Specifies the entity type being requested
- **ModelType**: Defines the view model type for the response
- **Options**: Contains grid operation parameters (filtering, sorting, paging, etc.)
- **SelectExpandDefinition**: Defines which related data to include in the response

### KendoGridDataSourceRequestOptions

Defines the data source operations for the grid:
```c#
	public class KendoGridDataSourceRequestOptions 
	{ 
		public string? Aggregate { get; set; } 
		public string? Filter { get; set; } 
		public string? Group { get; set; } 
		public int Page { get; set; } 
		public string? Sort { get; set; } 
		public int PageSize { get; set; } 
	}
```

## Dependencies

- **LogicBuilder.Structures** (v8.0.2): Provides SelectExpand descriptors and expression utilities
- **.NET Standard 2.0**: Ensures compatibility across .NET Framework, .NET Core, and modern .NET

## Testing

The library includes comprehensive unit tests covering:
- JSON serialization and deserialization
- Round-trip data integrity
- Case-insensitive property name handling
- Special character support in filter expressions
- Null and missing property handling
- Default value behavior

## License

This project is licensed under the MIT License.

## Related Projects

- [LogicBuilder](https://github.com/BpsLogicBuilder/LogicBuilder) - The main LogicBuilder project
- [LogicBuilder.Structures](https://www.nuget.org/packages/LogicBuilder.Structures) - Core structures and utilities

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

## Copyright

Copyright © BPS 2026

