
# Clase 4 - Testing

Como toda plataforma, el testing es una parte esencial del desarrollo del software. En particular en este caso, hablaremos sobre el unit testing en .NET Core.

## [](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/Clases/Clase%204%20-%20Testing.md#testing-en-net-core)Testing en .NET Core

Existen varias opciones para crear tests en .NET Core. La primera es elegir con que framework hacerlos:

-   **xUnit:**  El ya visto en materias anteriores
-   **NUnit:**  Otro framework muy conocido utilizado para hacer pruebas unitarias
-   **MSTest:**  MSTest es un framework de microsoft muy facil de utilizar. Este sera el seleccionado para el curso

Cabe aclarar que la creacion de los proyectos de tests y la ejecución de los mismos (no importa el framework seleccionado) son realizados facilmente desde la consola y/o desde alguna extension del Visual Studio Code.

## [](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/Clases/Clase%204%20-%20Testing.md#setup)Setup

Primero agregaremos algunas extensions a VSCode que nos seran de suma utilidad.

Instalaremos  **.NET Core Test Explorer**, esta nos permitirá explorar las pruebas creadas con mayor facilidad. Podemos buscarla desde el buscador de extensiones o en este  [link](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)

[![.NET Core Test Explorer in marketplace](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/raw/master/imgs/netCoreTestExplorer.PNG)](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/imgs/netCoreTestExplorer.PNG)

Luego lo configuraremos para que detecte nuestros proyectos de prueba. Para esto vamos a la opcion `Extension Settings` que se encuentra en la llave del plugin abajo a la derecha. Podemos acceder a el si nos dirijimos a donde estan todos los plugins que instalamos. Una vez encontrados ahi cambiamos la opcion de `User` a `Workspace`

![work space](https://github.com/ORT-DA2/TECNOLOGIA-2020.1/blob/master/Imagenes/Clase%20Testing/Workspace.PNG)

Aqui introdujimos una pequeña expresion regular. Los proyectos que cumplan con ella sera los que la extension utilizará para correr tests.

## [](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/Clases/Clase%204%20-%20Testing.md#creaci%C3%B3n-de-proyecto)Creación de proyecto

Para crear un proyecto de tests, utilizaremos el comando  `dotnet new mstest`.

```
dotnet new mstest -n Moodle.BusinessLogic.Test
```

Dentro de este proyecto, vamos a agregar un archivo de testing que se llame  `LogicTest.cs`  muy simple. Lo primero que haremos sera agregar la declaracion using  `using Microsoft.VisualStudio.TestTools.UnitTesting;`, para indicar que estamos utilizando el framework de UnitTesting

Muy similarmente a xUnit, el framework utiliza  **tags**  (como  `[TestClass]`  o  `[TestMethod]`) para indicar que una clase contiene tests o que un metodo es de tests. Tambien se utilizan tags para otras funcionalidades, como pasar parametros a los tests, etc. Crearemos dentro del archivo la clase  `LogicTest`  con el metodo  `sumIsOk`.

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moodle.BusinessLogic.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SumIsOk()
        {
        }
    }
}
```
### [](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/Clases/Clase%204%20-%20Testing.md#test-exitoso)Test exitoso

Es hora de crear el test. Para esto, haremos una simple suma y verificaremos que su resultado sea correcto. Lo haremos usando el metodo de Assert,  `Assert.IsEqual(valor1, valor2)`, que verifica que dos valores sean iguales

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moodle.BusinessLogic.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SumIsOk()
        {
            int sum = 2 + 2;
            Assert.AreEqual(4, sum);
        }
    }
}
```
Una vez creado el test, debemos correrlo. Para esto, tenemos dos opciones. Podemos correrlo desde la consola, donde se nos mostrara el output ahi mismo:

```
dotnet tests
```

