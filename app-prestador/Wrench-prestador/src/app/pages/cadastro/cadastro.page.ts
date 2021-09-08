import { Component, OnInit } from '@angular/core';
import { NavController, ToastController } from '@ionic/angular';
import { AutorizacaoService } from 'src/app/services/autorizacao.service';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.page.html',
  styleUrls: ['./cadastro.page.scss'],
})
export class CadastroPage implements OnInit {
  private inputNome:string;
  private inputEmail:string;
  private inputSenha:string;
  private inputRepetirSenha:string;
  private inputIdentificacao:string;
  constructor(private navCtrl:NavController, private authService:AutorizacaoService, private toastCtrl:ToastController) { }

  ngOnInit() {
  }

  async cadastrar(){
    let usuario = {
      nome: this.inputNome,
      email: this.inputEmail,
      senha: this.inputSenha,
      confirmarSenha: this.inputRepetirSenha,
      identificacao: this.inputIdentificacao,
      tipoUsuario: 1
    }
    let response = await this.authService.cadastrar_usuario(usuario);
    if(response.sucesso){
      localStorage.setItem('usuario_logado',response.token);
      this.showPageDefinirTags();
    }else{
      let toast = await this.toastCtrl.create({
        message: response.Errors[0],
        duration: 1000,
        position: 'top'
      });
      toast.present();
    }


  }
  async showPageDefinirTags(){
    await this.navCtrl.navigateForward('definir-tags')
  }
}
