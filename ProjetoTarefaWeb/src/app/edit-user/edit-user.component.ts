import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user: { id: number, nome: string, email: string, senha: string, telefone: string } = {
    id: 0,
    nome: '',
    email: '',
    senha: '',
    telefone: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    if (user && user.id) {
      this.http.get<any>(`http://localhost:5223/api/usuario/${user.id}`)
        .subscribe(userDetails => {
          this.user = {
            id: userDetails.id,
            nome: userDetails.nome,
            email: userDetails.email,
            senha: '', // Senha não carregada por segurança
            telefone: userDetails.telefone
          };
        }, error => {
          console.error('Erro ao carregar detalhes do usuário', error);
          this.router.navigate(['/login']);
        });
    } else {
      this.router.navigate(['/login']);
    }
  }

  onUpdateUser() {
    const updatedUser = {
      id: this.user.id,
      nome: this.user.nome,
      email: this.user.email,
      senha: this.user.senha || '', // Envia a senha, mesmo se for nova
      telefone: this.user.telefone
    };
  
    console.log('Dados para atualizar:', updatedUser);
  
    this.http.put(`http://localhost:5223/api/usuario/${this.user.id}`, updatedUser)
      .subscribe(
        response => {
          alert('Informações atualizadas com sucesso!');
          localStorage.setItem('user', JSON.stringify(updatedUser)); // Atualiza as informações no localStorage
          this.router.navigate(['/todo-list']);
        },
        error => {
          console.error('Erro ao atualizar informações', error);
        }
      );
  }  

  onCancel() {
    this.router.navigate(['/todo-list']);
  }
}
