# Filters en NET Core

Los filtros o _filters_ nos permiten ejecutar código antes o después de determinadas fases en el procesamiento de una solicitud HTTP. Es decir podes interferir esta solicitud antes o después de que llego a nuestro _Controller_.

## ¿Para qué utilizarlo?

Podemos crear filtros personalizados para diferentes cuestiones, como por ejemplo el control de errores, almacenamiento _cache_ , configuración, autorización y registro, entre otras.
¿Qué ganamos con esto? Evitamos la duplicación de código en aquellas instancias donde se deben aplicar los mismos procedimientos para muchos métodos del _Controller_.

## Tipos de filtro

Cada tipo de filtro es ejecutado en una fase diferente de la solicitud, es decir tienen un órden de ejecución según su responsabilidad. La manera de saber que fase va a tener nuestro filtra será según la implementación de la _interface_ `IFilterMetadata` del espacio de nombres `Microsoft.AspNetCore.Mvc.Filters`.

|         Tipo |            Interface | Descripción                                                                                                                         |
| -----------: | -------------------: | :---------------------------------------------------------------------------------------------------------------------------------- |
| Autorización | IAuthorizationFilter | Se utiliza para aplicar la política de autorización y seguridad                                                                     |
|       Acción |        IActionFilter | Se utiliza para realizar un trabajo específico inmediatamente antes o después de realizar un método de acción.                      |
|    Resultado |        IResultFilter | Se utiliza para realizar un trabajo específico inmediatamente antes o después de que se procese el resultado de un método de acción |
|    Excepción |     IExceptionFilter | Se usa para manejar excepciones                                                                                                     |

##### Orden de ejecución de los filtros

![logo](DiagramaFases.png)

---

### Filtros de autorización

Estos se utilizan con el fin de autenticar y crear politicas de seguridad para nuestro aplicación web. Se ejecutan antes que cualquier otro filtro y permiten evitar llegar al controller en caso de no cumplir con las politicas de seguridad. Estos estan comprendidos en la _interface_ `IAuthorizationFilter`

###### Definición de la _interface_ `IAuthorizationFilter`

```csharp
namespace Microsoft.AspNetCore.Mvc.Filters {
    public interface IAuthorizationFilter : IFilterMetadata {
        void OnAuthorization(AuthorizationFilterContext context);
    }
}
```

El método `OnAuthorization` se utiliza para escribir el código para que el filtro pueda autorizar la solicitud.
El parámetro `AuthorizationFilterContext context`, recibe los datos del contexto que describen la solicitud. Este objeto contiene una propiedad llamada `Result` del tipo `IActionResult` que se utiliza para alterar la respuesta en el caso de ser necesario. Por ejemplo, si no esta autorizado se responde un `401: Unauthorized`. Esto evita que se llame al _controller_ y el resto de los filtros.

###### Ejemplo

Para este ejemplo nos crearemos un filtro de autorización que simplemente verifique en el encabezado que contenga un identificador.

Utilizaremos una clase llamada `Auth` que nos permitira manejar la lógica del manejo de usuarios. Esta clase esta dentro del proyecto `ej-filters.auth` que fue creado por nosotros para darle completitud a este ejemplo.

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ej_filters.auth;

