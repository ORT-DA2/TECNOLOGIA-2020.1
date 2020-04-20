
# Mocking

Vamos a estudiar cómo podemos probar nuestro código evitando probar también sus dependencias, asegurándonos que los errores se restringen únicamente a la sección de código que efectivamente queremos probar. Para ello, utilizaremos una herramienta que nos permitirá crear Mocks. La herramienta será Moq.

## ¿Qué son los Mocks?

Los mocks son unos de los varios "test doubles" (es decir, objetos que no son reales respecto a nuestro dominio, y que se usan con finalidades de testing) que existen para probar nuestros sistemas. Los más conocidos son los Mocks y los Stubs, siendo la principal diferencia en ellos, el foco de lo que se está testeando.

Antes de hacer énfasis en tal diferencia, es importante aclarar que nos referiremos a la sección del sistema a probar como SUT (_System under test_). Los Mocks, nos permiten verificar la interacción del SUT con sus dependencias. Los Stubs, nos permiten verificar el estado de los objetos que se pasan. Como queremos testear el comportamiento de nuestro código, utilizaremos los primeros.

## Tipos de  _Test Doubles_
Tipo | Descripción
------------ | -------------
**Dummy** | Son objetos se pasan, pero nunca se usan. Por lo general, solo se utilizan para llenar listas de parámetros.
**Fake** | Son objetos funcionales, pero generalmente toman algún atajo que los hace inadecuados para la producción (una base de datos en la memoria es un buen ejemplo).
**Stubs** | Brindan respuestas predefinidas a las llamadas realizadas en el test, por lo general no responden a nada que no se use en el test.
**Spies** | Son Stubs pero que también registran cierta información cuando son invocados.
**Mocks** | Son objetos pre-programados con expetativas (son las llamadas que se espera que reciban), de todos estos objetos Mocks son los unicos que verifican el comportamiento. Los otros, solo verifican el estado.




## ¿Por qué los queremos usar?

Imaginense que estamos probando el modulo A. Este modulo A utiliza otro modulo, el modulo B. Hacemos nuestras pruebas unitarias, probando el comportamiento que es esperado. Dentro de nuestro grupo de pruebas, vemos que una falla y no sabemos porque. Resulta que el modulo B tiene un problema, un bug, el cual causa que nuestras pruebas del modulo A no pasen. Esto es un problema, nosotros queremos probar  **solo**  el modulo A, no el B.

Cuando hacemos pruebas unitarias, queremos probar objetos y la forma en que estos interactúan con otros objetos. Para ello creamos instancias de Mocks, es decir, objetos que simulen el comportamiento externo (es decir, la interfaz), de un cierto objeto. Son objetos tontos, que no dependen de nadie, siendo útiles para aislar una cierta parte de la aplicación que queramos probar.

Hay ciertos casos en los que incluso los mocks son realmente la forma más adecuada de llevar a cabo pruebas unitarias.

## Modificacion de nuestro codigo actual

Para poder hacer hacer mocks de las dependencias de un objeto que buscamos testear, es necesario cambiar el codigo que tenemos actualmente. Primero, analizaremos como es que se definen las dependencias en nuestro codigo. Tomemos una clase llamada  _StudentLogic_  que estaría  `Moodle.BusinessLogic`. Esta clase depende de  _StudentRepository_  de  `Moodle.DataAccess`. Como definimos esta dependencia? Simplemente en el constructor de  _StudentLogic_  creamos una nueva instancia.

```csharp
    private StudentRepository repository;

    public StudentLogic() {
        repository = new StudentRepository();
    }
```
    
El problema que tiene este enfoque es que cuando creamos una instancia de  _StudentLogic_, se creara una instancia  **real**  de  _StudentRepository_, y no podremos inyectarle un instancia mockeada del repository. La solución (por ahora) es agregar otro constructor que reciba el  _StudentRepository_. Sin embargo, esto no es suficiente: ya que no debemos recibir por parametro una instancia real del repositorio. Debemos crear una interfaz, la cual el repositorio implemente.

```csharp
private IStudentRepository repository;

public StudentLogic(IStudentRepository repository = null) 
{
    if (repository == null) 
    {
	    this.repository = new StudentRepository();
    } 
    else 
    {
	    this.repository = repository;
    }
}
```
 

