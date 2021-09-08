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

  buscarDemandas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/demandas-abertas`,httpOptions).toPromise();
  }

  buscarDemandasEscolhidas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/`,httpOptions).toPromise();
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
}
