import { Component, OnInit } from '@angular/core';
import { AlertController, ToastController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-avaliar-servicos',
  templateUrl: './avaliar-servicos.page.html',
  styleUrls: ['./avaliar-servicos.page.scss'],
})
export class AvaliarServicosPage implements OnInit {

  private demandas:any[] = [];

  constructor(private demandaService:DemandaService, private alertCtrl:AlertController, private toastCtrl:ToastController) {
    this.buscarDemandasAvaliacaoPendente();
   }

  ngOnInit() {
  }

  async buscarDemandasAvaliacaoPendente(){
    this.demandas = await this.demandaService.buscarDemandasAvaliacaoPendente();
  }

  async avaliar(demanda){
    let alerta = await this.alertCtrl.create({
      header: 'Por favor avalie o prestador',
      inputs: [{
        name: 'Nota',
        type: 'number',
        placeholder: 'Digite uma nota entre 0 e 10',
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
          if(form.nota<0 || form.nota>5){
            let toast = await this.toastCtrl.create({
              message:"Digite uma nota válida!",
              position:'top',
              duration: 2000
            });
            toast.present();
          }else{
            this.addAvaliacao(form, demanda);
          }
        }
      }],
    });
    alerta.present(); 
  }

  async addAvaliacao(form:any, demanda:any){
    let vm = {idDemanda: demanda.idDemanda, nota: form.Nota}
    
    await this.demandaService.avaliar(vm);
  }
}