namespace ej_filters.api.Filters
{
    public class ExampleAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private Auth auth;
        private readonly string msg;
        public ExampleAuthorizationFilter(string message)
        {
            msg = message;
            auth = new Auth();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["auth"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = msg + "no esta logueado."
                };
                return;
            }
            if (!auth.IsLogued(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = msg + "no esta identificado correctamente."
                };
                return;
            }
        }
    }
}
```

Vemos en que en el constructor de este filtro recibimos un atributo `string message`. Esto se debe a que los filtros aceptan parametros de tipo primitivos. Es decir, cuando nosotros indicamos en el método del _Controller_ el filtro a utilzar podemos pasarle parametros a este, que haremos referencia en el contructor del filtro.

Vemos que en los casos que no se cumple con la politica de seguridad se le asigna un `ContentResult` al resultado de la solicitud, y automaticamente se responde la solicitud no llegando de esta manera al método del _Controller_.

### Filtros de acción

Los filtros de acción se ejecutan después de los filtros de autorización. Se llaman justo antes de que se llame un método del _Controller_ y justo después de que se termina un método del _Controller_. Se derivan de la _interface_ de `IActionFilter`.

###### Definición de la _interface_ `IActionFilter`

```csharp
namespace Microsoft.AspNetCore.Mvc.Filters {
    public interface IActionFilter : IFilterMetadata {
        void OnActionExecuting(ActionExecutingContext context);
        void OnActionExecuted(ActionExecutedContext context);
    }
}
```

Al aplicar un filtro de acción a un método del _controller_, se llama al método `OnActionExecuting` justo antes de que se invoque el método del _controller_, y se llama al método `OnActionExecuted` justo después de que el método del _controller_ haya terminado de ejecutarse.

El método `OnActionExecuting` tiene un parámetro del tipo `ActionExecutingContext`. Que destacaremos la siguientes propiedades de este:

|       Nombre | Descripción                                                                                                                                             |
| -----------: | :------------------------------------------------------------------------------------------------------------------------------------------------------ |
| _Controller_ | El nombre del controlador cuyo método está a punto de ser invocado.                                                                                     |
|     _Result_ | Esta propiedad es de tipo `IActionResult`. Si esta propiedad establece un valor de este tipo, se sobreescribe el resultado del método del _Controller_. |

El método `OnActionExecuted` tiene un parámetro del tipo `ActionExecutedContext`. Que destacaremos las siguientes propiedades:

|             Nombre | Descripción                                                                                                                          |
| -----------------: | :----------------------------------------------------------------------------------------------------------------------------------- |
|       _Controller_ | El nombre del controlador cuyo método fue invocado.                                                                                  |
|        _Exception_ | Esta propiedad contiene la excepción que ocurrió en el método del _Controller_.                                                      |
| _ExceptionHandled_ | Cuando establece su propiedad en true, las excepciones no se propagarán más.                                                         |
|           _Result_ | Esta propiedad devuelve el `IActionResult` devuelto por el método del _Controller_, y puede cambiarlo o reemplazarlo si lo necesita. |

###### Ejemplo

En este ejemplo vamos a crear un filtro de acción que simplemente controle el tiempo de ejecución del método del _Controller_. Para esto vamos a iniciar un temporizador antes de la ejecución y lo vamos a parar luego de la ejecución para devolver el timepo que duró esta ejecución.

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ej_filters.api.Filters
{
    public class ExampleActionFilter : Attribute, IActionFilter
    {
        private Stopwatch timer;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            timer = Stopwatch.StartNew();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();
            string result = " Tiempo de ejecucion: " + $"{timer.Elapsed.TotalMilliseconds} ms";
            ((ObjectResult)context.Result).Value = result;
        }
    }
}
```

Para esto inicializamos este cronómetro en el método `OnActionExecuting` y lo detenemos en el método `OnActionExecuted`, cambiando en este último el valor de la respuesta por el tiempo de ejecución.

### Filtros de resultados

Los filtros de resultados se ejecutan antes y después de que se procese el resultado del método del _Controller_. Se ejecutan justo después de los filtros de acción. Se derivan de la _interface_ `IResultFilter`. Cabe destacar que los filtros de resultados tienen el mismo patrón que los filtros de acción.

###### Definición de la _interface_ `IResultFilter`

```csharp
namespace Microsoft.AspNetCore.Mvc.Filters {
    public interface IResultFilter : IFilterMetadata {
        void OnResultExecuting(ResultExecutingContext context);
        void OnResultExecuted(ResultExecutedContext context);
    }
}
```

La _interface_ `IResultFilter` tiene 2 métodos. El método `OnResultExecuting` se llama justo antes de que se procese el resultado del método del _Controller_, mientras que el método `OnResultExecuted` se llama justo después de que se procesa el resultado del método del _Controller_.

El método OnResultExecuting tiene un parámetro del tipo `ResultExecutingContext`. Que destacaremos las siguientes propiedades:

