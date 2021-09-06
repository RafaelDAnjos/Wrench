import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.page.html',
  styleUrls: ['./agenda.page.scss'],
})
export class AgendaPage implements OnInit {
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
    this.demandas = this.demandas.filter(elem => elem.Topada);
  }
}
