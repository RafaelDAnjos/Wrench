import { Component, OnInit } from '@angular/core';
import { ToastController } from '@ionic/angular';
import { Local } from 'protractor/built/driverProviders';
import { AutorizacaoService } from 'src/app/services/autorizacao.service';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.page.html',
  styleUrls: ['./cadastrar.page.scss'],
})
export class CadastrarPage implements OnInit {
  inputNome:string;
  inputEmail:string;
  inputIdentificacao:string;
  inputSenha:string;
  inputRepetirSenha:string;
  constructor(private authService:AutorizacaoService, private toastCtrl: ToastController) { }

  ngOnInit() {
  }

  async Cadastrar(){
    let usuario = {
      nome: this.inputNome,
      email: this.inputEmail,
      senha: this.inputSenha,
      confirmarSenha: this.inputRepetirSenha,
      identificacao: this.inputIdentificacao,
      tipoUsuario: 0
    }
    let response = await this.authService.cadastrar_usuario(usuario);
    if(response.Sucesso){
      localStorage.setItem('usuario_logado',response.Token);
    }else{
      let toast = await this.toastCtrl.create({
        message: response.Errors[0],
        duration: 1000,
        position: 'top'
      });
      toast.present();
    }
  }

}
