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
    this.buscarDemandas();
  }

  ngOnInit() {
  }

  logout(){
    localStorage.removeItem('usuario_logado');
    this.navCtrl.navigateForward('home');
  }

  async buscarDemandas(){
    this.demandas = await this.demandaService.buscarDemandasEscolhidas();
    this.demandas = this.demandas.filter(elem => !elem.Topada);
  }

}
