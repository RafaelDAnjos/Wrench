import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {

  constructor(private navCtrl:NavController) { }

  ngOnInit() {
  }

  async showPageDemandas(){
    await this.navCtrl.navigateForward('demandas');
  }
  async showPageCadastro(){
    await this.navCtrl.navigateForward('cadastrar');
  }

}
