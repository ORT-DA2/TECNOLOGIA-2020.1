# Componentes, Templates, Bootstrapping, Directivas, Data Binding y Pipes

## Repasamos el concepto de Componente

### Fundamentos de los componentes

Cada componente la idea es que funcione armoniosamente y en conjunto con el resto para proveer una experiencia de usuario única. Como dijimos, estos son modulares, resuelven un problema concreto y colaboran entre sí para lograr ir armando la interfaz de usuario como un puzzle donde cada pieza tiene sus diferentes responsabilidades.

Por ejemplo, una excelente forma de pensar los componentes es a través de la siguiente imagen:

![imagen](../imgs/angular-clase2/angular-components-sample-2.png)

A su vez, es interesante recordar cómo se comporta internamente cada componente. Como habíamos dicho, los componentes se componen de tres cosas:

1) **Template:** El **template** del componente, el cual define la estructura (HTML o la vista) del mismo. Se crea con código html y define lo que se renderizará en a página. A su vez incluye *bindings* y *directivas* para darle poder a nuestra vista y hacerla dinámica. Estos dos conceptos los veremos más adelante en este módulo

2) **Clase:** A su vez la view tiene un código asociado, el cual llamamos la **clase** de un componente. Esta representa el código asociado a la vista (creada con TypeScript), la cual posee los *datos*, llamadas las *properties* para que la vista los use (el nombre de una mascota por ejemplo), y a su vez posee la *lógica/funciones* que usan los mismos. Por ejemplo: la lógica para mostrar o esconder una imagen, la lógica para traer datos desde una Api, etc.

3) **Metadata:** Finalmente, el componente también tiene **metadata**, que es información adicional para Angular, siendo esta definida con un *decorator* (los que arrancan con **@**). Un decorador es una función que agrega metadata a una clase, sus miembros o los argumentos de sus métodos.

![imagen](../imgs/angular-clase2/angular_component_architecture.png)

### La clase de un componente:

Para la clase de un componente, la sintaxis es como sigue a continuación:

```typescript
export class NombreComponent {
  property1: tipo = valor,
  property2: tipo2 = valor2,
  property3: tipo2 = valor3
  ...
}
```

Pero a su vez necesitamos utilizar el decorador ```Component```, el cual lo debemos importar desde ```@angular/core```:

```typescript
import { Component } from '@angular/core';

@Component({
  selector: 'nombre-component',
  template: `<h1>Hello {{property1}}</h1>`
})
export class NombreComponent {
  property1: tipo = valor,
  property2: tipo2 = valor2,
  property3: tipo2 = valor3
  ...
}
```

También podemos definir un constructor:

```typescript

export class NombreComponent {
  property1: string;
  property2: number;

  constructor(property1:string, property2: number) {
    this.property1 = property1
    this.property2 = property2;
  }
}

```

Algunos otros conceptos a tener en cuenta:

- Recordemos que por convención, el componente fundamental de una app de angular se llama AppComponent (el root component).
- La palabra reservada “export” simplemente hace que el componente se exporte y pueda ser visto por otros componentes de nuestra app.
- La sintaxis de definición del archivo es ```nombre.component.ts```.
- El valor por defecto en las properties de nuestros componentes es opcional.
- Los métodos vienen luego de las properties, en lowerCamelCase.

### El Template y la Metadata de un componente

Sin embargo, como ya sabemos, las clases de los componentes no son lo único necesario que precisamos para armar nuestra app en angular, precisamos darle el HTML, la vista, la UI. Todo eso y ms lo definimos a través del **metadata** del componente.

Una clase como la que definimos anteriormente se convierte en un componente de Angular cuando le definimos la metadata de componente.

Angular precisa de esa metadata para saber como instanciar el componente, estructurar la view, y ver la interacción:

