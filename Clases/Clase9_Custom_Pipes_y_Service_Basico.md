# Custom Pipes y Service Basico

## Custom Pipes: Filtrado en el listado de tareas

Como vimos la clase anterior (Clase 9), Angular provee un conjunto de Pipes que ya vienen integrados y que sirven para transformar los datos de nuestras bound properties antes de mostrarlos en el template (HTML). Ahora veremos como construir nuestros propios, Pipes personalizados, o *Custom Pipes*. 

El código necesario para crearlos seguramente a esta altura ya nos resulte familiar:

```typescript
import { Pipe, PipeTransform } from '@angular/core'; //0) importamos
import { Homework } from '../models/Homework';

//1) Nos creamos nuestra propia clase HomeworksFilterPipe y la decoramos con @Pipe
@Pipe({
  name: 'homeworksFilter'
})
export class HomeworksFilterPipe implements PipeTransform { //2) Implementamos la interfaz PipeTransform

  transform(list: Array<Homework>, arg: string): Array<Homework> { //3) Método de la interfaz a implementar
        //4) Escribimos el código para filtrar las tareas
        // El primer parametro 'list', es el valor que estamos transformando con el pipe (la lista de tareas)
        // El segundo parametro 'arg', es el criterio a utilizar para transfmar el valor (para filtrar las tareas)
        // Es decir, lo que ingresó el usuario
        // El retorno es la lista de tareas filtrada
  }
}
```

Como podemos ver, tenemos que crear una **clase**, y hacerla que implemente la interfaz **PipeTransform**. Dicha interfaz tiene un método **transform** que es el que será encargado de filtrar las tareas. A su vez decoramos la clase con un ```@Pipe``` que hace que nuestra clase sea un Pipe. Como notamos, la experiencia a la hora de programar en Angular es bastante consistente, esto es muy similar a cuando creamos componentes.
Tambien podemos lanzar el comando ```ng generate pipe "Nombre de la pipe"``` que nos da como resultado esto mismo.

Luego, para usar este CustomPipe en un template, debemos hacer algo así:

```html
<tr *ngFor='let homework of homeworks | HomeworksFilter:listFilter'> </tr>
```

Siendo:
- HomeworksFilter: el pipe que acabamos de crear.
- listFilter: el string por el cual estaremos filtrando.

Si quisieramos pasar más argumentos además del listFilter, los ponemos separados por ```:```.

También nos falta agregar el Pipe a nuestro módulo. Si queremos que el componente pueda usarlo, entonces debemos decirle a nuestro AppModule que registre a dicho Pipe. Siempre que queremos que un Componente use un Pipe, entonces el módulo del componente debe referenciar al Pipe. Lo haremos definiendo al Pipe en el array ```declarations``` del decorador ```ngModule``` de nuestro módulo.

Armemos el Pipe!

### 1) Creamos un archivo para el Pipe

Creamos en la carpeta ```app/homeworks-list```, un ``homeworks-filter.pipe.ts```, siguiendo nuestras convenciones de nombre.
O lanzamos ```ng generate pipe HomeworksFilter``` y movemos los archivos a la carpeta.

### 2) Agregamos la lógica del Pipe:

```typescript
import { Pipe, PipeTransform } from '@angular/core';
import { Homework } from '../models/Homework';

@Pipe({
  name: 'homeworksFilter'
})
export class HomeworksFilterPipe implements PipeTransform {

