import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-conversas',
  templateUrl: './conversas.page.html',
  styleUrls: ['./conversas.page.scss'],
})
export class ConversasPage implements OnInit {

  constructor(private navCtrl:NavController) { }

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

}