- Usamos un decorator, siendo el scope de tal decorator la feature que decora.	Siempre son prefijos con un ```@```.
- Angular tiene un conjunto predefinido de decoradores que podemos usar, y hasta podemos crearnos los propios.
- El decorator va siempre antes de la definición de la clase, como las DataAnnotations en .NET (no va ;)
- Es una función y recibe un objeto que define el template.

En otras palabras el decorator ```@Component``` indica que la clase subyacente es un Componente de Angular y recibe la metadata del mismo (en forma de un objeto JSON de configuración). Aquí van algunas de las opciones de configuración más útiles para ```@Component```:

1) **selector**: Es un selector de CSS que le dice a Angular que cree e inserte una instancia de este componente en donde encuentre  un tag  ```<nombre-component>``` en su HTML padre. Por ejemplo, si el HTML  de una app contiene contains ```<nombre-component></nombre-component>```, entonces Angular inserta una instancia de la view asociada a ```NombreComponent``` entre esos dos tags.

Especifica el nombre de la directiva que vamos a usar con el componente. Es simplemente un tag HTML custom que vamos a poder usar.

2) **template**: Representa el código HTML asociado al componente y que debe ser mostrado cada vez que se use su selector. Es la UI para ese componente.

3) **templateUrl**: Similar al anterior pero permite referenciar a un documento HTML en lugar de tener que escribirlo directamente en el código del componente (puedo ponerlo en un archivo separado y tratarlo simplemente como un HTML.

4) **providers**: es un array de objeto de los providers de los servicios que el componente requiere para operar. Estos se inyectan a partir de inyección de dependencias; es simplemente una forma de decirle a Angular que el constructor del componente requiere algúns servicio para funcionar. Ejemplo:

![imagen](../imgs/angular-clase2/angular-component-code-sample.png)

## Importando o exportando módulos y componentes

Antes de usar una función o clase externa, tenemos que decirle al module de dónde lo puede sacar. Eso lo hacemos con el statement ```import```. Este statemente es parte de ES2015 y es implementado en TypeScript, funcionando como el import de java o el using de c#.

En consecuencia, nos permite usar todos los miembros que hayan sido exportados por algún ES Module (sea una librería de terceros, nuestros propios ES modules o modelos propios de Angular).

![imagen](../imgs/angular-clase2/import-sample.png)

Esto se debe a que angular es modular, define una colección de módulos que engloban funcionalidad. Cuando precisemos algo de angular lo tomaremos de un angular module.

![imagen](../imgs/angular-clase2/angular-is-modular.png)

Para ver todos los módulos disponibles de angular:

www.npmjs.com/~angular 

Por ejemplo, en el código de nuestros componentes, decoramos los mismos con la función Component que viene dentro de angular core, para poder definir nuestra clase como nuestra component.

```typescript 
import  { Component } from ‘@angular/core’
```

Si queremos poner importar más de una clase desde dicho módulo, simplemente debemos separarlos por coma dentro de las llaves que.

## Bootstrapping o proceso de inicio de nuestra aplicación

¿Cómo le decimos a Angular que debe cargar nuestro componente principal? Le decimos a Angular que cargue nuestro AppComponent a través de un proceso que se llama ***Bootstrapping***. Siendo el index.html quien  hostea nuestra app.

Podemos basarnos en varios mecanismos para levantar nuestros módulos. Particularmente utilizaremos ```System.js```, para hacer que el componente principal (el root component) de nuestra app se cargue. Para leer más sobre System.js: https://github.com/systemjs/systemjs

Y como Angular puede ser 'bootstrapped' en múltiples ambientes (como server-side), necesitamos importar un módulo específico para el ambiente en el que queremos 'bootstrappear' nuestra app (el navegador). Para levantar/bootstrappear en el navegador, necesitamos importar un módulo particular.

Cuando nuestra aplicación corre en el browser, se debe marcar una forma específica de hacer el boostraaping la cual es definida en ```@angular/platform-browser-dynamic```. Este módulo contiene, en simples plalabras, las features para que nuestra app precisa para corra en el navegador.

