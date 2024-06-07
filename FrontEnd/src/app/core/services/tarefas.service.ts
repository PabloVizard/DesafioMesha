import { HttpClient } from "@angular/common/http";
import { lastValueFrom, catchError } from "rxjs";
import { BaseService } from "./base.services";
import { TarefasModel } from "../models/TarefasModel";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
  
export class TarefasService extends BaseService {

    constructor(private http: HttpClient) {
      super();
    }
  
    async ObterTarefasPorId(id: number): Promise<any> {
        return await lastValueFrom(this.http
          .get(this.urlApi + `tarefas/ObterPorId?id=${id}`, super.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError))); 
      }
    
      async ObterTodasTarefas(): Promise<any> {
        return await lastValueFrom(this.http
          .get(this.urlApi + `tarefas/ObterTodos`, super.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError))); 
      }
    
      async AtualizarTarefa(tarefa: TarefasModel): Promise<any> {
        return await lastValueFrom(this.http
          .put(this.urlApi + `tarefas/Atualizar`,tarefa, super.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError))); 
      }
    
    
      async CriarNovaTarefa(tarefa: TarefasModel): Promise<any> {
        return await lastValueFrom(this.http
          .post(this.urlApi + `tarefas/Registrar`, tarefa, super.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError))); 
      }
    
      async RemoverTarefa(id: number): Promise<any> {
        return await lastValueFrom(this.http
          .delete(this.urlApi + `tarefas/Remover?id=${id}`, super.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError))); 
      }
  
  
  }
  