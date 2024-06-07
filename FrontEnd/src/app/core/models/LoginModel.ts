export class LoginModel{
    nomeUsuario: string;
    senha: string;

    constructor(nomeUsuario: string, senha: string) {
        this.nomeUsuario = nomeUsuario;
        this.senha = senha;
    }
} 