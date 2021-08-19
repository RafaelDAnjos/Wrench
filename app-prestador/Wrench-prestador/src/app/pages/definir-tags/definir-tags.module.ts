import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DefinirTagsPageRoutingModule } from './definir-tags-routing.module';

import { DefinirTagsPage } from './definir-tags.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    DefinirTagsPageRoutingModule
  ],
  declarations: [DefinirTagsPage]
})
export class DefinirTagsPageModule {}
