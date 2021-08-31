import { Component, OnInit } from '@angular/core';
import { AlertController, NavController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-demandas',
  templateUrl: './demandas.page.html',
  styleUrls: ['./demandas.page.scss'],
})
export class DemandasPage implements OnInit {
  demandas: any[] = []
  constructor(private navCtrl: NavController, private demandaService:DemandaService) {
    this.carregarDemandas();
  }

  ngOnInit() {
  }
  async showPageCriarDemanda(){
    await this.navCtrl.navigateForward('criar-demanda');
  }
  async carregarDemandas(){
    this.demandas = await this.demandaService.buscarDemandas();
  }

}
