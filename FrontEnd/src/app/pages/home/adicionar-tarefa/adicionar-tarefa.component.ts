import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { UsuariosModel } from '../../../core/models/UsuariosModel';
import { AuthService } from '../../../core/services/auth.service';
import { ToastService } from '../../../core/services/toast.service';
import { TarefasModel } from '../../../core/models/TarefasModel';
import { TarefasService } from '../../../core/services/tarefas.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adicionar-tarefa',
  templateUrl: './adicionar-tarefa.component.html',
  styleUrl: './adicionar-tarefa.component.scss'
})
export class AdicionarTarefaComponent implements OnInit {
  dados: any;
  usuarioLogado: UsuariosModel = new UsuariosModel();
  tarefaForm!: FormGroup;
  tarefaAtual!: TarefasModel;

  constructor(@Inject(MAT_DIALOG_DATA) public idTarefa: number,
              private fb: FormBuilder,
              private authService: AuthService,
              private toastService: ToastService,
              private tarefasService: TarefasService,
              private router: Router,
              public dialogRef: MatDialogRef<AdicionarTarefaComponent>) {}

  async ngOnInit() {
    this.usuarioLogado = this.authService.ObterUsuarioLogado();
    await this.buscarTarefa()
    
    console.log(this.tarefaAtual)
    this.tarefaForm = this.fb.group({
        id: [this.tarefaAtual?.id | 0],
        titulo: [this.tarefaAtual?.titulo, Validators.required],
        descricao: [this.tarefaAtual?.descricao, Validators.required],
        concluida: [this.tarefaAtual?.concluida != null ? this.tarefaAtual.concluida : false],
        usuarioId: [this.usuarioLogado.id]
    });
  }

  async salvarTarefa() {

    if(this.idTarefa){
      if(this.tarefaForm.valid){
        await this.tarefasService.AtualizarTarefa(this.tarefaForm.value).then(result => {
          this.toastService.show("success", "Tarefa atualizada com sucesso!")
          this.recarregarPagina()
          this.dialogRef.close()
        }, fail => {
          this.toastService.show("fail", "Erro ao atualizar tarefa! " + fail.error)
        })
      }
    }
    else{
      if(this.tarefaForm.valid){
        await this.tarefasService.CriarNovaTarefa(this.tarefaForm.value).then(result => {
          this.toastService.show("success", "Tarefa salva com sucesso!")
          this.recarregarPagina()
          this.dialogRef.close()
        }, fail => {
          this.toastService.show("fail", "Erro ao salvar tarefa! " + fail.error)
        })
      }
    }
      
  }
  async buscarTarefa (){
    
    if(this.idTarefa){
      await this.tarefasService.ObterTarefasPorId(this.idTarefa).then(result => {
        this.tarefaAtual = result;
      }, fail => {
        this.toastService.show("fail", "Erro ao buscar tarefa! " + fail.error)
      })
    }
    
  }

  recarregarPagina() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}