  transform(list: Array<Homework>, arg: string): Array<Homework> {
    return list.filter(
      x => x.description.toLocaleLowerCase()
        .includes(arg.toLocaleLowerCase())
    );
  }
}
```

### 3) Agregamos el filtrado en el template y sus estilos

Vamos a ```homeworks-list.component.html``` y donde usamos ```*ngFor```, agregamos el filtrado tal cual lo vimos arriba:

```html
<tr *ngFor="let aHomework of homeworks | homeworksFilter : listFilter">
```

### 4) Agregamos el Pipe a nuestro AppModule

Vamos a ```app.module.ts``` y agregamos el pipe:

```typescript
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HomeworksListComponent } from './homeworks-list/homeworks-list.component';
import { HomeworksFilterPipe } from './homeworks-list/homeworks-filter.pipe';
import { HomeworksService } from './services/homeworks.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeworksListComponent,
    HomeworksFilterPipe
  ],
  imports: [
    FormsModule,
    BrowserModule
  ],
  providers: [
    HomeworksService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
```
**Listo!** ya podemos filtar en nuestra lista!

## Servicios e Inyección de Dependencias

Los componentes nos permiten definir lógica y HTML para una cierta pantalla/vista en particular. Sin embargo, ¿qué hacemos con aquella lógica que no está asociada a una vista en concreto?, o ¿qué hacemos si queremos reusar lógica común a varios componentes (por ejemplo la lógica de conexión contra una API, lógica de manejo de la sesión/autenticación)?

Para lograr eso, construiremos **servicios**. Y a su vez, usaremos **inyección de dependencias** para poder meter/inyectar esos servicios en dichos componentes. 

Definiendo servicios, son simplemente clases con un fin en particular. Los usamos para aquellas features que son independientes de un componente en concreto, para reusar lógica o datos a través de componentes o para encapsular interacciones externas. Al cambiar esta responsabilidades y llevarlas a los servicios, nuestro código es más fácil de testear, debuggear y mantener.

Angular trae un ```Injector``` *built-in*, que nos permitirá registrar nuestros servicios en nuestros componentes, y que estos sean Singleton. Este Injector funciona en base a un contenedor de inyección de dependencias, donde una vez estos se registran, se mantiene una única instancia de cada uno.

Supongamos tenemos 3 servicios: svc, log y math. Una vez un componente utilice uno de dichos servicios en su constructor, el Angular Injector le provee la instancia del mismo al componente.

![image](../imgs/angular-clase3/13.png)

### Construyamos un servicio

Para armar nuestro servicio precisamos:
- Crear la clase del servicio.
- Definir la metadata con un @, es decir un decorador.
- Importar lo que precisamos.
¿Familiar? Son los mismos pasos que hemos seguido para construir nuestros componentes y nuestros custom pipes :)

### 1) Creamos nuestro servicio

Vamos a ```app/services``` y creamos un nuevo archivo: ```homeworks.service.ts```. 
O lanzamos ```ng generate service Homeworks``` y lo movemos a la carpeta

Luego, le pegamos el siguiente código:

```typescript
import { Injectable } from '@angular/core';
import { Homework } from '../models/Homework';
import { Exercise } from '../models/Exercise';

@Injectable()
export class HomeworksService {

  constructor() { }

  getHomeworks():Array<Homework> {
    return [
      new Homework('1', 'Una tarea', 0, new Date(), [
        new Exercise('1', 'Un problema', 1),
        new Exercise('2', 'otro problema', 10)
      ]),
      new Homework('2', 'Otra tarea', 0, new Date(), [])
    ];
  }
}
```

### 2) Registramos nuestro servicio a través de un provider

Para registrar nuestro servicio en nuestro componente, debemos registrar un Provider. Un provider es simplemente código que puede crear o retornar un servicio, **típicamente es la clase del servicio mismo**. Esto lo lograremos a través de definirlo en el componente, o como metadata en el Angular Module (AppModule).

- Si lo registramos en un componente, podemos inyectar el servicio en el componente y en todos sus hijos. 
- Si lo registramos en el módulo de la aplicación, lo podemos inyectar en toda la aplicación.

En este caso, lo registraremos en el Root Component (```AppModule```). Por ello, vamos a ```app.module.ts``` y reemplazamos todo el código para dejarlo así:

```typescript
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HomeworksListComponent } from './homeworks-list/homeworks-list.component';
import { HomeworksFilterPipe } from './homeworks-list/homeworks-filter.pipe';
import { HomeworksService } from './services/homeworks.service'; //importamos el servicio

@NgModule({
  declarations: [
    AppComponent,
    HomeworksListComponent,
    HomeworksFilterPipe
  ],
  imports: [
    FormsModule,
    BrowserModule
  ],
  providers: [
    HomeworksService //registramos el servicio para que este disponible en toda nuestra app
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

```

### 3) Inyectamos el servicio en nuestro HomeworksListComponent

La inyección la logramos a través del constructor de la clase, para ello hacemos en ```homeworks-list.component.ts```:

Primero el import:

```typescript
import { HomeworksService } from '../services/homeworks.service';
```
Y luego definimos el constructor que inyecta el servicio a la clase:
```typescript
constructor(private _serviceHomeworks:HomeworksService) { 
   // esta forma de escribir el parametro en el constructor lo que hace es:
   // 1) declara un parametro de tipo HomeworksService en el constructor
   // 2) declara un atributo de clase privado llamado _serviceHomeworks
   // 3) asigna el valor del parámetro al atributo de la clase
}
``` 

Eso el HomeworksService y lo deja disponible para la clase. Ahí mismo podríamos inicializar nuestras homeworks, llamando al ```getHomeworks``` del servicio, sin embargo, no es proljo mezclar la lógica de construcción del componente (todo lo que es renderización de la vista), con lo que es la lógica de obtención de datos. Para resover esto usarmoes Hooks particularmente, el ```OnInit``` que se ejecuta luego de inicializar el componente.

```typescript
ngOnInit(): void {
    this.homeworks = this._serviceHomeworks.getHomeworks();
}
```
Quedando, el código del componente algo así:
```typescript
import { Component, OnInit } from '@angular/core';
import { Homework } from '../models/Homework';
import { Exercise } from '../models/Exercise';
import { HomeworksService } from '../services/homeworks.service';

@Component({
  selector: 'app-homeworks-list',
  templateUrl: './homeworks-list.component.html',
  styleUrls: ['./homeworks-list.component.css']
})
export class HomeworksListComponent implements OnInit {
  pageTitle:string = 'HomeworksList';
  homeworks:Array<Homework>;
  showExercises:boolean = false;
  listFilter:string = "";

  constructor(private _serviceHomeworks:HomeworksService) { 
    
  }

  ngOnInit() {
    this.homeworks = this._serviceHomeworks.getHomeworks();
  }

  toogleExercises() {
    this.showExercises = !this.showExercises;
  }
}
```

