import { Component, OnInit } from '@angular/core';
import { AlertController, NavController } from '@ionic/angular';

@Component({
  selector: 'app-demandas',
  templateUrl: './demandas.page.html',
  styleUrls: ['./demandas.page.scss'],
})
export class DemandasPage implements OnInit {
  demandas: any[] = []
  constructor(private navCtrl: NavController) {
    this.demandas = JSON.parse(localStorage.getItem('demandas'));
    console.log(this.demandas);
  }

  ngOnInit() {
  }
  async showPageCriarDemanda(){
    await this.navCtrl.navigateForward('criar-demanda');
  }

}
