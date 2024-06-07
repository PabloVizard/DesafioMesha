import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';
import { LoginModel } from '../models/LoginModel';
import { lastValueFrom } from 'rxjs';
import { BaseService } from './base.services';
import { UsuariosModel } from '../models/UsuariosModel';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  async loginUsuario(loginModel: LoginModel): Promise<any> {
    return await lastValueFrom(this.http
      .post(this.urlApi + 'login/LoginUsuario', loginModel)
      .pipe(catchError(super.serviceError))); 
  }
  async registrarUsuario(cadastroUsuarioModel: UsuariosModel): Promise<any> {
    return await lastValueFrom(this.http
      .post(this.urlApi + 'login/RegistrarUsuario', cadastroUsuarioModel)
      .pipe(catchError(super.serviceError))); 
  }
}
