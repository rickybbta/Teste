import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Para usar [(ngModel)] nos formulários
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router'; // Importar para configurar as rotas

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component'; // Importar o componente de login

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redirecionar para login por padrão
  { path: 'login', component: LoginComponent } // Rota de login
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent // Declarar o componente de login
  ],
  imports: [
    BrowserModule,
    FormsModule, // Para suporte a formulários
    HttpClientModule,
    RouterModule.forRoot(routes) // Configurar as rotas
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
