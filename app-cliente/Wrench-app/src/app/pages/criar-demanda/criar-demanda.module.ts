import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { CriarDemandaPageRoutingModule } from './criar-demanda-routing.module';

import { CriarDemandaPage } from './criar-demanda.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    CriarDemandaPageRoutingModule
  ],
  declarations: [CriarDemandaPage]
})
export class CriarDemandaPageModule {}