### Detalle de este proceso

Eso lo que hace es simplemente levantar nuestro módulo ES. En nuestro caso, es el AppModule, el cual registra el AppComponent. 

Veamos con detalle este proceso:

1) El primer paso es que el navegador solicite al servidor donde se encuentra hosteadom la web el ```index.html```.Recordemos que tenemos una SPA, por ende tenemos un solo HTML, el index.html la cual contiene la página principal de la aplicación (en general es la única).

2) Este va a cargar nuestra app utilizando el selector que indicamos para nuestro componente, como una nueva directiva en el body de nuestro HTML

```html
<body>
  <my-app>Loading AppComponent content here ...</my-app>
</body>
```

3) Pero cómo encuentra nuestro Root Application Component? (AppComponent)?

Esto lo hace a partir de nuestro Systemjs (nuestro module loader), que cargamos desde el index.html ```System.import("main.js")```. El main.ts bootrstrapea nuestro primer angular module, desde donde arranca nuestra app.

```html
<script>
    System.import('main.js').catch(function(err){ console.error(err); });
</script>
```

Queremos que el angular compiler compile nuestra app en el browser dinámicamente, y que luego corra la aplicación, por eso importamos ```platformBrowserDynamic```.

```typescript
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

platformBrowserDynamic().bootstrapModule(AppModule);
```

Luego se llama a una función que lo que hace es bootstrappear nuestro angular module principal (root).

Más sobre el proceso de Bootstrapping: https://toddmotto.com/bootstrap-angular-2-hello-world 

## Data Binding e Interpolación

Como mencionamos anteriormente, queremos que cada componente tenga asociada una cierta vista (HTML), sin embargo, queremos que los datos que se muestran en la misma sean dinámicos, y que vengan desde las properties de la clase del componente. No queremos hardcodear el HTML que representa los datos a mostrar. Por ejemplo:

No queremos:

```html
<div class='panel-heading'>
    Nombre de la página que puede cambiar
</div>
```

Queremos:

```html
<div class='panel-heading'>
    {{pageTitle}}
</div>
```

```typescript
export class MyComponent {
  pageTitle: string = "Nombre de la página que puede cambiar"
  constructor(pageTitle : string)
  {
    this.pageTitle = pageTitle;
  }
}
```

Lo que se ve en el código anterior es el concepto de **Data Binding**, es decir, "el enlace" existente entre una porción de UI y ciertos datos de una clase de un componente. En otras palabras, estamos diciendole a la UI que mire el valor de la property ```pageTitle``` de la clase. Si dicha property cambia, entonces el valor mostrado por la UI cambia.
 
De manera que cuando el HTML se renderiza, el HTML muestra el valor asociado al modelo pageTitle.

El Data Binding va de la mano del concepto de **interpolación**, la cual es la habilidad de poner datos dentro de un HTML (interpolar). Esto es lo que logramos con las llaves dobles ``` {{ ... }} ```.

![imagen](../imgs/angular-clase2/angular_data_binding.png)

La interpolación no es el único tipo de Data Binding, también hay otros:

- **Property Binding**: cuando el binding es hacia una property particular y no  a una expresión compleja como puede ser la interpolación. Setea el valor de una property a un a expresíon en el template. Ver sintaxis en imagen de abajo.

- **Event Binding**: es binding hacia funciones o métodos que se ejecutan como consecuencia de eventos (por ejemplo: un click sobre un elemento).

- **Two-Way Binding**: Es un idea y vuelta entre el template y una property entre un component. Muestra el valor de la property en la vista, y si en la vista/template dicho valor cambia, la property también se ve reflejada (por eso es de dos caminos). Esto lo veremos  con ms detalle en el tutorial de más abajo.

![imagen](../imgs/angular-clase2/binding-types.png)

## Directivas

