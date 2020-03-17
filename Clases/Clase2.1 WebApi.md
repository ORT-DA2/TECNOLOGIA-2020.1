

# Web API

En esta parte, explicaremos que es Web API. Primero, daremos un paso atras y explicaremos los conceptos asociados a una API REST, para luego explicar como es que implementa todo esto ASP NET Core Web API.

### Que es una API?

Primero, debemos saber que es una API. En el software, una API (aplication programming interface) es un conjunto definiciones, protocolos y herramientas para crear software y aplicaciones. Estas son ofrecidos por un software para ser utilizado por  _otro_  software, ofreciendo asi una capa de abstracción.

En terminos mas simples, una API es un tipo de interfaz la cual tiene un conjunto de funciones que permite a los desarrolladores acceder a un conjunto especifico de funcionalidades o informacion de una aplicaciones, sistemas operativo, libreria, u otros servicios.

### Que es una Web API?

Una Web API, es como su nombre indica, una API especifica. Es una API la cual es utilizada mediante la web, mas especificamente utilizando el protocolo HTTP. Para acceder a esta API, se utiliza una URL (`https://x.com/a/b`). La informacion que se envia y recibe en estas APIs son en algun formato especifico, como XML o JSON (el cual usaremos en este curso y es el standard actual)

Es un concepto y no una tecnologia. Se puede crear una API Web usando distintas tecnologias como Java, Javascript, .NET, etc. Un ejemplo de una API Web es la de  `Twitter`, la cual permite obtener informacion de los datos e integrarse con la plataforma misma.

Existen varios conceptos asociados a las Web APIs, como endpoints, REST, entre otros, que veremos mas adelante.

### Que es ASP .NET Core Web Api?

`ASP .NET Core Web Api`  es el framework creado por Microsoft que corre sobre .NET Core. Es un framework extensible para crear servicios basados en HTTP REST, los cuales pueden ser accedidos mediante la web. Cuenta con varias caracteristicas modernas que hacen la vida del desarrollador mas facil:

-   Parseo automatica a JSON que tambien es customizable
-   Herramientas de autenticacion
-   Definición simple de rutas en el codigo
-   Tooling en varias plataformas para que el desarrollo sea comodo y facil
-   Orientado a APIs REST

## Que es REST? Diseño de APIs

REST es un estilo arquitectonico que define guias de como hacer y como restringir los servicios Web y las interacciones con ellos.

Es importante aclarar que REST, en su condicion de estilo arquitectonico, no es un estandard estricto, si no que permite flexibilidad. Debido a estar libertad y flexibilidad en su estructura, es importante definir buenas practicas.

REST sirve como guia para definir como nombrar los recursos web y como utilizarlos. Esta interaccion es fuertemente basada en herramientas del protocolo HTTP. Por ejemplo, REST da una recomendacion de en que casos utilizar cada uno de los verbos (`GET`,  `POST`, etc).

Algo importante de aclarar es que nada esta escrito con fuego. REST recomienda cosas, pero pueden haber casos especificos donde hay que romper estas convenciones. Lo importante es tener claro porque se rompe, que es lo que se esta buscando y mantener estos casos al minimo.

A continuación, definiremos una guia de diseño para APIs basada fuertemente en REST, mostrando algunas de las recomendaciones y limitaciones que REST plantea.

### Todas las requests son "stateless". No se mantiene estado del lado del servidor

Todo el estado (es decir, informacion necesaria para llevar a cabo una accion) tiene que estar del lado de la request (ya sea en la url, como parametro, como header) y no del lado del servidor.

Esto nos permite independizarnos del estado del servidor. No importa si tenemos muchos, si este se cae y se levanta de vuelta, nuestra request sera recibida y procesada de la misma manera.

A su vez, mantener estado del lado del servidor nos puede brindar problemas a la hora de tener que atender muchas solicitudes a la vez.

### Siempre usar sustantivos, nunca verbos.

Dentro de las url de una API REST, deben evitarse los verbos y preferir los sustantivos.

**Mantener la url base lo mas simple a intuitiva posible**

Una URL base que sea simple e intuitiva hace que utilizar la API sea simple. Si mediante la URL se puede entender que hace la API sin necesitar ningun tipo de documentacion extra, sera mas simple de ser utilizada.

Algo que mantiene la simplicidad es intentar tener solo 2 URLs por recurso. Tomemos el ejemplo de una api que maneja perros. Para los perros, deberiamos tener solo 2 urls:

-   `/dogs`  que representa todos los perros del sistema
-   `/dogs/123456`  que representa a un perro especifico en el sistema

**Mantener los verbos fuera de la URL:**

