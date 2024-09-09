import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user = {
    email: '',
    senha: ''
  };

  signUpUser = {
    email: '',
    nome: '',
    telefone: '',
    senha: ''
  };

  showSignUpForm = false;

  constructor(private http: HttpClient) { }

  onSubmit() {
    // Lógica para login
    this.http.post('http://localhost:5223/api/usuario/login', this.user)
      .subscribe(response => {
        // Manipule a resposta do login
        console.log(response);
      });
  }

  onSignUp() {
    // Lógica para cadastro
    this.http.post('http://localhost:5223/api/usuario/cadastro', this.signUpUser)
      .subscribe(response => {
        // Manipule a resposta do cadastro
        console.log(response);
      });
  }
}
