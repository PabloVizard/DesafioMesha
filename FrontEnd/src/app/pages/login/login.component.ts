import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../core/models/LoginModel';
import { Router } from '@angular/router';
import { UsuariosModel } from '../../core/models/UsuariosModel';
import { AuthService } from '../../core/services/auth.service';
import { LoginService } from '../../core/services/login.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  
  public loginModel: LoginModel = new LoginModel('','');
  public UsuarioModel: UsuariosModel = new UsuariosModel();
  public tokenUsuarioLogado: string = '';
  
  tipoSenha: string = 'password';
  
  constructor(private loginService: LoginService,
              private toastService: ToastService,
              private router: Router,
              private authService: AuthService){
  }
  ngOnInit(): void {

    if(localStorage.getItem("user")! != null){
      this.router.navigate(['/home'])
    }
    
  }


  mostrarSenha() {
    this.tipoSenha = this.tipoSenha === 'password' ? 'text' : 'password';
  }

  async LoginUsuario(){
    if(this.loginModel.nomeUsuario == '' || this.loginModel.senha == ''){
      this.toastService.show('fail',"Preencha todos os campos!")
    }
    else{
      await this.loginService.loginUsuario(this.loginModel).then(result => {
        this.UsuarioModel = result.data;
        this.tokenUsuarioLogado = result.token;
        localStorage.setItem("user", JSON.stringify(this.UsuarioModel))
        localStorage.setItem("token", this.tokenUsuarioLogado)
        this.toastService.show('success',"Login realizado com sucesso!")
        this.router.navigate(['/home'])
  
        
      }, fail => {
        this.toastService.show('fail',fail.error)
      })
    }
    
  }
}
