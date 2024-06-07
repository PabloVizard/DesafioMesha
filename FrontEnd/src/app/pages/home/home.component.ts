import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { UsuariosModel } from '../../core/models/UsuariosModel';
import { TarefasModel } from '../../core/models/TarefasModel';
import { TarefasService } from '../../core/services/tarefas.service';
import { ToastService } from '../../core/services/toast.service';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { AdicionarTarefaComponent } from './adicionar-tarefa/adicionar-tarefa.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {
  tarefas: Array<TarefasModel> = new Array<TarefasModel>();
  dataSource: MatTableDataSource<TarefasModel> = new MatTableDataSource();
  usuarioLogado!: UsuariosModel;
  color: string = 'primary';
  displayedColumns: string[] = ['titulo', 'descricao', 'concluida', 'actions'];
  pageSlice: Array<TarefasModel> = new Array<TarefasModel>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private tarefasService: TarefasService,
              private authService: AuthService,
              private toastService: ToastService,
              private dialog: MatDialog,
              private router: Router) {}

  async ngOnInit() {
    this.usuarioLogado = this.authService.ObterUsuarioLogado();
    await this.obterTarefas();
    this.pageSlice = this.tarefas.slice(0,5);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  async obterTarefas() {
    await this.tarefasService.ObterTodasTarefas().then(result => {
      this.tarefas = result;
      this.dataSource.data = this.tarefas;
    }, fail => {
      this.toastService.show('fail', "Erro ao obter tarefas!" + fail.error);
    });
  }

  adicionarTarefa(): void {
    this.dialog.open(AdicionarTarefaComponent, {
      width: '1024px',
      height: '480px',
    });
  }

  editarTarefa(idTarefa: number): void {
    this.dialog.open(AdicionarTarefaComponent, {
      width: '1024px',
      height: '480px',
      data: idTarefa
    });
  }

  async excluirTarefa(idTarefa: number) {
    if (confirm("Deseja realmente excluir a tarefa selecionada?")) {
      this.tarefasService.RemoverTarefa(idTarefa).then(result => {
        this.toastService.show('success', "Tarefa excluída com sucesso!");
        this.recarregarPagina()
      }, fail => {
        this.toastService.show('fail', "Erro ao excluir tarefa!" + fail.error);
      });
    }
  }

  OnPageChange(event: PageEvent){
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize;
    if(endIndex > this.tarefas.length){
      endIndex = this.tarefas.length
    }
    this.pageSlice = this.tarefas.slice(startIndex, endIndex)
  }


  atualizarStatusTarefa(tarefa: TarefasModel, concluida: boolean){

    tarefa.concluida = concluida;
    this.tarefasService.AtualizarTarefa(tarefa).then( result => {
      if(concluida){
        this.toastService.show("success", "Tarefa concluída com sucesso!")
      }
      else{
        this.toastService.show("warning", "Tarefa pendente!")
      }
      
    }, fail => {
      this.toastService.show('fail', "Erro ao atualizar tarefa!" + fail.error);
    })

  }

  
  recarregarPagina() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  logout(){
    this.authService.LogOut()
  }
}
