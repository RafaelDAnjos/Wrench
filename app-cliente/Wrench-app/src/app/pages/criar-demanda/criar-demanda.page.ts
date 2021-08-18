import { Component, OnInit } from '@angular/core';
import { AlertController, NavController } from '@ionic/angular';

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
  constructor(private navCtrl:NavController, private alertCtrl:AlertController) {
    this.demandas = JSON.parse(localStorage.getItem('demandas'));
    this.tituloD = '';
    this.descD= '';
    this.tags = [
      {
        id: 1,
        titulo:"tag 1" ,
        check: false ,
      },
      {
        id: 2,
        titulo:"tag 2" ,
        check: false ,
      },
      {
        id: 3,
        titulo:"tag 3" ,
        check: false ,
      },
      {
        id: 4,
        titulo:"tag 4" ,
        check: false,
      },
      {
        id: 5,
        titulo:"tag 5" ,
        check: false,
      }
    ];
  }
  
  ngOnInit() {
  }
  criarNovaDemanda(){
    let novaDemanda = {
      titulo: this.tituloD,
      descricao: this.descD,
      tags: this.tags.filter(tagItem=>tagItem.check == true)
    }
    if(this.demandas == null){
      let newdemandas:any[] = [];
      newdemandas.push(novaDemanda);
      localStorage.setItem('demandas',JSON.stringify(newdemandas));
      this.navCtrl.navigateForward('demandas');    
    }else{

      this.demandas.push(novaDemanda);
      localStorage.setItem('demandas',JSON.stringify(this.demandas));
      this.navCtrl.navigateForward('demandas');
    }
    
  }
  async voltar(){
    await this.navCtrl.navigateForward('demandas');
  }
  statusChange(tag:any,event:any){

    tag.check = !tag.check;

  }
  async criarTag(){
    const alerta = await this.alertCtrl.create({
      message: "Criar nova tag",
      inputs:[{
        name: 'titulo',
        type: 'text',
        placeholder: 'Insira o titulo da tag'
      }],
      buttons:[{
        text: 'Confirmar',
        handler: (form)=>{
          this.tags.push({titulo: form.titulo,done:false});
        }

      },
        {
        text:'Cancelar',
        role: 'cancel'
      }]
    });
    alerta.present();
  }

}
