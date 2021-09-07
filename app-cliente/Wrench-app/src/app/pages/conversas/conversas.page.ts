import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-conversas',
  templateUrl: './conversas.page.html',
  styleUrls: ['./conversas.page.scss'],
})
export class ConversasPage implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  topar(){
    //Integrar com o back-end
  }
  recusar(){
    //Integrar com o back-end
  }
  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }

}
