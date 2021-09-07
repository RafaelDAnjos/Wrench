import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ConversasPageRoutingModule } from './conversas-routing.module';

import { ConversasPage } from './conversas.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ConversasPageRoutingModule
  ],
  declarations: [ConversasPage]
})
export class ConversasPageModule {}
