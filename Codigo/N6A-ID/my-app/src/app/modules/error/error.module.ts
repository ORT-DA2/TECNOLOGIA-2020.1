import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { AuthenticationErrorComponent } from './authentication-error/authentication-error.component';



@NgModule({
  declarations: [NotFoundComponent, AuthenticationErrorComponent],
  imports: [
    CommonModule
  ],
  exports: [NotFoundComponent]
})
export class ErrorModule { }
