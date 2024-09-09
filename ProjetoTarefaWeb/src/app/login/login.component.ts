import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  senha: string = '';
  nome: string = '';
  telefone: string = '';
  isLoginMode: boolean = true; // Alterna entre login e cadastro

  constructor(private http: HttpClient, private router: Router) {}

  onToggleMode() {
    this.isLoginMode = !this.isLoginMode;
  }

  onSubmit() {
    if (this.isLoginMode) {
      // Rota para login
      const loginData = { email: this.email, senha: this.senha };
      this.http.post('http://localhost:5223/api/usuario/login', loginData)
        .subscribe(response => {
          console.log('Login bem-sucedido', response);
          this.router.navigate(['/home']); // Redirecionar para a página home após o login
        }, error => {
          console.error('Erro ao fazer login', error);
        });
    } else {
      // Rota para cadastro
      const cadastroData = { 
        Email: this.email, 
        Nome: this.nome, 
        Telefone: this.telefone, 
        senha: this.senha 
      };
      this.http.post('http://localhost:5223/api/usuario/cadastro', cadastroData)
        .subscribe(response => {
          console.log('Cadastro bem-sucedido', response);
          this.router.navigate(['/login']); // Redirecionar para a página de login após cadastro
        }, error => {
          console.error('Erro ao fazer cadastro', error);
        });
    }
  }
}
