import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FirstModuleModule } from './first-module/first-module.module';
import { EjemploPipe } from './ejemplo.pipe';

@NgModule({
  declarations: [
    AppComponent,
    EjemploPipe,
  ],
  exports: [],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FirstModuleModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