Tener verbos en las URLs lleva a que, cuando una API va creciendo, se terminen teniendo demasiados endpoints innecesariamente. Aunque puedan haber pocos recursos, es dificil representar todos los estados posibles con verbos.

Por ejemplo, siguiendo con el caso de los perros. Si representamos todo con verbos, podemos terminar con casos asi:

-   `/getAllLeashedDogs`: Los perros que tenga una correa. Deberia haber sido una url que obtenga todos los perros y reciba un parametro para filtrar.
-   `/getHungerLevel`:  `Hunger`  deberia ser un atributo que se obtenga del perro, no es necesario una url solo para esto.

Y asi pueden haber muchos mas casos.

**Utilizar los verbos HTTP para representar las acciones:**

Si tomamos nuevamente el caso de los perros tenemos dos URLs:  `/dogs`  y  `/dogs/123456`. Para operar sobre ellos, es decir, actualizarlos, obtenerlos, crearlos, etc, utilizamos los verbos HTTP. Estos son

-   `GET`
-   `POST`
-   `PUT`
-   `DELETE`

Estos generalmente se utilizan para expresar los que se llama  `CRUD`  de un recurso (**C**reate,  **R**ead,  **U**pdate,  **D**elete).

Con las dos URLs de recursos (`/dogs`  y  `/dogs/123456`) en conjunto con los 4 verbos HTTP, tenemos un conjunto de operaciones que es intuitivo para los usuarios de la API. A continuación se muestra como funcionan estas combinaciones:

POST (crear)

GET (obtener)

PUT (modificar)

DELETE (borrar)

**`/dogs`**

Crear un nuevo perro

Obtener todos los perros

Actualizar un conjunto de perros a la misma vez

Borrar todos los perros

**`/dogs/123456`**

Error

Devolver perro con id 123456

Si existe perro con id 123456, actualizarlo, si no error

Borrar perro con id 123456, si no existe error

Debido a que esto es intuitivo y conocido por todos los desarrolladores, esta tabla nisiquiera es necesaria despues. Alguien puede saber como funciona la API sin ninguna documentación.

### Plural o singular? Que tanta abstracción?

Dijimos previamente que se deben utilizar sustantivos, pero no especificamos si debian ser en plural o en singular.

A pesar de que no hay un decision especifica sobre esto, la intuicion lleva a pensar que es mejor tener los recursos en plural. Los recursos quedan mas facil de leer, y como generalmente los endpoints mas utilizados son los GET, estos quedan mas claro con plural.  `GET /dogs`  obtiene los perros,  `GET /dogs/1234`  obtiene de los perros, el de id  `1234`.

Lo mas importante aca es mantener consistencia. Nunca mezclar singular con plural. La inconsistencia hace que la API no sea predecible y sea mas dificil de usar.

**Los nombres concretos son mejor que los abstractos:**

A pesar de que los desarrolladores siempre estan buscando un nivel de abstraccion mas alto, en los casos de los recursos en una API REST, se debe preferir los nombres concretos.

Imaginense que tenemos una api que tiene perros, gatos, pajaros, etc. Uno podria pensar que una buena abstraccion es tener una sola url  `/animals`. Sin embargo, esto termina siendo contraproducente:

-   No se ve que hace la API con ver sus urls. Nos perdemos de la oportunidad de que un usuario de la API vea las urls y sepa que nuestro sistema maneja especificamente perros, gatos y pajaros.
-   Resulta mas dificil de utilizar la API, ya que no se sabe especificamente que puede contenter la respuesta.

### Simplificar las asociaciones. Utilizar el ? para ocultar la complejidad

**Asociaciones**

En todos los sistemas siempre hay asociaciones entre recursos. A tiene una lista de B, B tiene una instancia de C. Como podemos expresar estas asociaciones en las urls?

Seguiremos con nuestro ejemplo de los perros. Imaginemonos que tenemos dueños de estos perros. Queremos hacer una request a la API que me devuelva todos los perros de un dueño. Lo podemos representar de la siguiente manera

`GET /owners/1234/dogs`

Esto se puede leer de la siguiente manera: Del owner con id 1234, devolver todos los perros. Esto resulta intuitivo de leer y un usuario de la API lo puede entender simplemente de leer la URL.

Analogamente, para crear un perro para un owner especifico, podes utilizar la siguiente URL.

`POST /owners/1234/dogs`.

Es importante aclarar que hay que evitar llegar a demasiados niveles de relaciones "para adentro" ya que termina siendo confuso leerlo.

**Esconder complejidad detras del ?**

Generalmente, las APIs toman en cuenta varios otros atributos cuando se hace una request, como pueden ser filtros, parametros, etc. Por ejemplo, cuando se hace una consulta a  `GET /dogs`, se puede querer obtener  **solo**  los perros que tengan color rojo, y esten corriendo.

