import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Demandas Recomendadas', url: '/listar-demandas', icon: 'mail' },
    { title: 'Demandas Escolhidas', url: '/conversas', icon: 'chatbubbles' },
    { title: 'Demandas na Agenda', url: '/agenda', icon: 'calendar' },
    {title: 'Definir Tags',url:'/definir-tags',icon:'settings'}
  ];
 
  constructor() {}
}
