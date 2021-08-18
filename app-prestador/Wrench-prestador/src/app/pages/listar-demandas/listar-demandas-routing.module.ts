import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListarDemandasPage } from './listar-demandas.page';

const routes: Routes = [
  {
    path: '',
    component: ListarDemandasPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ListarDemandasPageRoutingModule {}