La interfaz  _IStudentRepository_  contendra los mismos metodos que tenia nuestro  _StudentRepository_. Esta tambien sera implementada por los mocks que crearemos mas adelante, lo cual nos permite poder crear un  _StudentLogic_  con el mock! Debemos repetir esto para todas las clases que dependan de otra clase. Por ejemplo, StudentController debera poder recibir en el constructor una interfaz que sea implementada  _StudentLogic_  también.

Al hacer esto, no estamos atando la instancia a un objeto en particular, si no que estamos  **inyectando**  la dependencia. Como vimos en la clase anterior. Todo objeto que queramos mockear, debera tener una interfaz bien definida.

[![alt text](https://camo.githubusercontent.com/fb50030bb43d41299a6afed9a27b89655b0b0c9d/687474703a2f2f7475746f7269616c732e6a656e6b6f762e636f6d2f696d616765732f6a6176612d756e69742d74657374696e672f74657374696e672d776974682d64692d636f6e7461696e6572732e706e67)](https://camo.githubusercontent.com/fb50030bb43d41299a6afed9a27b89655b0b0c9d/687474703a2f2f7475746f7269616c732e6a656e6b6f762e636f6d2f696d616765732f6a6176612d756e69742d74657374696e672f74657374696e672d776974682d64692d636f6e7461696e6572732e706e67)

En consecuencia, generamos un  **bajo acoplamiento**  entre una clase y sus dependencias, lo cual nos facilita utilizar un framework de mocking. Especialmente para aquellos objetos que dependen de un recurso externo (una red, un archivo o una base de datos).

A pesar de que aun se esta instanciando la clase directamente en el constructor, veremos mas adelante como podemos remover eso. Iremos aun mas adelante y removeremos la instanciacion en el constructor. Por ahora, hacemos lo suficiente para que podamos hacer los tests.

Debemos hacer esto para todas las dependencias que tengamos.

## Empezando con Moq

## WebApi

Para comenzar a utilizar Moq, comenzaremos probando nuestro paquete de controllers de la WebApi. Para esto, crearemos un nuevo proyecto de MSTests (Moodle.WebApi.Tests) y le instalamos Moq. 

```
dotnet new mstest -n Moodle.WebApi.Test
cd Moodle.WebApi.Test
dotnet add package Moq
```

Luego al proyecto de tests le agregaremos las referencias a WebApi, Domain y BusinessLogic.Interface

```
dotnet add reference ../Moodle.WebApi
dotnet add reference ../Moodle.Domain
dotnet add reference ../Moodle.BusinessLogic

```

Una vez que estos pasos estén prontos, podemos comenzar a realizar nuestro primer test. Creamos entonces la clase  `StudentControllerTests`, y en ella escribimos el primer  `TestMethod`.

## Probando el POST

```csharp
[TestClass]
public class StudentControllerTests
{
    [TestMethod]
    public void CreateValidStudentOkTest()
    {
        //Arrange
        
        //Act
        
        //Assert
    }

}
```

Para ello seguiremos la metodología  **AAA: Arrange, Act, Assert**.

-   **Arrange**: Contruimos el objeto mock y se lo pasamos al sistema a probar
-   **Act**: Ejecutamos el sistema a probar
-   **Assert**: Verificamos la interacción del  _SUT_  con el objeto mock.

```csharp
[TestMethod]
public void CreateValidStudentOkTest()
{
	Student student = new Student()
	{
	    Name = "Daniel",
	    StudentNumber = "123456",
	    Courses = new List<Course>()
	    {
		    new Course()
		    {
			    Id = 1,
			    Name = "DA2"
		    }
	    }
	};//1
	
	ModelStudent modelStudent = new ModelStudent()
	{
	    Name = "Daniel",
	    StudentNumber = "123456",
	    Courses = new List<ModelCourseBasicInfo>()
	    {
		    new ModelCourseBasicInfo()
		    {
			    Id = 1,
			    Name = "DA2"
		    }
	    }
	}
	var mock = new Mock<IStudentLogic>(MockBehavior.Strict); // 2
	var controller = new StudentController(mock.Object);//3

	var result = controller.Post(modelStudent); // 4
	var createdResult = result as CreatedAtRouteResult; // 5
	var model = createdResult.Value as ModelStudentDetailInfo; // 6

	//Assert
}
```
Veremos que pasa en el test:

1.  Crea un objeto de  `Student`  que usaremos para el mock. Este retorna data que no nos importa, es solo para testing
2.  Creamos el mock. La notacion es  `new Mock<A>`  siendo A la interfaz que queremos mockear. El parametro (`MockBehhavior.Strict`) es una parametro de configuracion del mock.  `.Strict`  hace que se tire una excepcion cuando se llama un metodo que no fue mockeado, mientras que  `.Loose`  retorna un valor por defecto si se llama un metodo no mockeado.
3.  Se crea el controlador (`StudentController`) con el objeto mockeado.
4.  Se ejecuta el metodo Post del controlador
5.  Debido a que la clase retorna un  `CreatedAtRouteResult`, como se puede ver en la implementacion, casteamos el resultado a esto
6.  Mediante  `.Value`  obtenemos el resultado de la request

Sin embargo, nos falta definir el comportamiento que debe tener el mock del nuestro  `IStudentLogic`. Esto es lo que llamamos  **expectativas**  y lo que vamos asegurarnos que se cumpla al final de la prueba. Recordemos, los mocks simulan el comportamiento de nuestros objetos, siendo ese comportamiento lo que vamos a especificar a partir de expectativas. Para ello, usamos el método  **Setup**.

### ¿Cómo saber qué expectativas asignar?

Esto va en función del método de prueba. Las expectativas se corresponden al caso de uso particular que estamos probando dentro de nuestro método de prueba. Si esperamos probar el  `Post()`  de nuestro  `StudentController`, y queremos mockear la clase  `StudentLogic`, entonces las expectativas se corresponden a las llamadas que hace  `StudentController`  sobre  `StudentLogic`.

Veamos el método a probar, el  `POST`  de un student:

```csharp
[HttpPost]
public IActionResult Post([FromBody] ModelStudent modelStudent)
{
    try {
        Student createdStudent = this.studentLogic.Add(modelStudent.ToEntity());
        return CreatedAtRoute("GetStudent", new { id = createdStudent.Id }, new ModelStudentDetailInfo(createdStudent));
    } catch(ArgumentException e) {
        return BadRequest(e.Message);
    }
}
```
La línea que queremos mockear es la de:
```csharp
Student createdHomework = this.studentLogic.Add(modelStudent.ToEntity());
```
Entonces:

1.  Primero vamos a decirle que esperamos que sobre nuestro Mock que se llame a la función Add().
2.  Luego vamos a indicarle que esperamos que tal función retorne un student que definimos en otro lado.

```csharp
[TestMethod]
public void CreateValidHomework()
{
    Student student = new Student()new Student()
    {
	    Name = "Daniel",
	    StudentNumber = "123456",
	    Courses = new List<Course>()
	    {
		    new Course()
		    {
			    Id = 1,
			    Name = "DA2"
		    }
	    }
    };
	
	ModelStudent modelStudent = new ModelStudent()
	{
		Name = "Daniel",
	    StudentNumber = "123456",
	    Courses = new List<ModelCourseBasicInfo>()
	    {
		    new ModelCourseBasicInfo()
		    {
			    Id = 1,
			    Name = "DA2"
		    }
	    }
	};

    var mock = new Mock<IStudentLogic>(MockBehavior.Strict);
    mock.Setup(s => s.Add(It.IsAny<Student>())).Returns(student); //1
    var controller = new StudentController(mock.Object);

    var result = controller.Post(modelStudent);
    var createdResult = result as CreatedAtRouteResult;
    var model = createdResult.Value as ModelStudentDetailInfo;

    mock.VerifyAll();//2
    Assert.AreEqual(new ModelStudentDetailInfo(student), model);//3
}
```
Veamos que le agregamos al metodo de test:

1.  Seteamos el mock. Cuando decimos setear, queremos decir que le definimos el comportamiento que queremos en el test de un metodo de un mock. El metodo setup recibe una funcion inline de LINQ, la cual recibe el objeto a mockear. Es decir, en este caso  `s`  es un objeto de tipo  `IStudentLogic`. Aqui estamos definiendo que para el metodo  `Add`, cuando reciba cualquier parametro de tipo  `Student`  (`It.IsAny<Student>()`). En este caso, se retorna el  `student`. Asi como devolvimos el estudiante porque estamos simulando un caso sin errores tambien podemos devolver excepciones con _Throw_ para simular que sucedio algo inesperado en la capa que se esta haciendo uso.
2.  También debemos verificar que se hicieron las llamadas pertinentes. Para esto usamos el método  `VerifyAll`  del mock. Este revisa que fueron llamadas todas las funciones que mockeamos.
3.  Verificamos que los datos obtenidos sean correctos. Para esto hacemos asserts (aquí estamos probando estado) para ver que los objetos usados son consistentes de acuerdo al resultado esperado. Para que este Assert funcione hay que hacer un _override_ del metodo _Equals_ en la clase _ModelStudentDetailInfo_ para que se ejecute. Otra opcion seria verificar estado por estado sin redefinir el metodo _Equals_.

Corremos los tests utilizando  `dotnet test`  y vemos que nuestro test pasa  😎!

## Mockeando excepciones

Como dijimos antes tambien podemos mockear excepciones. Ahora veamos como probar cuando nuestro  `Post()`  del Controller nos devuelve una  **BadRequest**.

Particularmente, en el caso que hemos visto antes nuestro Controller retornaba  `CreatedAtRoute`  para dicha situación. Ahora, nos interesa probar el caso en el que nuestro Controller retorna una BadRequest. Particularmente, esto se da cuando el método  `Add()`  recibe  `null`. Para probar este caso entonces, seteamos dichas expectativas y probemos.

```csharp
[TestMethod]
public void CreateInvalidStudentBadRequestTest()
{
    var mock = new Mock<IStudentLogic>(MockBehavior.Strict);
    mock.Setup(m => m.Add(null)).Throws(new ArgumentException());
    var controller = new StudentController(mock.Object);

    var result = controller.Post(null);

    mock.VerifyAll();
    Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
}
```
Lo que hicimos fue indicar que cuando se invoque  `Add`  con el parametro en  `null`, se lance  `ArgumentException`. En consecuencia, cuando nuestro controller llame a este mock, se lanzara  `ArgumentException`  causando que nuestro controller la capture y retorne  `BadRequest`.

Finalmente entonces, verificamos que las expectativas se hayan cumplido (con el  `VerifyAll()`), y luego que el resultado obtenido sea un  `BadRequestObjectResult`, usando el metodo de  `Assert`  `IsInstanceOfType`.

## BusinessLogic

Creamos nuestro proyecto:

```
dotnet new mstest -n Moodle.BusinessLogic.Test
cd Moodle.BusinessLogic/Test
dotnet add package Moq
```

Agregamos las referencias a  `BusinessLogic`,  `Domain`  y finalmente a  `DataAccess`

Creamos entonces la clase StudentLogicTests.

## Probando el Create User

Entonces:

1.  Primero vamos a decirle que esperamos que sobre nuestro Mock que se llame a la función Add().
2.  Luego vamos a indicarle que esperamos que se llame la función Save().
3.  Invocamos Add
4.  Verificamos que se hicieron las llamadas pertinentes, y realizamos Asserts

```csharp
[TestMethod]
public void CreateValidStudentTest()
{
    Student student = new Student()new Student()
    {
	    Name = "Daniel",
	    StudentNumber = "123456",
	    Courses = new List<Course>()
	    {
		    new Course()
		    {
			    Id = 1,
			    Name = "DA2"
		    }
	    }
    };

    var mock = new Mock<IStudentRepository>(MockBehavior.Strict);
    mock.Setup(m => m.Add(It.IsAny<Student>()));
    mock.Setup(m => m.Save());

    var studentLogic = new StudentLogic(mock.Object);

    var result = studentLogic.Add(student);

    mock.VerifyAll();
    Assert.AreEqual(student, result);
}
```
## Ejercicio

-   Agregar muchos mas tests a todo el sistema!

# Mas Info

En la documentación de MOQ, se encuentran varios ejemplos y definiciones de como hacer los mocks:

-   [MOQ](https://github.com/moq/moq4)
-   [MOQ quickstart](https://github.com/Moq/moq4/wiki/Quickstart)

Tipos de respuesta de Web API. Puede ser importante saberlo para testear las respuestas de los controladores de Web API

-   [Action return types](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-2.2)

Info en general:

-   [Mocks Aren't Stubs](https://martinfowler.com/articles/mocksArentStubs.html)
-   [Asserting exception with MSTest](http://www.bradoncode.com/blog/2012/01/asserting-exceptions-in-mstest-with.html)
-   [Exploring assertions (recomendado)](https://www.meziantou.net/2018/01/22/mstest-v2-setup-a-test-project-and-run-tests)
