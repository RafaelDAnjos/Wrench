import { Component, OnInit } from '@angular/core';
import { AlertController, NavController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-listar-demandas',
  templateUrl: './listar-demandas.page.html',
  styleUrls: ['./listar-demandas.page.scss'],
})
export class ListarDemandasPage implements OnInit {
  private demandas:any[]
  constructor(private demandaService:DemandaService, private alertCtrl:AlertController, private navCtrl:NavController) {
    this.buscarDemandas();
  }

  ngOnInit() {
  }
  async buscarDemandas(){
    this.demandas = await this.demandaService.buscarDemandas();
  }

  async escolherDemanda(demanda:any){
    let alerta = await this.alertCtrl.create({
      header: 'Se vai escolher essa demanda nos de as seguintes informações:',
      inputs: [{
        name: 'Valor',
        type: 'text',
        placeholder: 'Entre com o valor esperado'

      },
      {
        name:'Prazo',
        type: 'text',
        placeholder: 'Estabeleça um prazo para concluir o trabalho'
      },
      {name:'mensagem',
      type: 'textarea',
      placeholder:'deixe uma mensagem para o cliente'}],
      buttons: [{
        text: 'Cancel',
        role: 'cancel',
        cssClass: 'secondary',
        handler: () => {
          console.log('Confirm Cancel:');
        }

      },
      {
        text: 'Adicionar',
        handler: (form) => {
          this.add(form,demanda);
        }


      }],
    });
    alerta.present();
  }

  add(form:any,demanda:any){
    let demandaEscolhida = {
      valor: form.Valor,
      prazo: form.Prazo,
      mensagem: form.mensagem,
      id_demanda: demanda.id
    }
    
    // comunicação com o serviço de escolher a demanda passando a demanda

  }
  logout(){
    localStorage.removeItem('usuario_logado');
    this.navCtrl.navigateForward('home');
  }


}
