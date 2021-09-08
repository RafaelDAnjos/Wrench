import { Component, OnInit } from '@angular/core';
import { AlertController, NavController, ToastController } from '@ionic/angular';
import { DemandaService } from 'src/app/services/demanda.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-criar-demanda',
  templateUrl: './criar-demanda.page.html',
  styleUrls: ['./criar-demanda.page.scss'],
})
export class CriarDemandaPage implements OnInit {
  tags: any[] = [];

  tituloD: string;
  descD: string;

  demandas:any[] = [];
  constructor(private navCtrl:NavController, private alertCtrl:AlertController, private tagService:TagService, private toastCtrl:ToastController, private demandaService:DemandaService) {
    this.buscarListaTag();
    this.tituloD = '';
    this.descD= '';
  }
  
  ngOnInit() {
  }
  async criarNovaDemanda(){
    let novaDemanda = {
      titulo: this.tituloD,
      descricao: this.descD,
      tags: this.tags.filter(tagItem=>tagItem.check == true).map(tag => tag.nome)
    }
      console.log(novaDemanda);
      await this.demandaService.criarDemanda(novaDemanda);
      this.navCtrl.navigateForward('demandas');

      
    
  }
 
  async voltar(){
    await this.navCtrl.navigateForward('demandas');
  }
  statusChange(tag:any,event:any){

    tag.check = !tag.check;

  }
  async criarTag(){
    let alerta = await this.alertCtrl.create({
      header: 'Crie sua tag',
      inputs: [{
        name: 'Tag',
        type: 'text',
        placeholder: 'Entre com titulo da sua tag'

      }],
      buttons: [{
        text: 'Cancel',
        role: 'cancel',
        cssClass: 'secondary',
        handler: () => {
          console.log('Confirm Cancel:');
        }

      },
      {
        text: 'Adicionar',
        handler: (form) => {
          this.add(form.Tag);
        }


      }],
    });

    alerta.present();
  }
  async add(newTag: string) {
    if (newTag.trim().length < 1) {

      const toast = await this.toastCtrl.create({
        message: 'Titulo da tag está vazio',
        duration: 2000,
        position: 'top'
      });
      toast.present();
      return;
    }
    let tag = { nome: newTag};
    await this.tagService.criarTag(tag);

    this.buscarListaTag();
  }
  
  async buscarListaTag(){
    this.tags = await this.tagService.buscarTags();
    let i;
    for(i=0;i<this.tags.length;i++){
      this.tags[i] = {
        nome: this.tags[i].nome,
        id: this.tags[i].idTag,
        check: false
      }
    }
  }
  doRefresh(event:any) {
    console.log('Begin async operation');

    setTimeout(() => {
      console.log('Async operation has ended');
      event.target.complete();
    }, 2000);
  }

}
