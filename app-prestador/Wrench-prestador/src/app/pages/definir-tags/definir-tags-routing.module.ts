import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DefinirTagsPage } from './definir-tags.page';

const routes: Routes = [
  {
    path: '',
    component: DefinirTagsPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DefinirTagsPageRoutingModule {}
