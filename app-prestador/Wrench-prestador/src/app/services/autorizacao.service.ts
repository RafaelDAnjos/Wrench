import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AutorizacaoService {
  private url:any = 'https://localhost:44303';
  constructor(private http:HttpClient) { }

  logar(json:any):Promise<any> {
  
    return this.http.post(`${this.url}/api/auth/login`,json).toPromise();
  
  }

  cadastrar_usuario(json:any):Promise<any> {

    return this.http.post(`${this.url}/api/auth/registrar`,json).toPromise();

  }


}
