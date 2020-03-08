# Introducción

Durante el transcurso del curso se contribuirá una aplicación completa vista desde la perspectiva de sus componentes, es decir, se va desarrollar un back-end con funcionalidad y base de datos, y también, se va generar una SPA (Single Page Application) para que el usuario pueda utilizar las funcionalidades provistas por el servidor.

Para este curso se divide la construcción en dos partes:

 **La primera** es la construcción de la API (Application Programming Interface) REST la cual se creará utilizando [.Net Core](https://dotnet.microsoft.com) y [WebApi.](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code) 
Cabe hacer énfasis que en esta parte no vamos a tener una GUI de usuario, sino que haremos el servicio y lo probaremos utilizando una aplicación para ello llamada [Postman](https://www.postman.com).

**La segunda** es la construcción de la aplicación web con la que el usuario podrá utilizar el sistema. La misma se realizará en en [Angular](https://angular.io).

# Stack de tecnologías a emplear
## Visual Studio Code

Editor [Visual Studio Code](https://code.visualstudio.com). Es un editor de texto que soporta todo lo que requerimos para el desarrollo del trabajo tanto en C# (Back-end) como para TypeScript (Front-end). Es posible agregarle algunas extensiones y completar su funcionalidad. 

Links interesantes:

 - [Arrancar con Visual Studio Code](https://code.visualstudio.com/docs/introvideos/basics).
 - [C# for Visual Studio](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) Extensión que facilita el desarrollo en C# con Visual Studio Code.
 - [TSLint]([https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-typescript-tslint-plugin](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-typescript-tslint-plugin)) Extensión que facilita el desarrollo con TypeScript en Visual Studio Code.
 

## Base de datos
En el curso vamos usar el motor de base de datos [MS SQL 2017](https://www.microsoft.com/es-es/sql-server/sql-server-2017). De esa versión en delante funciona Windows, Linux y MacOS (para estos dos últimos tal vez requiera alguna configuración adicional).

## Framework
Para lo que es la codificación del **backend** vamos a usar la última versión de [.Net Core](https://dotnet.microsoft.com).

Para lo que es la codificación del **frontend** vamos a usar  [Angular](https://angular.io).

Para que nos funcione Angular es necesario tener [Node](https://nodejs.org/es/download/) instalado en la versión al mínimo 10.15.1. LTS.

## Gestión del código
Es inprescindible tener GIT instalado en el equipo ya que es el repositorio que vamos a utilizar.

-Se consigue acá: [GIT](https://git-scm.com) y se puede usar línea de comandos o aplicación con GUI. **En la que mejor trabajemos**.


# Conceptos que se necesitan desde el día 0

A nivel teórico y tecnológico todo lo que se dio en Diseño de Aplicaciones 1 se entiende como ya "dado" en este curso y por ello, los aspectos centrales deben estar claros: 
	

- Buenas prácticas de codificación y diseño, a modo de ejemplo Clean Code, principios SOLID y los GRASP, codificación con TDD y Refactoring.
- UML 
- Como se codifica en C#
-  Se necesita tener claro el concepto de ORM y lo visto para Entity Framework. En particular vamos a usar [EF Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx), si no se acuerdan o no conocen, pueden revisando eso.
- Conocimientos adquiridos en asignaturas de Ingeniería de Software tales como elaborar documentación, tener una organización mínima en el proyecto, etc.


