import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CriarDemandaPage } from './criar-demanda.page';

const routes: Routes = [
  {
    path: '',
    component: CriarDemandaPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CriarDemandaPageRoutingModule {}