|       Nombre | Descripción                                                                                                           |
| -----------: | :-------------------------------------------------------------------------------------------------------------------- |
| _Controller_ | El nombre del controlador cuyo método se invoca.                                                                      |
|     _Result_ | Esta propiedad es de tipo IActionResult y contiene el objeto `IActionResult` devuelto por el método del _Controller_. |
|     _Cancel_ | Establecer esta propiedad en verdadero detendrá el procesamiento del resultado de la acción y dará una respuesta 404. |

El método `OnResultExecuted` tiene un parámetro del tipo `ResultExecutedContext`. Que destacaremos las siguientes propiedades:

|             Nombre | Descripción                                                                                            |
| -----------------: | :----------------------------------------------------------------------------------------------------- |
|       _Controller_ | El nombre del controlador cuyo método se invoca.                                                       |
|         _Canceled_ | Una propiedad de solo lectura que indica si la solicitud fue cancelada.                                |
|        _Exception_ | Contiene las excepciones lanzadas en el método del _Controller_.                                       |
| _ExceptionHandled_ | Cuando esta propiedad se establece en true, las excepciones no se propagan más.                        |
|           _Result_ | Una propiedad de solo lectura que contiene el `IActionResult` generado por el método del _Controller_. |

###### Ejemplo

En este ejemplo vamos a crear un filtro de resultado que concatene a la respuesta un valor a la respuesta del método del _Controller_. Y luego registre en un log en texto plano la fecha y hora de la ejecución.

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ej_filters.api.Logic;

namespace ej_filters.api.Filters
{
    public class ExampleResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            string result = (string)((ObjectResult)context.Result).Value;
            context.Result = new ObjectResult("Hola " + result);
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            ServiceLogic.EntryLog();
        }
    }
}
```

Cabe destacar que el método `EntryLog` es de una clase creada por nosotros a modo de ejemplo para darle completitud al ejemplo.

### Filtros de excepciones

Los filtros de excepciones permiten capturar excepciones sin tener que escribir el bloque _try & catch_. Para esto implementaremos la _interface_ `IExceptionFilter`.

###### Definición de la _interface_ `IExceptionFilter`

```csharp
namespace Microsoft.AspNetCore.Mvc.Filters {
    public interface IExceptionFilter : IFilterMetadata {
        void OnException(ExceptionContext context);
    }
}
```

Para esta _interface_, los datos de contexto se proporcionan a través de la clase `ExceptionContext` , que es un parámetro del método `OnException`. Destacaremos las siguientes propiedades:

|                Nombre | Descripción                                                                        |
| --------------------: | :--------------------------------------------------------------------------------- |
|             Exception | La propiedad contiene las excepciones que se lanzan                                |
| ExceptionDispatchInfo | Contiene los detalles de seguimiento de la pila de la excepción.                   |
|      ExceptionHandled | Una propiedad de solo lectura que indica si se maneja la excepción                 |
|                Result | Esta propiedad establece el `IActionResult` que se usará para generar la respuesta |

###### Ejemplo

En este ejemplo vamos a capturar las excepciones que salten y vamos a devolver con una respuesta personalizada la solicitud.

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ej_filters.api.Filters
{
    public class ExampleExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = "Se lanzo una excepcion con el siguiente mensaje: " + context.Exception.Message
            };
        }
    }
}
```

Obtenemos la excepción que se ejecutó y anexamos el mensaje a la respuesta.
Es de gran utilidad para cuando repetimos muchos _try & catch_ en los métodos de los _Controllers_.

---

### ¿Cómo los utilizamos?

Para esto fue que agregamos la clase `Attribute` en los filtros de la siguiente manera:

```csharp
public class ExampleAuthorizationFilter : Attribute, IAuthorizationFilter
```

Esto nos permite poder utilizar los filtros de la siguiente manera:

```csharp
[ExampleAuthorizationFilter("No puede ver chistes porque ")]
[ExampleActionFilter]
[ExampleResultFilter]
[HttpGet]
public IActionResult GetAll([FromHeader(Name = "auth")]string token)
{
    return Ok(ServiceLogic.GetJokes());
}
```

Cabe destacar que no solo podemos poner filtros por método sino que también lo podemos aplicar a la clase, de la siguiente forma:

```csharp
[ExampleExceptionFilter]
[ApiController]
[Route("[controller]")]
public class JokesController : ControllerBase
```

---
#### Referencias
* [Filters in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1)