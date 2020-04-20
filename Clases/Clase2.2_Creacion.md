
# Clase 2: Creacion de un proyecto
Como se menciono anteriormente vamos a necesitar: 

 1. [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
 2. [Visual Studio Code](https://code.visualstudio.com/download) 
 3. [Postman](https://www.postman.com/downloads/)

En los titulos encontraran los videos explicando el contenido.

## [Cheat sheet (Lista de comandos desde la consola)](https://youtu.be/-pkbte8x6iw)

|Comando|Resultado  |
|--|--|
|```dotnet new sln``` |Creamos una solucion (principalmente util para VisualStudio, cuando queremos abir la solucion y levantar los proyectos asociados)
|```dotnet new webapi -n "Nombre del proyecto"```  | Crear un nuevo proyecto del template WebApi
| ```dotnet sln add```|Asociamos el proyecto creado al .sln 
| ```dotnet sln list``` | Vemos todos los proyectos asociados a la solucion 
| ```dotnet new classlib -n "Nombre del proyecto"``` | Crea una nueva libreria (standard)
|```dotnet add "Nombre del proyecto 1" reference "Nombre del proyecto 2"``` | Agrega una referencia del Proyecto 2 al Proyecto 1
|```dotnet add package "Nombre del package"``` | Instala el Package al proyecto actual. Similar a cuando se agregaban paquetes de Nuget en .NET Framework
|```dotnet clean``` | Borra lo compilado
|```dotnet build``` | Compila y genera los archivos prontos para ser desplegados
|```dotnet run``` | Compila y corre el proyecto
| ```dotnet -h``` | Ayuda para ejecutar un comando o para inspeccionar diferentes comandos

### Es necesario crear una solucion? (SLN)

No, no es necesario. Sin embargo, tener una trae varias utilidades:

-   Si usamos Visual Studio 2019, es necesario crearla para levantar todos los proyectos dentro del IDE
-   Aunque no usemos VS2019, tener una solucion permite crear/compilar/manejar todos los proyectos involucrados juntos, sin tener que correr cada uno, por ejemplo. Se maneja todo como una unica unidad.

Para mas informacion, se puede leer  [aquí](https://stackoverflow.com/questions/42730877/net-core-when-to-use-dotnet-new-sln)

## [Creacion de proyecto HomeworkWebApi](https://youtu.be/JrOLycQE_5Y)

A continuación crearemos un proyecto de ejemplo, sobre el cual seguiremos trabajando y seguiremos agregandole funcionalidad.

### Creamos el sln para poder abrirlo en vs2017 y otras utilidades (opcional)

```
dotnet new sln
```

### Creamos el proyecto webapi y lo agregamos al sln

```
dotnet new webapi -au none -n Moodle.WebApi
dotnet sln add Moodle.WebApi
```

### Creamos la libreria businesslogic y la agregamos al sln

```
dotnet new classlib -n Moodle.BusinessLogic
dotnet sln add Moodle.BusinessLogic

```

### Creamos la libreria dataaccess y la agregamos al sln

```
dotnet new classlib -n Moodle.DataAccess
dotnet sln add Moodle.DataAccess

```

### Creamos la libreria domain y la agregamos al sln

```
dotnet new classlib -n Moodle.Domain
dotnet sln add Moodle.Domain

```

### [Agregamos referencias de los proyectos a la webapi](https://youtu.be/t_Mdv4X4fPc)

```
dotnet add Moodle.WebApi reference Moodle.DataAccess
dotnet add Moodle.WebApi reference Moodle.Domain
dotnet add Moodle.WebApi reference Moodle.BusinessLogic

```

### Agregamos la referencia del domain al dataaccess

```
dotnet add Moodle.DataAccess reference Moodle.Domain

```

### Agregamos las referencias de domain y dataaccess a businesslogic

```
dotnet add Moodle.BusinessLogic reference Moodle.Domain
dotnet add Moodle.BusinessLogic reference Moodle.DataAccess

```

### Descargamos Entity Framework Core

Nos movemos a la carpeta WebApi (`cd Moodle.WebApi`)

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory

```

Nos movemos a la carpeta dataaccess (`cd Moodle.DataAccess`)

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory

```

### [Estructura de los proyectos](https://youtu.be/E4ZQr0x-QhU)

Antes de comenzar a progamar, es importante saber como esta formado cada uno de los proyectos. Tenemos 3 grandes elementos (o conjuntos de) en el proyecto ademas de nuestro codigo.

**X.csproj**

Este es el archivo de configuracion del proyecto. Aqui se definen varias cosas como:

-   Version del framework usado (netcore3.1 por ejemplo)
-   Dependencias a otros proyectos dentro de una solucion y el path a ellos (por ejemplo  `<ProjectReference Include="..\Homeworks.Domain\Moodle.Domain.csproj" />`  indica que hay una diferencia el proyecto  `Moodle.Domain`)
-   Dependencias de paquetes externos de nuget. (Por ejemplo:  `<PackageReference Include="Microsoft.EntityFrameworkCore" Version="2.2.2" />`  indica que este proyecto utiliza  `EntityFrameworkCore`). Cuando se toma el proyecto nuevo, se utiliza esta informacion para bajar los archivos necesarios de nuget packages.

Este archivo vendria a tener una funcionalidad similar a la que tienen archivos similares en otros lenguajes/plataformas, como Javascript con el  `package.json`

**/bin**

Aca se encuentran todos los archivos compilados. Cada vez que se hace  `dotnet run`  o  `dotnet build`, se compila el proyecto y se generan los  `.dll`  correspondientes. Lo mejor es ignorar de git esta carpeta

**/obj**

Son varios archivos que utiliza despues el compilador para compilar el proyecto. Son una especie de "archivos intermedios". Tambien conviene ignorar en git esta carpeta.

### Codigo basico

Agregaremos un poco de funcionalidad muy basica al sistema que tenemos para poder probar que este funcionando correctamente.

[**En Moodle.Domain**](https://youtu.be/Needv7-yz2c)
Crearemos la clase que representa a nuestros estudiantes,  `Student`.
```csharp
using System;
using System.Collections.Generic;

namespace Moodle.Domain
{
    public class Student
    {       
        public int Id { get; set; }
        public string Name { get; set; }
        public string StudentNumber { get; set; }
        public List<Course> Courses { get; set; }

        public Student()
        {
            this.Courses = new List<Course>();
        }
    }
}
```


```csharp
using System;

namespace Moodle.Domain
{
    public class Course
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public List<Student> Students {get; set;}

        public Course() {
            this.Students = new List<Student>();
        }
    }
}
```

[**En Moodle.BusinessLogic**](https://youtu.be/CVp5mRqKJGo)

Crearemos una clase llamada  `StudentLogic`, la cual tiene la logica de nuestros estudiantes y la clase `CourseLogic`, la cual tiene la logica de nuestros cursos.
```csharp
using System;
using Moodle.Domain;

namespace Moodle.BusinessLogic
{
    public class StudentLogic
    {
	    public List<Student> GetAll()
	    {
		    return new List<Student>()
		    {
			    new Student()
			    {
				    Id = 0,
				    Name = "Daniel",
				    StudentNumber = "185082"
			    }
		    };
	    }
    }
}
```
```csharp
using System;

namespace Moodle.BusinessLogic
{
    public class CourseLogic
    {
	    
    }
}
```
[**En Moodle.WebApi**](https://youtu.be/a6VyIfmJ6e4)

Este proyecto sera donde tengamos los controllers. Estos tendran la responsabilidad de:

-   Definir las rutas
-   Obtener los datos que son enviados en las requests (ya sea por la URL, por headers, por el body, etc)

Primero crearemos la clase  `StudentController` y luego `CourseController`. Dentro de `StudentController`, agregaremos una unica ruta:
```csharp
namespace Moodle.WebApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
	    private readonly StudentLogic studentLogic;
		
		public StudentController()
		{
			this.studentLogic = new StudentLogic();
		}
		
        // GET api/values
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(this.studentLogic.GetAll());
        }
    }
}
```
Esta ruta lo que hace es devolvernos la lista de estudiantes que esta creando `StudentLogic`.

Para [probarlo](https://youtu.be/lXlqmRalYt8), podemos utilizar el browser. Si corremos  `dotnet run`  dentro de  `Moodle.WebApi`, el proyecto se inicia. Si vamos a  `https://localhost:5001/api/students`  podemos ver el estudiante que creamos.

[Posible error que puede aparecer cuando se prueba desde el puerto 5001](https://youtu.be/K5yHGXAaVDM)




[Finalizacion](https://youtu.be/mJn_eMse9BQ)
