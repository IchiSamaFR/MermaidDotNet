# MermaidSharp
[![NuGet Version](https://img.shields.io/nuget/v/MermaidSharp)](https://www.nuget.org/packages/MermaidSharp)

> **Info:** This project is a fork of the original [MermaidDotNet](https://github.com/samsmithnz/MermaidDotNet) repository. All enhancements, fixes, and additions are based on the initial project.


MermaidSharp is a comprehensive .NET wrapper to create [Mermaid](https://mermaid.js.org/) diagrams with full syntax support, including flowcharts, entity relationship diagrams, class diagrams, git graphs, pie charts, quadrant charts, and XY charts. Diagrams can be inserted into markdown or directly displayed in HTML with mermaid.js.


## Features

- Flowcharts, Entity Relationship Diagrams, Class Diagrams
- Git Graphs, Pie Charts, Quadrant Charts, XY Charts
- Full Mermaid syntax support
- .NET 4.8, .NET 8, .NET 9 sample apps (MVC, Blazor, WebAssembly)

See the [Features](../wiki/Features) page for a complete list.


## Getting Started

Install the NuGet package:

```
dotnet add package MermaidSharp
```

For advanced usage, prerequisites, and build instructions, see [Getting-Started](../wiki/Getting-Started).

## Quick Example

```csharp
using MermaidSharp.Diagrams;

var diagram = new FlowchartDiagram("LR");
diagram.Nodes.Add(new FlowNode("a", "Start"));
diagram.Nodes.Add(new FlowNode("b", "End"));
diagram.Links.Add(new FlowLink("a", "b"));

Console.WriteLine(diagram.CalculateDiagram());
// Output:
// flowchart LR
//     a[Start]
//     b[End]
//     a-->b
```

For complete examples of all diagram types, see [Basic-Usage](../wiki/Basic-Usage).

## EntityFramework Integration

Auto-generate ERDs from your `DbContext`:

```csharp
using MermaidSharp.EntityFrameworkCore;
var diagram = myDbContext.ToMermaidEntityDiagram();
Console.WriteLine(diagram.CalculateDiagram());
```

See [EntityFramework-Integration](../wiki/EntityFramework-Integration) for options and details.

## AutoDiagram (Reflection)

Generate diagrams automatically from .NET types using reflection:

```csharp
using MermaidSharp.AutoDiagram.Diagrams;
var diagram = typeof(MyClass).ToMermaidClassDiagram();
Console.WriteLine(diagram.CalculateDiagram());
```

See [AutoDiagram](../wiki/AutoDiagram) for more options and flowchart generation.

## Wiki & Documentation

- [Getting Started](../wiki/Getting-Started) — Prerequisites, installation, build instructions
- [Features](../wiki/Features) — Overview of all supported diagram types
- [Basic Usage](../wiki/Basic-Usage) — Complete examples for each diagram type
- [AutoDiagram](../wiki/AutoDiagram) — Auto-generate diagrams from .NET types using reflection
- [EntityFramework Integration](../wiki/EntityFramework-Integration) — Auto-generate ERDs from a `DbContext`
- [Sample Projects](../wiki/Sample-Projects) — MVC and Blazor WebAssembly sample applications
- [Contributing](../wiki/Contributing) — Coding standards, testing, and contribution guidelines
