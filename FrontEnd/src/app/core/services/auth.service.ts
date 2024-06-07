import { EventEmitter, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastService } from "./toast.service";
import { UsuariosModel } from "../models/UsuariosModel";


@Injectable()
export class AuthService{

    constructor(private router: Router,
                private toastService: ToastService){
        
    }

    VerificarUsuarioLogado(){
        
        var usuarioLogado = JSON.parse(localStorage.getItem('user')!)

        return usuarioLogado != null ? true : false;
    }

    ObterUsuarioLogado(): UsuariosModel{
        
        var usuarioLogado = JSON.parse(localStorage.getItem('user')!)

        if(usuarioLogado == null){
            this.ForcedLogOut();
        }

        return usuarioLogado;
    }
    ForcedLogOut(){
        
        localStorage.clear();
        
        this.toastService.show('fail', "Realize o login novamente!")
        this.router.navigate(['/'])
    }

    LogOut(){
        
        localStorage.clear();
        this.router.navigate(['/'])
    }
    
}