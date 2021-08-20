import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  private url = 'https://localhost:44303';

  constructor(private http:HttpClient) { }

  buscarUsuario(token:string):Promise<any>{
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      })
    };
    return this.http.get(`${this.url}/api/Usuarios/profile`, httpOptions).toPromise();
  }


}
