import { HomeworkDetailGuard } from './homework-detail.guard';
import { HomeworksService } from './services/homeworks.service';
import { MaterialComponentsModule } from './material.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HomeworkListComponent } from './homework-list/homework-list.component';
import { FormsModule } from '@angular/forms';
import { HomeworksFilterPipe } from './homework-list/homeworks-filter.pipe';
import { StarComponent } from './star/star.component';
import { HomeworkDetailComponent } from './homework-detail/homework-detail.component';

import { NewHomeworkComponent } from './new-homework/new-homework.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeworkListComponent,
    HomeworksFilterPipe,
    StarComponent,
    HomeworkDetailComponent,
    NewHomeworkComponent,
    WelcomeComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialComponentsModule,
    FormsModule,
  ],
  providers: [
    HomeworksService,
    HomeworkDetailGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
