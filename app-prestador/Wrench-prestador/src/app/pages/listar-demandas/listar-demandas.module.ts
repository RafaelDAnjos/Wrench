import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ListarDemandasPageRoutingModule } from './listar-demandas-routing.module';

import { ListarDemandasPage } from './listar-demandas.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ListarDemandasPageRoutingModule
  ],
  declarations: [ListarDemandasPage]
})
export class ListarDemandasPageModule {}
