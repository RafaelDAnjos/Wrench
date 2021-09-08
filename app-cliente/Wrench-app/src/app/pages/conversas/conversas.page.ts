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
    this.buscarDemandasEscolhidas();
  }

  ngOnInit() {
  }

  async buscarDemandasEscolhidas(){
    let propostas:any[] = [];    

    let demandasEscolhidas = await this.demandaService.buscarDemandasEscolhidas();
    
    for(let i = 0; i < demandasEscolhidas.length; i++){
      let temp = demandasEscolhidas[i];

      for(let j = 0; j < temp.propostas.length; j++){
        let temp2 = temp.propostas[j][0];

        let proposta = {
          idDemanda: temp.idDemanda,
          titulo: temp.titulo,
          descricao: temp.descricao,
          idRegistroServico: temp2.idRegistroServico,
          mensagem: temp2.mensagem,
          prazo: temp2.prazo,
          valorEstimado: temp2.valorEstimado,
          prestador: temp2.prestador          
        }

        propostas.push(proposta);
      }

    }
    
    this.demandas = propostas;
    
  }

  async topar(demanda:any){    
    await this.demandaService.toparDemanda(demanda);
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
