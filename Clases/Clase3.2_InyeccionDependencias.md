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

## Inyección de Dependencias en Nuestra WebApi

* [**DI y Startup (IMPORTANTE)**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
* [Startup](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup)
