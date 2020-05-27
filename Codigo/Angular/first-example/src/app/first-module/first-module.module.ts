import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyFirstComponent } from './components/my-first/my-first.component';
import { FormsModule } from '@angular/forms';
import { FirstModuleModuleService } from '../my-service/first-module-module.service';
import { SecondComponent } from './components/second/second.component';
import { FiltroPipe } from './filtro.pipe';



@NgModule({
  declarations: [MyFirstComponent, SecondComponent, FiltroPipe],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [MyFirstComponent],
  providers: [FirstModuleModuleService]
})
export class FirstModuleModule { }
