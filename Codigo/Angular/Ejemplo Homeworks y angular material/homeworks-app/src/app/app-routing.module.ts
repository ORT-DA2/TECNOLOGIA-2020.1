import { HomeworkDetailComponent } from "./homework-detail/homework-detail.component";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeworkListComponent } from "./homework-list/homework-list.component";
import { HomeworkDetailGuard } from "./homework-detail.guard";
import { NewHomeworkComponent } from "./new-homework/new-homework.component";
import { WelcomeComponent } from "./welcome/welcome.component";
import { NotFoundComponent } from "./not-found/not-found.component";

const routes: Routes = [
  { path: "homeworks", component: HomeworkListComponent },
  {
    path: "homeworks/:id",
    component: HomeworkDetailComponent,
    canActivate: [HomeworkDetailGuard],
  },
  { path: "", component: WelcomeComponent },
  { path: "newHomework", component: NewHomeworkComponent },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