Estos parametros (color y state pueden ser llamados) deben ser enviados como parametros despues del  `?`, conocidos como query params.

Por ejemplo, la request puede ser asi:

`GET /dogs?color=red&state=running&location=park`

### Como se manejan operaciones que no estan relacionadas a recursos especificos?

Hasta ahora, estuvimos viendo como manejar recursos especificos.  `Dog`  es un recurso que representa una entidad en el sistema, que debe ser accedido, modificado, creado, etc.

Que pasa si tenemos que hacer algun tipo de calculo o funcion en nuestra API. Por ejemplo, hacer algun tipo de calculo financiero complejo, o hacer una traduccion de un lenguaje a otro. Ninguna de estas acciones se representa por un recurso. Estas acciones responden un resultado, no un recurso.

En este caso, es necesario usar verbos y no sustantivos. Es importante mantener estos verbos lo mas simple posible. Por ejemplo, si tendriamos que tener un endpoint para convertir de una moneda a otra, se podria hacer de la siguiente manera:

`/convert?from=EUR&to=CNY&amount=100`

Esto convierte 100 euros a yuanes.

Es importante que estos endpoints sean documentados correctamente, especificando sus parametros y su comportamiento. Dado que no es estandard, un usuario de la API no sabra como se comporta facilmente.

### Manejo de errores

El manejo de errores es un aspecto critico de una buena API. Es muy importante poder explicarle al usuario de la API porque una request fallo, y brindarle toda la información necesaria para que pueda solucionarlo.

**Usar HTTP Codes**

Es importante usar los HTTP codes en las situaciones adecuadas para seguir un standard. Existen sobre 70  [codigos](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes), aunque solo un subconjunto es utilizado comunmente.

_Cuantos usar?_

Si se analiza los posibles flujos que pueden haber cuando se ejecuta un endpoint, hay solo 3:

-   Todo anduvo correctamente - Exito
-   El usuario hizo algo mal - Error del cliente
-   La API hizo algo mal - Error de la API

Cada uno de estos se puede representar con los 3 siguientes codigos:

-   **200**  - OK
-   **400**  - Bad Request
-   **500**  - Server Error

A partir de esto, se pueden agregar los que se consideren necesarios.  **201 - Created**  es un codigo muy utilizado cuando se crea un elemento de un recurso.  **401 - Unauthorized**  tambien es muy utilizado, cuando el usuario no tiene permisos para realizar esa operacion.

**Retornar mensajes lo mas expresivos posibles**

Mientras mas expresivo y mas información se le brinde al usuario, mas facil sera de usar la API.

Siempre sera peor tener:

`{"code" : 401, "message": "Authentication Required"}`

que:

```
{
    "developerMessage" : "Verbose, plain language description of the problem for the app developer with hints about how to fix it.",
    "userMessage": "Pass this message on to the app user if needed.",
    "errorCode" : 12345,
    "more info": "http://dev.teachdogrest.com/errors/12345"
}

```

En el segundo ejemplo, se brinda informacion descriptiva, se sabe donde ir a buscar mas informacion sobre el error, y se brinda un mensaje que se le puede mostrar a un usuario.

## Como implementa esto  `ASP .Net Core Web API`

A continuacion vamos a ver como implementa todos estos conceptos ASP .NET Core Web API.

