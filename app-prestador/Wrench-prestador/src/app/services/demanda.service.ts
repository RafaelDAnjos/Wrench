import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Local } from 'protractor/built/driverProviders';

@Injectable({
  providedIn: 'root'
})
export class DemandaService {  
  private url = 'https://localhost:44303'
  constructor(private http:HttpClient) { }

  concluirDemanda(arg0:any) {
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/demandas/concluir-demanda`,arg0, httpOptions).toPromise();
  }

  buscarDemandasAbertas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/demandas-abertas`,httpOptions).toPromise();
  }

  buscarPropostasEnviadas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/propostas-enviadas`,httpOptions).toPromise();
  }

  buscarPropostasTopadas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/propostas-topadas`,httpOptions).toPromise();
  }

  buscarDemandasAvaliacaoPendente():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/propostas-avaliacao`,httpOptions).toPromise();
  }

  criarDemanda(demanda:any,):Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/demandas`,demanda,httpOptions).toPromise();
  }

  escolherDemanda(demandaEscolhida:any,):Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/demandas/escolher-demanda`, demandaEscolhida, httpOptions).toPromise();
  }

  avaliar(vm:any){
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    
    return this.http.post(`${this.url}/api/demandas/avaliar-servico`, vm, httpOptions).toPromise();
  }

  recusarServico(demanda:any,):Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/demandas/cancelar-servico`,demanda,httpOptions).toPromise();
  }

  recusarDemanda(demanda:any,):Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/demandas/cancelar-demanda`,demanda,httpOptions).toPromise();
  }
}
