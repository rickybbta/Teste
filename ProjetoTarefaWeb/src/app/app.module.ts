import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Para usar [(ngModel)] nos formulários
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router'; // Importar para configurar as rotas
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { EditUserComponent } from './edit-user/edit-user.component'; // Importar o componente de login

const routes: Routes = [
  { path: 'todo-list', component: TodoListComponent },
  { path: 'edit-user', component: EditUserComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TodoListComponent,
    EditUserComponent // Declarar o componente de login
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
