import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DemandasPageRoutingModule } from './demandas-routing.module';

import { DemandasPage } from './demandas.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    DemandasPageRoutingModule
  ],
  declarations: [DemandasPage]
})
export class DemandasPageModule {}
