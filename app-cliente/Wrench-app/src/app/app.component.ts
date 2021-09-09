import { Component } from '@angular/core';
import * as firebase from 'firebase/app';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Minhas Demandas', url: 'demandas', icon: 'mail' },
    { title: 'Propostas Recebidas', url:'conversas',icon: 'chatbubbles'},
    {title: 'Demandas Agendadas',url:'agenda',icon: 'calendar'}
    
  ];
 
  constructor() {}
}
