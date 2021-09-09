import { Component, OnInit } from '@angular/core';
import { AlertController, ToastController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.page.html',
  styleUrls: ['./agenda.page.scss'],
})
export class AgendaPage implements OnInit {
 private demandas:any[] = [];

  constructor(private alertCtrl:AlertController, private toastCtrl:ToastController, private demandaService:DemandaService) { 
    this.buscarDemandasTopadas();
  }

  ngOnInit() {
  }

  async buscarDemandasTopadas(){
    this.demandas = await this.demandaService.buscarDemandasTopadas();
  }

  async concluirServico(demanda:any){
    let alerta_conclusao = await this.alertCtrl.create({
        header: 'Deseja Concluir esse serviço?',
        inputs: [{
          name: 'Valor',
          type: 'text',
          placeholder: 'Entre com o valor cobrado'
  
        },
        {
          name:'Prazo',
          type: 'date',
          placeholder: 'Indique a data de conclusão'
        }],
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
            this.addConcluir(form, demanda);
          }
        }],
      }
    );
    alerta_conclusao.present();
  }

  async addConcluir(form:any, demanda:any){    
    await this.demandaService.concluirDemanda({idDemanda: demanda.idDemanda, idRegistroServico: demanda.idRegistroServico, valorCobrado: form.Valor})
  }
  
  async cancelarServico(demanda:any){
    await this.demandaService.recusarServico({idRegistroServico: demanda.idRegistroServico});
  }

  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }

}
