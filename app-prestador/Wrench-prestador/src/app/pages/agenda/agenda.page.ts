import { Component, OnInit } from '@angular/core';
import { AlertController, NavController, ToastController } from '@ionic/angular';
import { toastController } from '@ionic/core';
import { format } from 'path';
import { DemandaService } from 'src/app/services/demanda.service';


@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.page.html',
  styleUrls: ['./agenda.page.scss'],
})
export class AgendaPage implements OnInit {
  private demandas:any[] = [];


  constructor(private navCtrl:NavController,     private alertCtrl:AlertController,     private toastCtrl:ToastController,    private demandaService:DemandaService) {  
    this.buscarDemandas();  
  }
  
  ngOnInit() {
  }

  async logout(){
    localStorage.removeItem('usuario_logado');
    await this.navCtrl.navigateForward('home');
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
  
  async avaliarDemandante(){
    let alerta = await this.alertCtrl.create({
      header: 'Por favor avalie o demandante',
      inputs: [{
        name: 'Nota',
        type: 'number',
        placeholder: 'Digite uma nota entre 0 e 10',
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
        handler: async (form) => {
          if(form.nota<0 || form.nota>10){
            let toast = await this.toastCtrl.create({
              message:"Digite uma nota válida!",
              position:'top',
              duration: 2000
            });
            toast.present();
          }else{
            this.addAvaliacao(form);

          }
        }
      }],
    });
    alerta.present();    
  }

  addAvaliacao(form:any){

    // Integrar com o back-end
  }
  cancelarServico(demanda:any){
    //Integrar com o back-end
  }
  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }

  async buscarDemandas(){
    this.demandas = await this.demandaService.buscarDemandasEscolhidas();    
    this.demandas = this.demandas.filter(elem => elem.topada);
    console.log(this.demandas)
  }
}
