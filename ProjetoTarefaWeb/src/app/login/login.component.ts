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
  errorMessage: string = ''; // Variável para armazenar a mensagem de erro

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
      .subscribe(
        (response: any) => {
          console.log('Login bem-sucedido', response);
          localStorage.setItem('user', JSON.stringify(response));
          this.router.navigate(['/todo-list']);
        },
        error => {
          console.error('Erro ao fazer login', error);
          this.errorMessage = 'Email ou senha errados'; // Exibir mensagem de erro
        }
      );
  }

  onOpenRegisterModal() {
    this.showModal = true;
  }

  onCloseRegisterModal() {
    this.showModal = false;
  }

  // Função para validar o formato do e-mail
  isEmailValid(email: string): boolean {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
  }

  onRegister() {
    // Verificar se o e-mail é válido antes de enviar o formulário
    if (!this.isEmailValid(this.registerData.email)) {
      alert('Por favor, insira um e-mail válido.');
      return;
    }

    this.http.post('http://localhost:5223/api/usuario/cadastro', this.registerData)
      .subscribe(
        response => {
          alert('Cadastrado com sucesso!');
          this.showModal = false;
          this.router.navigate(['/login']);
        },
        error => {
          console.error('Erro ao fazer cadastro', error);
        }
      );
  }
}
