import { Component, OnInit } from '@angular/core';
import { AlertButton, AlertController, NavController, ToastButton, ToastController } from '@ionic/angular';
import { Local } from 'protractor/built/driverProviders';
import { TagService } from 'src/app/services/tag.service';
import { UsuariosService } from 'src/app/services/usuarios.service';

@Component({
  selector: 'app-definir-tags',
  templateUrl: './definir-tags.page.html',
  styleUrls: ['./definir-tags.page.scss'],
})
export class DefinirTagsPage implements OnInit {
  private tags:any[] = [];
  constructor(private tagService: TagService, private alertCtrl:AlertController, private toastCtrl:ToastController, private usuarioService:UsuariosService, private navCtrl:NavController) { 
    this.buscarListaTag();
  }

  ngOnInit() {
  }

  async buscarListaTag(){
    let token = localStorage.getItem("usuario_logado");

    this.tags = await this.tagService.buscarTags();
    let minhas_tags = (await this.usuarioService.buscarUsuario(token)).tags;

    let i;
    for(i=0;i<this.tags.length;i++){
      this.tags[i] = {
        nome: this.tags[i].nome,
        id: this.tags[i].idTag,
        check: minhas_tags.some(tag => tag.idTag == this.tags[i].idTag)
      }
    }
  
    
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
    let token = localStorage.getItem("usuario_logado")
    await this.tagService.criarTag(tag, token);

    this.buscarListaTag();
  }
  async continuar(){
    let tags_ativas = this.tags.filter(tagItem=>tagItem.check == true);

    let tags_selecionadas = {
      "tags": tags_ativas.map(tag => tag.nome)
    }
    
    let token = localStorage.getItem("usuario_logado")
    let usuario  = await this.usuarioService.buscarUsuario(token);
    await this.tagService.escolherTag(usuario,tags_selecionadas,token);
    this.showPageDemandas();
  }
  async showPageDemandas(){
    this.navCtrl.navigateForward('listar-demandas')
  }
}

