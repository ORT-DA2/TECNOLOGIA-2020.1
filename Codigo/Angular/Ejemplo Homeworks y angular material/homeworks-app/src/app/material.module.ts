import { NgModule } from '@angular/core';

import {
  MatListModule,
  MatInputModule,
  MatTableModule,
  MatSlideToggleModule,
  MatCardModule,
  MatMenuModule,
  MatFormFieldModule,
  MatButtonModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatToolbarModule
} from '@angular/material';

@NgModule({
  exports: [
    MatToolbarModule,
    MatListModule,
    MatInputModule,
    MatTableModule,
    MatSlideToggleModule,
    MatCardModule,
    MatMenuModule,
    MatFormFieldModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ]
})
export class MaterialComponentsModule {}
