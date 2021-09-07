import { Component, OnInit } from '@angular/core';
import { NavController, ToastController } from '@ionic/angular';
import { Local } from 'protractor/built/driverProviders';
import { AutorizacaoService } from '../services/autorizacao.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  inputEmail:string;
  inputSenha:string;
  constructor(private navCtrl:NavController, private authService: AutorizacaoService, private toastCtrl:ToastController) { }

  ngOnInit() {
  }

  async showPageDemandas(){
    await this.navCtrl.navigateForward('listar-demandas');
  }
  async showPageCadastro(){
    await this.navCtrl.navigateForward('cadastro');
  }
  async login(){
    let user = {
      email: this.inputEmail,
      senha: this.inputSenha
    }
    let response = await this.authService.logar(user);

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

}
