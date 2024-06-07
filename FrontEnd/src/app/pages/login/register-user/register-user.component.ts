import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../../core/services/login.service';
import { ToastService } from '../../../core/services/toast.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrl: './register-user.component.scss'
})
export class RegisterUserComponent implements OnInit {
  registerForm!: FormGroup;
  mostrarErro: boolean = false;

  tipoSenha: string = 'password';



  constructor(private formBuilder: FormBuilder,
              private location: Location,
              private router: Router,
              private toastService: ToastService,
              private loginService: LoginService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      nomeUsuario: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmarSenha: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  
  mostrarSenha() {
    this.tipoSenha = this.tipoSenha === 'password' ? 'text' : 'password';
  }

  onRegister() {
    var usuario = this.registerForm.value;
    if(usuario.senha != usuario.confirmarSenha){
      this.toastService.show('fail', 'Senhas não estão iguais!')
    }

    else if (this.registerForm.valid) {
      this.loginService.registrarUsuario(this.registerForm.value).then(result => {
        this.toastService.show('success',"Cadastro Realizado com Sucesso!")
        this.router.navigate(['/login'])
      }, fail => {
        this.toastService.show('fail',fail.error)
      })
    }
    else{
      this.toastService.show('fail',"Formulário Invalido!")

    }
  }

  voltar() {
    this.location.back();
  }
}
