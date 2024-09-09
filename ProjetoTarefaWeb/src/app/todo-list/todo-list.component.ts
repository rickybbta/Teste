import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  user: { id: number, name: string, email: string } = { id: 0, name: '', email: '' }; // Dados do usuário logado
  tasks: any[] = [];
  showAddTaskModal: boolean = false;
  newTask = {
    nome: '',
    descricao: '',
    dataRealizacao: '',
    status: 'Pendente',
    usuarioId: 0
  };

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.loadUser();
    this.loadTasks();
  }

  loadUser() {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    if (user && user.id) {
      this.user = user;
      this.newTask.usuarioId = this.user.id; // Defina o usuarioId para o newTask
    } else {
      // Redirecionar para a página de login se o usuário não estiver autenticado
      this.router.navigate(['/login']);
    }
  }

  loadTasks() {
    if (this.user.id) {
      this.http.get<any[]>(`http://localhost:5223/api/tarefa/usuario/${this.user.id}`)
        .subscribe(response => {
          this.tasks = response;
        }, error => {
          console.error('Erro ao carregar tarefas', error);
        });
    }
  }

  onOpenAddTaskModal() {
    this.showAddTaskModal = true;
  }

  onCloseAddTaskModal() {
    this.showAddTaskModal = false;
  }

  onAddTask() {
    this.newTask.usuarioId = this.user.id;
    this.http.post('http://localhost:5223/api/tarefa', this.newTask)
      .subscribe(response => {
        alert('Tarefa adicionada com sucesso!');
        this.showAddTaskModal = false;
        this.loadTasks(); // Recarregar a lista de tarefas
      }, error => {
        console.error('Erro ao adicionar tarefa', error);
      });
  }

  onUpdateTask(task: any) {
    this.http.put(`http://localhost:5223/api/tarefa/usuario/${this.user.id}/${task.id}`, task)
      .subscribe(response => {
        alert('Tarefa atualizada com sucesso!');
        this.loadTasks(); // Recarregar a lista de tarefas
      }, error => {
        console.error('Erro ao atualizar tarefa', error);
      });
  }

  onDeleteTask(taskId: number) {
    this.http.delete(`http://localhost:5223/api/tarefa/usuario/${this.user.id}/${taskId}`)
      .subscribe(response => {
        alert('Tarefa deletada com sucesso!');
        this.loadTasks(); // Recarregar a lista de tarefas
      }, error => {
        console.error('Erro ao deletar tarefa', error);
      });
  }
}
