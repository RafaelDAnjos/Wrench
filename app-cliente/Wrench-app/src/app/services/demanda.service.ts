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

  avaliar(vm:any){
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    
    return this.http.post(`${this.url}/api/demandas/avaliar-servico`, vm, httpOptions).toPromise();
  }

  buscarDemandas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas`, httpOptions).toPromise();
  }

  buscarMinhasDemandas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/minhas-demandas`, httpOptions).toPromise();
  }
  
  buscarPropostas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/buscar-propostas`,httpOptions).toPromise();
  }

  buscarDemandasTopadas():Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/demandas/demandas-topadas`, httpOptions).toPromise();
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

  toparDemanda(demanda:any,):Promise<any>{
    const token = localStorage.getItem('usuario_logado');
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/demandas/topar-demanda`,demanda,httpOptions).toPromise();
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