A su vez, también podemos enriquecer nuestro HTML a partir de lo que se llaman **directivas**, pudiendo agregar **ifs** o **loops** (estilo for), sobre datos en nuestro HTML y generar contenido dinámicamente.

Una directiva es un elemento custom del HTML que usamos para extender o mejorar nuestro HTML. Cada vez que creamos un componente y queremos renderizar su template, lo hacemos a través de su *selector* asociado, el cual define la directiva del componente.

Pero a su vez angular también tiene algunas directivas built-in, sobre todo las *structural directives*. Por ejemplo: ***ngIf** o ***ngFor** (los asteriscos marcan a las directivas como que son estructurales).

## Tutorial: nuestro primer Component 
En este tutorial veremos la creación de un componente, agregarlo a nuestro módulo principal, trabajaremos con templates, data binding, interpolación y directivas.
** Para ello haremos un listado de tareas. **

### 1. Instalamos Bootstrap

Instalamos la librería de Twitter Bootstrap (nos da estilos y nos permite lograr diseños responsive de forma simple).
Para ello, parados sobre nuestro proyecto usamos npm para descargarla (recordemos que npm es como Nuget pero para librerías o módulos de JavaScript):

```
npm install bootstrap@3 --save
```
El --save lo que hace es guardar la referencia a este módulo en el package.json

Vemos como se impacta el package.json
```json
"dependencies": {
    "@angular/animations": "^6.1.0",
    "@angular/common": "^6.1.0",
    "@angular/compiler": "^6.1.0",
    "@angular/core": "^6.1.0",
    "@angular/forms": "^6.1.0",
    "@angular/http": "^6.1.0",
    "@angular/platform-browser": "^6.1.0",
    "@angular/platform-browser-dynamic": "^6.1.0",
    "@angular/router": "^6.1.0",
    "bootstrap": "^3.3.7", //aca aparecio boostrap
    "core-js": "^2.5.4",
    "rxjs": "~6.2.0",
    "zone.js": "~0.8.26"
  },
```
Y en el ```angular.json``` agregamos (en projects/:NOMBRE DEL PROJECTO:/styles) para poder usarlo en nuestra aplicacion.
```json
"styles": [
  "node_modules/bootstrap/dist/css/bootstrap.min.css",
  "src/styles.css"
],
```
### 3. Creamos nuestro componente

Para eso lanzaremos el siguiente commando ```ng generate component HomeworksList```
Una carpeta llamada homeworks-list, con 4 archivos:
- homeworks-list.component.spec.ts (Archivo con pruebas) (Se puede tanto eleminar como mover a la carpeta e2e)
- homeworks-list.component.ts (Archivo con la clase del componente y la metadata)
- homeworks-list.component.html (Archivo que contiene la vista)
- homeworks-list.component.css (Archivo que contiene el css del componente)
Este commando nos agrega el componente automaticamnte al ```app.module.ts``` dentro del array declarations
```json
{
  declarations: [
    AppComponent,
    HomeworksListComponent,
    HomeworksFilterPipe,
    
  ],
....
```

### 3. Agregamos el html.
Agregamos en nuestro archivo de vista (```homeworks-list.component.html```) nuestro template basico.

![imagen](../imgs/angular-clase2/templates-types.png)

Particularmente utilizaremos la propiedad ```templateUrl``` luego en nuestro componente:
```html
<div class='panel panel-primary'>
    <div class='panel-heading'>
        Homeworks List
    </div>

    <div class='panel-body'>
        <!-- Aca filtramos las tareas  -->
        <!-- Selector de filtro:  -->
        <div class='row'>
            <div class='col-md-2'>Filter by:</div>
            <div class='col-md-4'>
                <input type='text' />
            </div>
        </div>
        <!-- Muestra filtro:  -->
        <div class='row'>
            <div class='col-md-6'>
                <h3>Filtered by: </h3>
            </div>
        </div>

        <!-- Mensaje de error -->
        <div class='has-error'> </div>

        <!--Tabla de tareas -->
        <div class='table-responsive'>
            <table class='table'>
                <!--Cabezal de la tabla -->
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Description</th>
                        <th>DueDate</th>
                        <th>Score</th>
                        <th>
                            <button class='btn btn-primary'>
                               Show Exercises
                            </button>
                        </th>
                    </tr>
                </thead>
                <!--Cuerpo de la tabla-->
                <tbody>
                    <!-- Aca va todo el contenido de la tabla  -->
                </tbody>
            </table>
        </div>
    </div>
</div>
```
### 4. El código del componente

