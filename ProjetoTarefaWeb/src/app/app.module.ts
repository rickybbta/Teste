import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Para usar [(ngModel)] nos formulários
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router'; // Importar para configurar as rotas
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { TodoListComponent } from './todo-list/todo-list.component'; // Importar o componente de login

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redirecionar para login por padrão
  { path: 'todo-list', component: TodoListComponent },
  { path: 'login', component: LoginComponent }, // Rota de login
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redireciona para login se o caminho for vazio
  { path: '**', redirectTo: '/login' } // Redireciona para login para rotas desconhecidas
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TodoListComponent // Declarar o componente de login
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
