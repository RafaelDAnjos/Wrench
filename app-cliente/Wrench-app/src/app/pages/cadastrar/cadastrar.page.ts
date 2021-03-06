import { Component, OnInit } from '@angular/core';
import { NavController, ToastController } from '@ionic/angular';
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
  constructor(private authService:AutorizacaoService, private toastCtrl: ToastController, private navCtrl:NavController) { }

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
    if(response.sucesso){
      localStorage.setItem('usuario_logado',response.token);
      this.showPageDemandas();
    }else{
      let toast = await this.toastCtrl.create({
        message: response.Errors[0],
        duration: 1000,
        position: 'top'
      });
      toast.present();
    }
  }
   async showPageDemandas(){
     this.navCtrl.navigateForward('demandas')
   }

}
