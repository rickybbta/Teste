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
  registerData = {
    nome: '',
    email: '',
    senha: '',
    telefone: ''
  };
  showModal: boolean = false;

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    const loginData = { email: this.email, senha: this.senha };
    this.http.post('http://localhost:5223/api/usuario/login', loginData)
      .subscribe((response: any) => {
        console.log('Login bem-sucedido', response);
        // Armazena as informações do usuário no localStorage
        localStorage.setItem('user', JSON.stringify(response));
        this.router.navigate(['/todo-list']); // Redirecionar para a página todo-list zapós o login
      }, error => {
        console.error('Erro ao fazer login', error);
      });
  }

  onOpenRegisterModal() {
    this.showModal = true;
  }

  onCloseRegisterModal() {
    this.showModal = false;
  }

  onRegister() {
    this.http.post('http://localhost:5223/api/usuario/cadastro', this.registerData)
      .subscribe(response => {
        alert('Cadastrado com sucesso!');
        this.showModal = false;
        this.router.navigate(['/login']); // Redirecionar para a página de login após cadastro
      }, error => {
        console.error('Erro ao fazer cadastro', error);
      });
  }
}
