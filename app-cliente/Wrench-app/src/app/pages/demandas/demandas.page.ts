import { Component, OnInit } from '@angular/core';
import { AlertController, NavController, ToastController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-demandas',
  templateUrl: './demandas.page.html',
  styleUrls: ['./demandas.page.scss'],
})
export class DemandasPage implements OnInit {
  demandas: any[] = []  

  constructor(private navCtrl: NavController,private alertCtrl:AlertController, private toastCtrl:ToastController, private demandaService:DemandaService) {
    this.carregarDemandas();
  }

  ngOnInit() {
  }
  async showPageCriarDemanda(){
    await this.navCtrl.navigateForward('criar-demanda');
  }
  async carregarDemandas(){
    this.demandas = await this.demandaService.buscarMinhasDemandas();    
  }

  async cancelar(demanda:any){
    await this.demandaService.recusarDemanda({idDemanda: demanda.idDemanda});
  }

  async avaliarPrestador(demanda:any){
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

  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }
}