**Output:**
![consola](https://github.com/ORT-DA2/TECNOLOGIA-2020.1/blob/master/Imagenes/Clase%20Testing/Result.PNG)

O podemos correrlo utilizando la extension que instalamos previamente. Para eso la seleccionamos (La proveta que se encuentra en la barra de la izquierda del VSCode) y seleccionamos el boton de play.

**Output:**

![plugin](https://github.com/ORT-DA2/TECNOLOGIA-2020.1/blob/master/Imagenes/Clase%20Testing/plugin.PNG)

### [](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/Clases/Clase%204%20-%20Testing.md#test-que-falla)Test que falla

Igualmente que como creamos el anterior test, crearemos uno que falle para ver el resultado. Agregamos el siguiente test.

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moodle.BusinessLogic.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SumIsOk()
        {
            int sum = 2 + 2;
            Assert.AreEqual(4, sum);
        }

        [TestMethod]
        public void SumIsNotReallyOk()
        {
            int sum = 2 + 3;
            Assert.AreEqual(6, sum);
        }
    }
}
```
Luego de correrlos nuevamente, podemos ver como el test falla:

![enter image description here](https://github.com/ORT-DA2/TECNOLOGIA-2020.1/blob/master/Imagenes/Clase%20Testing/NoPasa.PNG)

Y hasta se muestra la razon de porque fallo inline:


![Inline explanation](https://github.com/ORT-DA2/TECNOLOGIA-2020.1/blob/master/Imagenes/Clase%20Testing/Esperable.png)
## [](https://github.com/fedeojeda95/N6A-AN-DA2-2019.1-Clases/blob/master/Clases/Clase%204%20-%20Testing.md#test-coverage)Test coverage

A pesar de no existir herramientas integradas a un IDE, como si nos pasaba con visual studio, eso no significa que sea dificil obtener el test coverage de nuestros tests. Para ellos existen excelentes herramientas que nos permitiran obtener el test coverage de manera simple. La que usaremos se llama  [Coverlet](https://github.com/tonerdo/coverlet), y es tan facil como agregar un paquete a la libreria.

Haremos un pequeño ejemplo para mostrar como funciona esta herramienta.


Nos moveremos al proyecto de tests (`cd Moodle.BusinessLogic.Test`) y agregaremos una referencia al proyecto de business logic.

```
dotnet add reference ../Moodle.BusinessLogic
```

Por ultimo, dentro del proyecto de tests (`Moodle.BusinessLogic.Test`) agregaremos como dependencia Coverlet

```
dotnet add package coverlet.msbuild  
```
Luego para probar esta herramienta mas rapido lo que haremos es modificar la clase `StudentLogic` para que no haga uso del repositorio. Quedando asi:
```csharp
using  System;
using  System.Collections.Generic;
using  Moodle.DataAccess;
using  Moodle.Domain;

namespace  Moodle.BusinessLogic
{
	public  class  StudentLogic
	{
		private  readonly  StudentRepository  studentRepository;

		public  StudentLogic()
		{
			MoodleContext  moodleContext = ContextFactory.GetNewContext();
			this.studentRepository = new  StudentRepository(moodleContext);
		}

		public  List<Student> GetAll()
		{
			return  this.studentRepository.GetAll();
		}

		public  Student  Get(int  id)
		{
			//return  this.studentRepository.Get(id);
			return new Student()
			{
				Id = id
			};
		}

		public  void  Add(Student  student)
		{
			this.studentRepository.Add(student);
			this.studentRepository.Save();
		}
		
	}
}
```
El cambio lo podemos ver en el metodo `Get(int id)` donde comento la linea de retorno con el repositorio e implemento un retorno simple.

Ahora pasaremos a probar este metodo desde la clase del paquete de prueba.

Agregamos la referencia a `Moodle.Domain` para comparar estudiantes.

Si estamos en la raiz:
```
dotnet add Moodle.BusinessLogic.Test reference ../Moodle.Domain
```
Si estamos en Moodle.BusinessLogic.Test
```
dotnet add reference Moodle.Domain
```

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moodle.BusinessLogic;

namespace Moodle.BusinessLogic.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly StudentLogic studentLogic;

        public LogicTest()
        {
            this.studentLogic = new StudentLogic();
        }

        [TestMethod]
        public void FirstMethodIsOk()
        {
	        Student studentExpected = new Student()
	        {
		        Id = 2,
		        Name = "Daniel",
		        StudentNumber = "123456" 
	        };
            Student result = this.studentLogic.Get(2);
            
            Assert.AreEqual(studentExpected, result);
        }
    }
}
```

Para que esto funcione debemos implementar el metodo equals en la clase `Student` que solamente compare por `Id`;
```csharp
using  System.Collections.Generic;
using System;  

namespace  Moodle.Domain
{
	public  class  Student
	{
		public  int  Id { get; set; }
		public  string  Name { get; set; }
		public  string  StudentNumber { get; set; }
		
		public  virtual  List<StudentCourse> Courses { get; set; }
		
		public  Student()
		{
			this.Courses = new  List<StudentCourse>();
		}
	
		public bool Equals(object other)
		{
			Student s = obj as Student;
			bool equals;

			if (u is null)
			{
				equals = false;
			}
			else
			{
				equals = this.Id == s.Id;
			}

			return equals;
		}
	}
}
```
Si corremos los tests desde la extension de VS Code, podemos ver que el test pasa correctamente.

Sin embargo, para poder ver el output del test coverage, debemos correrlo desde la consola. Dentro del proyecto, corremos el siguiente comando

```
dotnet test /p:CollectCoverage=true
```

Este comando corre los tests, y con la flat  `CollectCoverage=true`  le indicamos que efectivamente busque el code coverage. Luego de ejecutarlo, vemos el siguiente resultado:

![Cobertura](https://github.com/ORT-DA2/TECNOLOGIA-2020.1/blob/master/Imagenes/Clase%20Testing/cobertura.PNG)

Coverlet tiene muchas mas funcionalidades que pueden investigar en su documentación. Por ultimo, pueden revisar el archivo  `.json`  que es creado (su ubicación se muestra en el output de consola).
