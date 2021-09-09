import { Component, OnInit } from '@angular/core';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-conversas',
  templateUrl: './conversas.page.html',
  styleUrls: ['./conversas.page.scss'],
})
export class ConversasPage implements OnInit {

  private demandas:any[];

  constructor(private demandaService:DemandaService) { 
    this.buscarPropostas();
  }

  ngOnInit() {
  }

  async buscarPropostas(){
    this.demandas = await this.demandaService.buscarPropostas();;    
  }

  async topar(demanda:any){    
    await this.demandaService.toparDemanda(demanda);
  }

  async recusar(demanda:any){
    await this.demandaService.recusarServico({idRegistroServico: demanda.idRegistroServico});
  }

  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }

}
