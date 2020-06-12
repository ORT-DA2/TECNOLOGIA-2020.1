import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { FootBarComponent } from './foot-bar/foot-bar.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [NavBarComponent, SideBarComponent, FootBarComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[NavBarComponent,SideBarComponent,FootBarComponent]
})
export class NavigationModule { }