Modificamos el archivo ```homeworks-list.component.ts``` y le agregamos el siguiente código:
```typescript
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-homeworks-list',
  templateUrl: './homeworks-list.component.html',
  styleUrls: ['./homeworks-list.component.css']
})
export class HomeworksListComponent implements OnInit {
    pageTitle:string = "Homeworks List"
    
    constructor() { }

    ngOnInit() {
    }
}
```

### 5. Agregamos el componente nuevo a través de su selector.
Lo que haremos aquí es usar el selector ```app-homeworks-list``` en el root component, es decir el AppComponent.

De manera que en ```app.component.ts``` quedaría algo como:
```typescript
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    title:string = 'Homeworks Angular';
    name:string = "Santiago Mnedez";
    email : string = "santi17mendez@hotmail.com";
    address = {
        street: "la dirección del profe",
        city: "Montevideo",
        number: "1234"
    }
}
```
Y nuestro ```app.component.html```:
```html
<div style="text-align:center">
    <h1>Bienvenidos a {{title}}</h1>
    <h2>Curso de DA2 de {{name}}</h2>
    <p> <strong>Email:</strong> {{email}} </p>
    <p> <strong>Dirección:</strong> {{address.street}} {{address.number}} de la ciudad - {{address.city}} </p>
</div>
<app-homeworks-list></app-homeworks-list>
```
Sin embargo, con esto no basta, ya que para que un componente pueda usar a otro componente (a través de su selector), estos deben pertenecer al mismo módulo, o el módulo del componente que importa debe importar al mdulo del otro componente.

En consecuencia, vamos a ```app.module.ts```, y aseguraros que se encuentre el import:
```typescript
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HomeworksListComponent } from './homeworks-list/homeworks-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeworksListComponent,
  ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
```
¿Como hace el componente para saber a dónde buscar el component? Cómo ya dijimos, ahora lo encuentra porque pertenecen al mismo modulo. El módulo que sea dueño de este component es examinado para encontrar todas las directivas que pertenecen al mismo.

### 5. Usando Data Binding para mostrar datos dinmicos

Tenemos una pequeña tabla que muestra cosas, pero todavía no tiene ningún tipo de interacción, por lo que comenzaremos a explorar más a fondo las features del data binding que nos permiten manejar eventos y user input.

Ahora lo que queremos es ver poner contenido dinámico en nuestro componente. Para ello, repasemos el concepto de **Binding**. Este es el mecanismo que tiene Angular para coordinar los datos que existen en la clase de nuestro componente con su template, es decir en cómo se pasan los datos entre uno y otro.

La sintaxis del binding siempre se define en el template, a partir de lo que ya sabemos que se llama **interpolación**

![imagen](../imgs/angular-clase2/interpolacion-ejemplo.png)

La interpolación soporta mucho más que el mostrado properties simples, también permite realizar operaciones complejas o cálculos, o llamar métodos!

Hacer cambio en el  ```homeworks-list.component.html``` y poner:

```
 <div class='panel-heading'>
     {{pageTitle}}
 </div>
```
Veamos que pasa.

### 6. Utilizando *ngIf para elegir hacer algo o no
En el template, cambiamos ```<table clas="table">``` por lo siguiente:

```html
<table class='table' *ngIf='homeworks && homeworks.length'>
```
Esto todava no va a tener resultado hasta que en el paso siguiente agreguemos la property 'homeworks'.

