import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './modules/error/not-found/not-found.component';
import { AuthenticationGuard } from './guards/authentication/authentication.guard';
import { AuthenticationErrorComponent } from './modules/error/authentication-error/authentication-error.component';

const routes: Routes = [
    {
        path:'',
        redirectTo: 'admin',
        pathMatch: 'full'
    },
    {
        path:'home',
        loadChildren: () => import('./modules/home/home.module').then(h => h.HomeModule)
    },
    {
        path:'admin',
        canActivate: [AuthenticationGuard],
        loadChildren: () => import('./modules/admin/admin.module').then(a => a.AdminModule)
    },
    {
        path:'login',
        component: LoginComponent
    },
    {
        path: 'not-allowed',
        component: AuthenticationErrorComponent
    },
    {
        path:'**',
        component: NotFoundComponent
    }
] 
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }