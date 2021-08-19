import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-definir-tags',
  templateUrl: './definir-tags.page.html',
  styleUrls: ['./definir-tags.page.scss'],
})
export class DefinirTagsPage implements OnInit {
  private tags:any[];
  constructor() { }

  ngOnInit() {
  }

  statusChange(tag:any,event:any){

    tag.check = !tag.check;

  }

}