### 7. Utilizando *ngFor para iterar sobre elementos de forma dinamica

Ahora crearemos la clase Exercise y Homeworks (en una carpeta llamada models), y agregaremos la propiedad 'homeworks' a nuestro componente.

Clase Exercise en la carpeta models:
```typescript
export class Exercise {
    id: string;
    problem: string;
    score: number;

    constructor(id:string = "", problem:string = "", score:number = 0) {
        this.id = id;
        this.problem = problem;
        this.score = score;
    }
}
```
Clase Homework en la carpeta models:
```typescript
import { Exercise } from './Exercise';

export class Homework {
    id: string;
    description: string;
    dueDate: Date;
    score: number;
    exercises: Array<Exercise>;

    constructor(id:string, description:string, score:number, dueDate:Date, exercises: Array<Exercise>){
        this.id = id;
        this.description = description;
        this.score = score;
        this.dueDate = dueDate;
        this.exercises = exercises;
    }
}
```
Nuestro componente:
```typescript
import { Component, OnInit } from '@angular/core';
import { Homework } from '../models/Homework';
import { Exercise } from '../models/Exercise';

@Component({
  selector: 'app-homeworks-list',
  templateUrl: './homeworks-list.component.html',
  styleUrls: ['./homeworks-list.component.css']
})
export class HomeworksListComponent implements OnInit {
    pageTitle:string = "Homeworks List";
    homeworks:Array<Homework> = [
        new Homework("1", "Una tarea", 0, new Date(), [new Exercise("1", "Un Problema", 0)]),
        new Homework("2", "Otra tarea", 0, new Date(), [])
    ];

    constructor() { }

    ngOnInit() {
    }
}
```
Y en el template cambiamos el ```<tbody>``` por lo siguiente:
```html
<tbody>
  <tr *ngFor='let aHomework of homeworks'>
      <td>{{aHomework.id}}</td>
      <td>{{aHomework.description  | uppercase}}</td>
      <td>{{aHomework.dueDate}}</td>
      <td>{{aHomework.score}}</td>
      <td>
          <div>
              <table>
                  <thead>
                      <tr>
                          <th>Problem</th>
                          <th>Score</th>
                      </tr>
                  </thead>
                  <tbody>
                      <tr *ngFor='let aExercise of aHomework.exercises'>
                          <td>{{aExercise.problem}}</td>
                          <td>{{aExercise.score}}</td>
                      </tr>
                  </tbody>
              </table>
          </div>
      </td>
  </tr>
</tbody>
```

### 7. Agregando Two-Way Binding:
En nuestro HomeworksListComponent, agregamos la property listFilter:
```typescript
listFilter: string;
```
En el template asociado, reemplazamos los dos primeros divs de class "row" que aparecen:
```html
<div class='row'>
    <div class='col-md-2'>Filter by:</div>
    <div class='col-md-4'>
        <input type='text' [(ngModel)]='listFilter' />
    </div>
</div>
<div class='row' *ngIf='listFilter'>
    <div class='col-md-6'>
        <h3>Filtered by: {{listFilter}} </h3>
    </div>
</div>
```
Vemos que no nos anda.

Para ello vamos al ```app.module.ts``` y agregamos el import a FormsModule:
```typescript
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeworksListComponent } from './homeworks-list/homeworks-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeworksListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
```
Nos faltaría ver cómo agregar la condición de filtro al for (esto lo haremos la clase que viene ya que es más complejo).

Esto que hicimos se llama **Two-Way Binding**.

![imagen](../imgs/angular-clase2/two%20way%20data%20binding%20in%20angular%202.png)

Two-Way Binding es simplemente un mecanismo a partir del cual podemos establecer un enlace entre nuestros datos (properties), y una expresión en un template; de manera que cada vez que desde la UI se modifique dicho valor, el valor de la property cambia, y viceversa.

### 7. Usando Pipes en Angular

