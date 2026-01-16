# FluentUI-POC - Prueba de Concepto con Microsoft Fluent UI

POC de aplicación web desarrollada con Blazor WebAssembly y Microsoft Fluent UI para demostrar las capacidades y características de desarrollo frontend.

## Tecnologías Utilizadas

- **.NET 10.0** - Framework de desarrollo
- **Blazor WebAssembly** - Framework para aplicaciones web SPA
- **Microsoft Fluent UI 4.13.2** - Librería de componentes UI
- **C#** - Lenguaje de programación

## Características Implementadas

### 1. Dashboard Interactivo
- Tarjetas con estadísticas en tiempo real
- Gráficos de productos por categoría
- Distribución de tareas por estado y prioridad
- Botones de acción rápida para navegación

### 2. CRUD Completo de Productos
- **Lista de Productos** con:
  - Tabla con ordenamiento por columnas
  - Búsqueda y filtrado
  - Paginación (10 productos por página)
  - Acciones de editar y eliminar
- **Formulario de Producto** con:
  - Validación de campos (nombre, descripción, categoría, precio, stock)
  - Selector de categorías predefinidas
  - Mensajes de éxito/error con diálogos

### 3. CRUD Completo de Tareas
- **Lista de Tareas** con:
  - Tarjetas visuales con color según prioridad
  - Checkbox para marcar como completada
  - Filtros por estado (Pendiente, En Progreso, Completada)
  - Búsqueda por título o descripción
  - Indicador de tareas vencidas
- **Formulario de Tarea** con:
  - Campos: título, descripción, estado, prioridad, fecha vencimiento
  - Validación de formulario
  - Checkbox para marcar como completada

### 4. Showcase de Componentes
Página demostrativa organizada en tabs que muestra:
- **Botones**: Diferentes estilos y estados (Accent, Neutral, Outline, Disabled, Loading)
- **Formularios**: TextField, TextArea, NumberField, Select, DatePicker, Checkbox, Radio, Switch, Slider
- **Alertas y Diálogos**: MessageBar con diferentes intents, Toasts, Diálogos de confirmación
- **Cards y Badges**: Tarjetas con contenido y badges con colores
- **Progress Indicators**: ProgressBar, ProgressRing, Skeleton loaders
- **Otros**: Accordion, Divider, Tooltip

### 5. Navegación
- Layout con header y menú lateral colapsable
- Navegación entre páginas con FluentNavMenu
- Rutas bien definidas para cada sección

## Estructura del Proyecto

```
FluentUI-POC/
├── Models/
│   ├── Producto.cs          # Modelo de datos de producto
│   └── Tarea.cs             # Modelo de datos de tarea con enums
├── Services/
│   ├── ProductoService.cs   # Lógica de negocio de productos
│   ├── TareaService.cs      # Lógica de negocio de tareas
│   └── EstadisticasService.cs # Cálculos para el dashboard
├── Pages/
│   ├── Home.razor           # Dashboard principal
│   ├── Productos.razor      # Lista de productos
│   ├── ProductoForm.razor   # Formulario de producto
│   ├── Tareas.razor         # Lista de tareas
│   ├── TareaForm.razor      # Formulario de tarea
│   └── Componentes.razor    # Showcase de componentes
├── Layout/
│   ├── MainLayout.razor     # Layout principal
│   └── NavMenu.razor        # Menú de navegación
└── wwwroot/
    └── index.html           # HTML base
```

## Requisitos Previos

- .NET SDK 10.0 o superior
- Visual Studio 2022 / VS Code / Rider (opcional)
- Navegador web moderno (Chrome, Edge, Firefox)

## Instrucciones de Ejecución

### Opción 1: Línea de Comandos

```bash
# Navegar al directorio del proyecto
cd FluentUI-POC/FluentUI-POC

# Restaurar dependencias (opcional, se hace automáticamente)
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la aplicación
dotnet run
```

La aplicación estará disponible en:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`

### Opción 2: Visual Studio / Rider

1. Abrir el archivo `FluentUI-POC.sln`
2. Presionar F5 o hacer clic en el botón "Run"
3. El navegador se abrirá automáticamente

## Datos de Ejemplo

La aplicación incluye datos de ejemplo precargados:
- **8 Productos** de diferentes categorías (Electrónica, Periféricos, etc.)
- **8 Tareas** con diferentes estados y prioridades

Los datos se almacenan en memoria, por lo que se resetean al reiniciar la aplicación.

## Funcionalidades Clave de Fluent UI

### Ventajas Observadas

1. **Diseño Moderno**: Componentes con el Microsoft Design System
2. **Componentes Completos**: Gran variedad de componentes UI listos para usar
3. **Tipografía**: Sistema de tipografía bien definido
4. **Temas**: Soporte para variables CSS personalizables
5. **Stack Layout**: FluentStack para layouts flexibles

### Consideraciones de Desarrollo

1. **Curva de Aprendizaje**: Requiere familiarizarse con la API específica de Fluent UI
2. **Documentación**: Buena documentación en [fluentui-blazor.net](https://www.fluentui-blazor.net/)
3. **Tipos Genéricos**: Algunos componentes requieren especificar tipos genéricos (`TOption`, `TValue`)
4. **Binding**: Binding de datos robusto pero con algunas peculiaridades en componentes específicos

## Próximos Pasos

Para extender esta POC:
- Integrar con backend real (API REST)
- Agregar autenticación y autorización
- Implementar persistencia de datos
- Agregar más páginas y funcionalidades
- Personalizar el tema con variables CSS

## Recursos

- [Microsoft Fluent UI Blazor](https://www.fluentui-blazor.net/)
- [Documentación de Blazor](https://learn.microsoft.com/aspnet/core/blazor/)
- [Código fuente en GitHub](https://github.com/microsoft/fluentui-blazor)
