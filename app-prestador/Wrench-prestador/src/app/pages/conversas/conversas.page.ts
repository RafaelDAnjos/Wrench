import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';
import { threadId } from 'worker_threads';

@Component({
  selector: 'app-conversas',
  templateUrl: './conversas.page.html',
  styleUrls: ['./conversas.page.scss'],
})
export class ConversasPage implements OnInit {

  private demandas:any[] = [];

  constructor(private navCtrl:NavController, private demandaService:DemandaService) { 
    this.buscarPropostasEnviadas();
  }

  ngOnInit() {
  }

  logout(){
    localStorage.removeItem('usuario_logado');
    this.navCtrl.navigateForward('home');
  }
  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }
  async buscarPropostasEnviadas(){
    this.demandas = await this.demandaService.buscarPropostasEnviadas();    
  }

  async cancelarProposta(demanda:any){
    await this.demandaService.recusarServico({idRegistroServico: demanda.idRegistroServico});
  }

}
