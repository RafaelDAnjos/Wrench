import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private url = "https://localhost:44303"
  constructor(private http: HttpClient) { }

  buscarTags():Promise<any>{
    return this.http.get(`${this.url}/api/Tags`).toPromise();
  }

}