Cuando los datos no están en el formato apropiado que queremos para mostrarlos, usamos Pipes. Estos permiten aplicar cierta lógica sobre las properties de nuestra clase antes de que estas sean mostradas (por ejemplo para que sean más user friendly). Angular ya provee varios Pipes built-in para diferentes tipos de datos (date, number, decimal,json, etc), e incluso permite crear pipes propios para realizar lógica particular como lo es manejar el filtrado. Esto lo haremos en la próxima clase.
Los pipes en general se denotan con el caracter ```|``` (pipe), expresión.
Por ahora, nos quedamos con pipes simples, como los de la imagen:

![imagen](../imgs/angular-clase2/pipes-sample.png)

Para ello, simplemente cambiamos:
```html
<td>{{aHomework.description  | uppercase}}</td>
... // o
<td>{{aHomework.description  | lowercase}}</td>
```

## 8. Agregando Event Binding para los Exercises
Usaremos **Event Binding** para mandar información al revés de los tipos de Data Binding como la Interpolación o el Property Binding. En lugar de que el las properties de nuestra clase manden datos al template (o vista), esta vez será el template o vista quien se comunicará con la clase. Esto lo hace a partir de responder a eventos del usuario, por ejemplo un click, un mouse over, un copy o paste, un scroll, un tecleo, etc.
La información de eventos disponibles que podemos usar se encuentra bien documentada en:
https://developer.mozilla.org/en-US/docs/Web/Events
La idea es que nuestros componentes estén funcionando como "listeners" a las acciones del usuario, usando Event Binding, y pudiendo ejecutar cierta lógica particular.

La sintaxis es por ejemplo:
```html
<button (click)='hacerAlgoCuandoOcurreClick()'> </button>
```
- El nombre del evento va entre paréntesis.
- La lógica a ejecutar va entre comillas simples luego del igual.

Lo que haremos ahora es la lógica del mostrado de imagenes con Event Binding, para ello:
En ```homeworks-list.component.ts```, agregamos la siguiente property a la clase:
```typescript
showExercises:boolean = false;
```
A su vez agregamos la siguiente función:
```typescript
toggleExercises(): void {
    this.showExercises = !this.showExercises;
}
```
Quedando:
```typescript
import { Component, OnInit } from '@angular/core';
import { Homework } from '../models/Homework';
import { Exercise } from '../models/Exercise';

@Component({
  selector: 'app-homeworks-list',
  templateUrl: './homeworks-list.component.html',
  styleUrls: ['./homeworks-list.component.css']
})
export class HomeworksListComponent implements OnInit {
    pageTitle:string = "Homeworks List";
    listFilter:string = "";
    showExercises:boolean = false;
    homeworks:Array<Homework> = [
        new Homework("1", "Una tarea", 0, new Date(), [new Exercise("1", "Un Problema", 0)]),
        new Homework("2", "Otra tarea", 0, new Date(), [])
    ];

    constructor() { }

    ngOnInit() {
    }

    toggleExercises(): void {
        this.showExercises = !this.showExercises;
    }

}
```
Y en el template hacemos estos dos cambios:
1) En cada click al botón que tenemos en el header de la tabla, llamamos a la función ```toggleExercises()```:
```html
<button (click)='toggleExercises()'class='btn btn-primary'>
    {{showExercises ? 'Hide' : 'Show'}} Exercises
</button>
```
2) En el mostrado de los Exercises, agregamos la condición de que solo se muestre si la property lo indica.
```html
<div *ngIf='showExercises'> //agregamos esta linea
    <table>
        <thead>
            <tr>
                <th>Problem</th>
                <th>Score</th>
            </tr>
        </thead>
        <tbody>
            <tr *ngFor='let aExercise of aHomework.exercises'>
                <td>{{aExercise.problem}}</td>
                <td>{{aExercise.score}}</td>
            </tr>
        </tbody>
    </table>
</div>
```
