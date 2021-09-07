import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private url = 'https://localhost:44303'
  constructor(private http: HttpClient) { }

  buscarTags():Promise<any>{
    return this.http.get(`${this.url}/api/Tags`).toPromise();
  }
  criarTag(js:any, token:string):Promise<any>{
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };

    return this.http.post(`${this.url}/api/Tags`,js, httpOptions).toPromise();
  }
  escolherTag(usuario:any, tags:any, token:string):Promise<any>{
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.post(`${this.url}/api/usuarios/${usuario.id}/controle-tags`,tags, httpOptions).toPromise();
  }


}
