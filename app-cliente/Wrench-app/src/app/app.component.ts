import { Component } from '@angular/core';
import * as firebase from 'firebase/app';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Demandas', url: 'demandas', icon: 'mail' },
    { title: 'Conversas', url:'conversas',icon: 'chatbubbles'},
    {title: 'Agenda',url:'agenda',icon: 'calendar'}
    
  ];
 
  constructor() {}
}
