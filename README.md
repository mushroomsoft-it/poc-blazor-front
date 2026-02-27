# Blazor UI Component Libraries - POC Comparison

This repository contains proof-of-concept implementations comparing different UI component libraries for Blazor WebAssembly applications. Each POC implements the same features (dashboard, products CRUD, tasks CRUD, and component showcase) using a different component library.

## POCs Included

| POC | Component Library | Design System | .NET Version |
|-----|------------------|---------------|--------------|
| [Bootstrap-POC](#bootstrap-poc) | Bootstrap 5 (CDN) | Bootstrap | .NET 10.0 |
| [MudBlazor-POC](#mudblazor-poc) | MudBlazor 8.15.0 | Material Design | .NET 9.0 |
| [Radzen-POC](#radzen-poc) | Radzen.Blazor 6.0.0 | Custom | .NET 10.0 |
| [Blazorise-POC](#blazorise-poc) | Blazorise 1.8.8 | Bootstrap 5 | .NET 10.0 |
| [FluentUI-POC](#fluentui-poc) | FluentUI 4.13.2 | Microsoft Fluent | .NET 10.0 |

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (for MudBlazor-POC)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (for all other POCs)

## Running Each POC

### Bootstrap-POC

```bash
cd Bootstrap-POC
dotnet run
```

Navigate to `https://localhost:5001` or the URL shown in the terminal.

### MudBlazor-POC

```bash
cd MudBlazor-POC
dotnet run
```

Navigate to `https://localhost:5001` or the URL shown in the terminal.

### Radzen-POC

```bash
cd Radzen-POC
dotnet run
```

Navigate to `https://localhost:5001` or the URL shown in the terminal.

### Blazorise-POC

```bash
cd Blazorise-POC/Blazorise-POC
dotnet run
```

Navigate to `https://localhost:5001` or the URL shown in the terminal.

### FluentUI-POC

```bash
cd FluentUI-POC/FluentUI-POC
dotnet run
```

Navigate to `https://localhost:5001` or the URL shown in the terminal.

### PocSyncfusion

```bash
cd PocSyncfusion
dotnet run
```

Navigate to `https://localhost:5184` or the URL shown in the terminal.

### PocTeeChart

```bash
cd PocTeeChart
dotnet run
```

Navigate to `https://localhost:5184` or the URL shown in the terminal.

### PocVegaLite

```bash
cd PocVegaLite
dotnet run
```

Navigate to `https://localhost:5184` or the URL shown in the terminal.

## Features Implemented in Each POC

- **Dashboard**: Statistics overview with cards
- **Products CRUD**: Full create, read, update, delete operations
- **Tasks CRUD**: Task management with status and priority
- **Component Showcase**: Demonstration of library components (buttons, forms, alerts, modals, etc.)

## Comparison

### Bootstrap-POC

**Pros:**
- Familiar Bootstrap syntax and styling
- No additional NuGet packages needed (CDN-based)
- Lightweight - uses standard HTML/CSS
- Large community and extensive documentation
- Easy to customize with standard CSS

**Cons:**
- No native Blazor components - manual implementation required
- Less interactive features out of the box
- More manual work for complex components (DataGrids, etc.)

**Best for:** Teams familiar with Bootstrap, projects needing lightweight solutions, or when full control over markup is required.

---

### MudBlazor-POC

**Pros:**
- Beautiful Material Design aesthetics
- Rich set of pre-built components
- Excellent documentation
- Strong community support
- Built-in theming system
- Good DataGrid component

**Cons:**
- Material Design may not fit all project aesthetics
- Larger bundle size compared to Bootstrap
- Learning curve for Material Design patterns

**Best for:** Applications wanting modern Material Design look, internal tools, dashboards.

---

### Radzen-POC

**Pros:**
- Comprehensive component library (70+ components)
- Professional-grade DataGrid with advanced features
- Built-in form validation
- Good performance
- Free tier available

**Cons:**
- Some advanced features require paid license
- Less community content compared to MudBlazor
- Custom design system may require more theming work

**Best for:** Enterprise applications, data-heavy dashboards, when advanced grid features are needed.

---

### Blazorise-POC

**Pros:**
- Framework-agnostic (supports Bootstrap, Bulma, Material, etc.)
- Familiar Bootstrap-like API
- Powerful DataGrid component
- Good documentation
- Flexible theming

**Cons:**
- Requires multiple packages for full functionality
- Can be complex to configure initially
- Slightly steeper learning curve

**Best for:** Teams wanting Bootstrap styling with rich Blazor components, projects that may switch CSS frameworks.

---

### FluentUI-POC

**Pros:**
- Official Microsoft component library
- Modern Fluent Design System
- Consistent with Microsoft products (Teams, Office)
- Good accessibility support
- Active development by Microsoft

**Cons:**
- Microsoft-specific aesthetic may not fit all projects
- Smaller community compared to MudBlazor
- Some components still maturing

**Best for:** Enterprise applications, Microsoft ecosystem projects, applications needing Microsoft product consistency.

---

## Quick Comparison Table

| Feature | Bootstrap | MudBlazor | Radzen | Blazorise | FluentUI |
|---------|-----------|-----------|--------|-----------|----------|
| **Bundle Size** | Small | Medium | Medium | Medium | Medium |
| **Components** | Manual | 70+ | 70+ | 80+ | 50+ |
| **DataGrid** | Manual | Built-in | Advanced | Built-in | Basic |
| **Theming** | CSS | Built-in | Built-in | Built-in | Built-in |
| **Learning Curve** | Low | Medium | Medium | Medium | Medium |
| **Documentation** | External | Excellent | Good | Good | Good |
| **License** | MIT | MIT | MIT/Commercial | MIT | MIT |
| **Design System** | Bootstrap | Material | Custom | Flexible | Fluent |

## Project Structure

All POCs follow a similar structure:

```
[POC-Name]/
├── Layout/
│   ├── MainLayout.razor
│   └── NavMenu.razor
├── Models/
│   ├── Product.cs
│   └── Task.cs
├── Pages/
│   ├── Home.razor
│   ├── Products.razor
│   ├── ProductForm.razor
│   ├── Tasks.razor
│   ├── TaskForm.razor
│   └── Components.razor
├── Services/
│   ├── ProductService.cs
│   ├── TaskService.cs
│   └── StatisticsService.cs
├── wwwroot/
│   └── index.html
├── App.razor
├── Program.cs
└── [POC-Name].csproj
```

## License

This project is for evaluation and comparison purposes.
