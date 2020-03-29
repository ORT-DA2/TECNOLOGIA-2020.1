# Clase 3 -  Inyección de Dependencias (ID)

## ¿Qué es una dependencia?

En software, cuando hablamos de que dos piezas, componentes, librerías, módulos, clases, funciones (o lo que se nos pueda ocurrir relacionado al área), son dependientes entre sí, nos estamos refiriendo a que uno requiere del otro para funcionar. A nivel de clases, significa que una cierta **'Clase A'** tiene algún tipo de relación con una **'Clase B'**, delegándole el flujo de ejecución a la misma en cierta lógica.
Ej: **UserLogic** *depende de* **UserRepository**

##### Business Logic -> Repository

```c#
public class UserLogic : IUserLogic
{
    public IRepository<User> users;

    public UserLogic()
    {
        users = new new UserRepository();
    }
}
```

**¿Notaron el problema (común entre ambas porciones de código) que existe?**

El problema reside en que ambas piezas de código tiene la responsabilidad de la instanciación de sus dependencias. Nuestras capas no deberían estar tan fuertemente acopladas y no deberían ser tan dependientes entre sí. Si bien el acoplamiento es a nivel de interfaz (tenemos IUserLogic y IRepository), la tarea de creación/instanciación/"hacer el new" de los objetos debería ser asignada a alguien más. Nuestras capas no deberían preocuparse sobre la creación de sus dependencias.

**¿Por qué? ¿Qué tiene esto de malo?**:-1:

1. Si queremos **reemplazar** por ejemplo nuestro BreedsBusinessLogic **por una implementación diferente**, deberemos modificar nuestro controller. Si queremos reemplazar nuestro UserRepository por otro, tenemos que modificar nuestra clase UserLogic.

2. Si la UserLogic tiene sus propias dependencias, **debemos configurarías dentro del controller**. Para un proyecto grande con muchos controllers, el código de configuración empieza a esparcirse a lo largo de toda la solución.

3. **Es muy difícil de testear, ya que las dependencias 'están hardcodeadas'.** Nuestro controller siempre llama a la misma lógica de negocio, y nuestra lógica de negocio siempre llama al mismo repositorio para interactuar con la base de datos. En una prueba unitaria, se necesitaría realizar un mock/stub las clases dependientes, para evitar probar las dependencias. Por ejemplo: si queremos probar la lógica de UserLogic sin tener que depender de la lógica de la base de datos, podemos hacer un mock de UserRepository. Sin embargo, con nuestro diseño actual, al estar las dependencias 'hardcodeadas', esto no es posible.

Una forma de resolver esto es a partir de lo que se llama, **Inyección de Dependencias**. Vamos a inyectar la dependencia de la lógica de negocio en nuestro controller, y vamos a inyectar la dependencia del repositorio de datos en nuestra lógica de negocio. **Inyectar dependencias es entonces pasarle la referencia de un objeto a un cliente, al objeto dependiente (el que tiene la dependencia)**. Significa simplemente que la dependencia es encajada/empujada en la clase desde afuera. Esto significa que no debemos instanciar (hacer new), dependencias, dentro de la clase.

Esto lo haremos a partir de un parámetro en el constructor, o de un setter. Por ejemplo:

```c#
public class UserLogic : IUserLogic
{
    public IRepository<User> users;

    public UserLogic(IRepository<User> users)
    {
        this.users = users;
    }
}
```

Esto es fácil lograrlo usando interfaces o clases abstractas en C#. Siempre que una clase satisfaga la interfaz,voy a poder sustituirla e inyectarla.

## Ventajas de ID

Logramos resolver lo que antes habíamos descrito como desventajas o problemas.

1. Código más limpio. El código es más fácil de leer y de usar.
2. Nuestro software termina siendo más fácil de Testear. 
3. Es más fácil de modificar. Nuestros módulos son flexibles a usar otras implementaciones. Desacoplamos nuestras capas.
4. Permite NO Violar SRP. Permite que sea más fácil romper la funcionalidad coherente en cada interfaz. Ahora nuestra lógica de creación de objetos no va a estar relacionada a la lógica de cada módulo. Cada módulo solo usa sus dependencias, no se encarga de inicializarlas ni conocer cada una de forma particular.
5. Permite NO Violar OCP. Por todo lo anterior, nuestro código es abierto a la extensión y cerrado a la modificación. El acoplamiento entre módulos o clases es  siempre a nivel de interfaz.

