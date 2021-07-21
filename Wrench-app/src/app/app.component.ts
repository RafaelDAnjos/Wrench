import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Demandas', url: 'demandas', icon: 'mail' },
    { title: 'Conversas', url:'conversas',icon: 'mail'},
    {title: 'Agenda',url:'agenda',icon: 'mail'}
    
  ];
  public labels = ['Family', 'Friends', 'Notes', 'Work', 'Travel', 'Reminders'];
  constructor() {}
}
