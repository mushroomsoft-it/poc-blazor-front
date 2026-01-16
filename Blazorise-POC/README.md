# Blazorise POC - Blazor WebAssembly

Proof of Concept de aplicación Blazor WebAssembly utilizando **Blazorise** con Bootstrap 5 como framework de componentes UI.

## Características Implementadas

### 1. Dashboard Interactivo
- Tarjetas de estadísticas con iconos
- Gráficos de productos por categoría
- Visualización de tareas por estado y prioridad
- Indicadores de bajo stock e inventario
- Botones de acciones rápidas

### 2. CRUD Completo de Productos
- **Lista de productos** con DataGrid
  - Paginación (10 productos por página)
  - Búsqueda y filtrado por nombre/categoría
  - Columnas ordenables
  - Badges para estado de stock
- **Formulario crear/editar**
  - Validación de campos
  - Mensajes de error
  - Indicadores visuales de stock bajo
- **Modal de confirmación** para eliminación
- **Notificaciones** de éxito/error

### 3. CRUD Completo de Tareas
- **Lista de tareas** en formato cards
  - Filtrado por estado (Pendiente, En Progreso, Completada)
  - Búsqueda por texto
  - Indicadores de prioridad con colores
  - Checkbox para marcar como completada
  - Alertas de tareas vencidas
- **Formulario crear/editar**
  - Selección de estado y prioridad
  - DatePicker para fecha de vencimiento
  - Vista previa en tiempo real
  - Validación completa
- **Modal de confirmación** para eliminación

### 4. Showcase de Componentes
Demostración completa de componentes Blazorise organizados en tabs:
- **Botones**: Colores, tamaños, estados, iconos
- **Formularios**: Inputs, selects, checkboxes, radios, switches
- **Alertas**: Success, Info, Warning, Error, dismissible
- **Modales**: Simple, centrado, con formulario
- **Cards**: Con imagen, header, footer, colores
- **Badges**: Colores, pills, con iconos
- **Progress bars**: Colores, striped, animated
- **Otros**: Accordion, breadcrumbs, dividers, iconos

### 5. Navegación
- Navbar superior responsive con Bar component
- Links a todas las secciones
- Routing entre páginas

## Tecnologías Utilizadas

- **.NET 10.0**
- **Blazor WebAssembly** (standalone)
- **Blazorise 1.8.8**
  - Blazorise.Bootstrap5
  - Blazorise.Icons.FontAwesome
  - Blazorise.DataGrid
- **Bootstrap 5.3.0** (CDN)
- **Font Awesome 6.4.0** (CDN)

## Estructura del Proyecto

```
Blazorise-POC/
├── Layout/
│   └── MainLayout.razor          # Layout principal con navbar
├── Models/
│   ├── Producto.cs                # Modelo de producto
│   └── Tarea.cs                   # Modelo de tarea con enums
├── Pages/
│   ├── Home.razor                 # Dashboard principal
│   ├── Productos.razor            # Lista de productos
│   ├── ProductoForm.razor         # Formulario de producto
│   ├── Tareas.razor               # Lista de tareas
│   ├── TareaForm.razor            # Formulario de tarea
│   └── Componentes.razor          # Showcase de componentes
├── Services/
│   ├── ProductoService.cs         # Servicio CRUD productos
│   ├── TareaService.cs            # Servicio CRUD tareas
│   └── EstadisticasService.cs     # Servicio de estadísticas
├── Program.cs                     # Configuración de servicios
└── _Imports.razor                 # Imports globales
```

## Instrucciones de Ejecución

### Prerrequisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Pasos

1. **Navegar al directorio del proyecto:**
   ```bash
   cd Blazorise-POC/Blazorise-POC
   ```

2. **Restaurar dependencias:**
   ```bash
   dotnet restore
   ```

3. **Compilar el proyecto:**
   ```bash
   dotnet build
   ```

4. **Ejecutar la aplicación:**
   ```bash
   dotnet run
   ```

5. **Abrir en el navegador:**
   - La aplicación se ejecutará en `https://localhost:5001` (o el puerto que se muestre en la consola)
   - Navega a la URL indicada en la terminal

### Compilación para Producción
```bash
dotnet publish -c Release
```

Los archivos publicados estarán en `bin/Release/net10.0/publish/`

## Funcionalidades Destacadas de Blazorise

### Ventajas

1. **Sintaxis Bootstrap-like**:
   - Familiar para desarrolladores con experiencia en Bootstrap
   - Sintaxis clara y predecible

2. **Sistema de Grid Responsive**:
   ```razor
   <Row>
       <Column ColumnSize="ColumnSize.Is12.Is6.OnTablet.Is3.OnDesktop">
           <!-- Contenido -->
       </Column>
   </Row>
   ```

3. **Componentes Rico en Funcionalidad**:
   - DataGrid con paginación, ordenamiento, filtrado
   - Modal, Card, Alert totalmente configurables
   - Formularios con validación integrada

4. **Iconos Integrados**:
   ```razor
   <Icon Name="IconName.Save" />
   ```

5. **Helpers Utility**:
   - Margin, Padding, Display, TextColor
   - Background, Border, etc.

### Consideraciones

1. **Chaining de propiedades limitado**:
   - Algunas propiedades como Display requieren inline styles para propiedades complejas
   - Ejemplo: `Display="Display.Flex" Style="justify-content: space-between;"`

2. **Compatibilidad de Iconos**:
   - Algunos nombres de iconos difieren de Font Awesome estándar
   - Requiere verificación en documentación

3. **Validación de Formularios**:
   - System integrado con `<Validations>` y `<Validation>`
   - Requiere configuración manual para escenarios complejos

## Datos de Ejemplo

La aplicación incluye datos de ejemplo pre-cargados:
- **8 productos** en diferentes categorías
- **8 tareas** con diferentes estados y prioridades

Los datos se mantienen en memoria durante la sesión.

## Componentes Blazorise Utilizados

- **Layout**: Layout, LayoutHeader, LayoutContent
- **Navigation**: Bar, BarBrand, BarMenu, BarItem, BarLink
- **Forms**: Field, FieldLabel, FieldBody, TextEdit, Select, Check, NumericEdit, DateEdit, MemoEdit, Switch
- **Data**: DataGrid, DataGridColumn
- **Feedback**: Alert, Modal, Badge
- **UI**: Card, Button, Icon, Tabs, Progress, Divider, Heading, Paragraph
- **Helpers**: Container, Row, Column, Div, Buttons, Small

## Notas Técnicas

### Configuración de Servicios (Program.cs)
```csharp
builder.Services
    .AddBlazorise(options => { options.Immediate = true; })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.Services.AddSingleton<ProductoService>();
builder.Services.AddSingleton<TareaService>();
builder.Services.AddSingleton<EstadisticasService>();
```

### Referencias CDN (index.html)
- Bootstrap 5.3.0 CSS
- Font Awesome 6.4.0
- Blazorise CSS (de paquetes NuGet)

## Comparación con Fluent UI

Para una comparación detallada entre Blazorise y Fluent UI, consulta el archivo `COMPARATIVA.md` en la raíz del proyecto.

## Autor

Generado como POC para evaluar Blazorise como framework de componentes UI para Blazor WebAssembly.

---

**Fecha**: Enero 2026
**Framework**: Blazorise 1.8.8
**.NET Version**: 10.0