Cuando creamos un proyecto Web API de ASP .NET core, se nos crea el proyecto con un Controlador de ejemplo. Este no tiene ninguna funcionalidad "real" si no que es para mostrar como funciona y poder hacer una rapida prueba. Lo utilizaremos para explicar los conceptos principales.
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Homeworks.WebApi.Controllers
{
    [Route("api/[controller]")] // 2
    [ApiController] // 3
    public class ValuesController : ControllerBase // 1
    {
        // GET api/values
        [HttpGet] // 4
        public ActionResult<IEnumerable<string>> Get() // 5
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")] // 6
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) // 7
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
```
**--`public class ValuesController : ControllerBase`  // 1**

Lo primero que debemos explicar es que es un controlador. Un  `Controller`  es una clase que se encarga de definir cada una de las rutas con la cual se puede acceder a un recurso especifico. Se pueden tener varios en la aplicacion, y tienen que heredar de  `ControllerBase`. Una buena practica que hay que seguir es tener un  `Controller`  para cada uno de los recursos. Es decir, si nuestro sistema maneja  `Usuarios`  y  `Animales`, se debe tener un Controller que se encarge del manejo de  `Animales`, y otro Controller que se encarge del manejo de  `Usuarios`.

**--  `[Route("api/[controller]")]`  // 2**

La primera anotacion que veremos sera  `Route`. Esta se utiliza para indicar a que url debe responder este controlador. Es una anotacion que se pone encima de la clase. Es decir, si se pone  `[Route("api/testing")]`  encima de un controlador, todos los metodos de adentro del controlador seran llamados con la url  `{url}/api/testing/{resto la url}`, siendo resto de la url lo indicado (o no) por cada metodo.

En este caso, estamos utilizando  `[Route("api/[controller]")]`. Esto indica que se utilice el nombre del controlador dentro de la url. Por ejemplo, en este caso, el controlador se llama  `ValuesController`. La ruta que utilizara para este controlador sera  `{url}/api/values`. Basicamente, si el Controller se llama  `XController`, se remueve el controller y se utiliza  `{url}/api/X`.

**--  `[ApiController]`  // 3**

La siguiente anotacion es simple. Tambien se pone a nivel de clase y le indica a ASP .NET Core que esta clase es un controller.

**--  `[HttpGet]`  // 4**

La siguiente notacion que vemos es  `[HttpGet]`. Esta se pone a nivel de metodo, e indica que este metodo sera llamado cuando se utilice el verbo  `GET`  de HTTP en la url del controlador. En este caso, cuando se haga la request  `GET {url}/api/values`  se llamara al metodo  `Get()`.

Cada uno de estos metodos que definimos, al cual se accede mediante una ruta con un verbo HTTP, es lo que llamamos  **endpoint**. Un endpoint es un punto de entrada a la API, ya sea para recibir o enviar datos.

**--  `public ActionResult<IEnumerable<string>> Get()`  // 5**

Aca veremos el retorno de una funcion de un controlador. En una API,cuando se retorna de una funcion, existen varias cosas que se retornan.

-   Primero, los datos solicitados en si, como pueden ser los valores del  `Get()`  en este caso.
-   Segundo, se retorna un codigo HTTP que da mas informacion sobre el resultado. Este puede ser un 400 si es un error, un 200 si es exitoso, 404 no encontrado, y un sin fin mas de codigos HTTP que representan una cosa
-   Tercero y ultimo, se retornan headers de una request HTTP, los cuales se utilizan para brindar otra información.

Existen 4 retornos posibles para un metodo de un endpoint:

**Tipo especifico:**  Se retorna un tipo en particular, como por ejemplo,  `IEnumerable<string>`  o un  `string`. El resultado es transformado en un JSON y se envia con un codigo de ejecucion exitosa (200).

**IActionResult:**  `ASP .NET Core`  brinda varias ayudas para esto. Por ejemplo, existen varios metodos que retornan una respuesta adecuada, a la cual se le puede pasar la informacion que queremos devolver. Alguns de los mas usados de estos son:

-   `Ok(data)`  que retorna una respuesta exitosa con un codigo 200. La data enviada como parametro es transformada a JSON y devuelta (puede ser omitido y retornar vacio)
-   `NotFoundResult()`  que reotrna un codigo 404 por no haber encontrado un recurso
-   `BadRequestResult()`  que retorna un codigo 400, que hay un error en la request (puede ser por datos invalidos o cualquier otra razon). Si se pasa un string u objeto como parametro, sera enviado como respuesta.

**ActionResult:**

Funciona de manera muy similar, brindando mayor facilidad a la hora de generar las respuestas, ya que se puede retornar tanto un IActionResult como el tipo especifico T.

Se puede ver mas del retorno de una API  [aqui](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-2.2)

**--  `[HttpGet("{id}")]`  // 6**

Aca se puede ver como se puede definir la ruta del endpoint mediante la anotacion  `[HttpGet]`. El string que se le pasa como parametro se debe agregar a la ruta para llamar a este endpoint especifico.

Es decir, si tenemos un metodo con  `[HttpGet("another")]`  en un Controller que tiene  `[Route("api/testing")]`, para llamar al endpoint se debe usar la url  `GET {url}/api/testing/another`.

En este caso, vemos que en la url se encuentra  `{id}`. Cuando esta entre llaves  `{x}`  indica que este valor debe ser pasado como parametro a la funcion. La funcion debe recibir como parametro este valor, con el tipo adecuado y el mismo nombre que se definio en la url.

Por ejemplo, si se utiliza la url  `GET {url}/api/values/5`, se llamara a este metodo y se enviara como parametro a la funcion el valor 5.

**--  `public void Post([FromBody] string value)`  // 7**

Por ultimo, aca se puede ver como obtener la informacion que es enviada en el body de la request. Se le agrega el atributo  `[FromBody]`  a un parametro, y se intentara parsear el JSON del body, crear un elemento del tipo del parametro, y llamar a la funcion con el.

# Mas información

-   El libro API Design EBook, que se encuentra en el curso de aulas.
