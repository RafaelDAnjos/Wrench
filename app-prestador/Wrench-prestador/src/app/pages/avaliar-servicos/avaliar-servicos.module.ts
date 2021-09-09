import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AvaliarServicosPageRoutingModule } from './avaliar-servicos-routing.module';

import { AvaliarServicosPage } from './avaliar-servicos.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AvaliarServicosPageRoutingModule
  ],
  declarations: [AvaliarServicosPage]
})
export class AvaliarServicosPageModule {}
