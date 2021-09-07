import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DemandasPage } from './demandas.page';

const routes: Routes = [
  {
    path: '',
    component: DemandasPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DemandasPageRoutingModule {}