## WebApi

Incorporando Inyección de dependencias en nuestra WebApi, para lograrlo debemos remover todos los constructores sin parámetros y agregar nuevos que reciban las dependencias que queremos que sean inyectadas.

### DataAccess: EJEMPLO

```csharp
public class HomeworkRepository
{
    public HomeworkRepository(DbContext context)
    {
        Context = context;
    }

    // RESTO DEL CODIGO
}
```

### BusinessLogic: EJEMPLO

```csharp
public class HomeworkLogic
{
    private readonly HomeworkRepository repositoryHome;

    public HomeworkLogic(HomeworkRepository homeworks)
    {
        repositoryHome = homeworks;
    }

    // RESTO DEL CODIGO
}
```

### WebApi Controller: EJEMPLO

```csharp
[ApiController]
[Route("api/[controller]")]
public class HomeworksController : ControllerBase
{
    private readonly HomeworkLogic homeworks;

    public HomeworksController(HomeworkLogic homeworks) : base()
    {
        this.homeworks = homeworks;
    }

    // RESTO DEL CODIGO
}
```

## WebApi: Startup

Por ultimo hay que decirle a la wab api como crear y que servicios crear para inyectar en nuestros controllers. 
Por ejemplo:
* Para instanciar el HomeworksController cuando alguien le solicite una request va a tener que pasar le un objeto de tipo HomeworkLogic por parámetro.
* Y a su vez para instanciar un HomeworkLogic tiene que instanciar un HomeworkRepository.
* Pero para instanciar el HomeworkRepository necesita instanciar un HomeworkContext.

P: ¿Entonces como le indicamos que servicios instanciar?

R: Se lo indicamos a traves del startup le decimos que servicios tiene esta disponibles para instanciar.

Para nuestro ejemplo de Homeworks:

```csharp
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // EN ESTE MÉTODO SE REGISTRAN LOS SERVICIOS
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        // REGISTRO EL CONTEXTO
        services.AddDbContext<DbContext, HomeworksContext>(
            o => o.UseSqlServer(Configuration.GetConnectionString("HomeworksDB"))
        );
        // REGISTRO EL REPOSITORIO Y SU LÓGICA
        services.AddScoped<HomeworkLogic, HomeworkLogic>();
        services.AddScoped<HomeworkRepository, HomeworkRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
```

### Explicación

* **REGISTRO EL REPOSITORIO Y SU LÓGICA**: ```services.AddScoped<Interfaz, Servicio>()```, este lo que haces es registra un servicio de tipo ```Servicio``` y lo va a instanciar e inyectar cuando alguien necesite una interfaz de tipo ```Interfaz```.

* **REGISTRO EL CONTEXTO**: El DbContext se registra de una manera especial, utilizando el siguiente método:
```csharp
// Le indicamos que Contexto (DbContexto) va a inyectar para cada interfaz (DbInterfaz)
services.AddDbContext<DbInterfaz, DbContexto>(
    // Configuración del contexto se hace aqui
    // En particular le vamos a indicar que bd vamos a usar en este caso
    // SqlServer y cual es el ConnectionString que va a usar.
    // en este caso el connection string va a estar en un archivo de 
    // configuración llamado appsettings.json
    o => o.UseSqlServer(Configuration.GetConnectionString("REFERENCIA_AL_ARCHIVO_DE_CONFIGURACIÓN"))
);
```

Explicacion del ejemplo:

```csharp
// En este caso le indicamos que cuando vea la interfaz DbContext para inyectar que instancie e inyecte el contexto: HomeworksContext
services.AddDbContext<DbContext, HomeworksContext>(
    // Por ultimo le indicamos que la BD va a ser MSSQL y que el
    // ConnectionString lo va a extraer de una archivo de configuración (appsettings.json) y que tome el valor de la key "HomeworksDB"
    o => o.UseSqlServer(Configuration.GetConnectionString("HomeworksDB"))
);
```

Archivo de configuración del ejemplo (appsettings.json):

```json
{
  "ConnectionStrings": {
    "HomeworksDB": "Server=.\\SQLEXPRESS;Database=HomeworksDB;Trusted_Connection=True;MultipleActiveResultSets=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

Este archivo de configuración se encuentra dentro del proyecto WebApi y es generado por defecto cuando se crea un tipo de proyecto WebApi.

## Inyección de Dependencias en Nuestra WebApi

* [**DI y Startup (IMPORTANTE)**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
* [Startup](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup)
