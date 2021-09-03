import { Component, OnInit } from '@angular/core';
import { DemandaService } from 'src/app/services/demanda.service';

@Component({
  selector: 'app-listar-demandas',
  templateUrl: './listar-demandas.page.html',
  styleUrls: ['./listar-demandas.page.scss'],
})
export class ListarDemandasPage implements OnInit {
  private demandas:any[]
  constructor(private demandaService:DemandaService) {
    this.buscarDemandas();
  }

  ngOnInit() {
  }
  async buscarDemandas(){
    this.demandas = await this.demandaService.buscarDemandas();
  }

}
