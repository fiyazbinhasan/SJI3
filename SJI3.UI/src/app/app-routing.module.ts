import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskManagementPageComponent } from './features/task-management/task-management-page.component';
import { TaskManagementViewComponent } from './features/task-management/components/task-management-view/task-management-view.component';

const routes: Routes = [
  { path: '', redirectTo: 'task-management', pathMatch: 'full' },
  {
    path: 'task-management',
    component: TaskManagementPageComponent,
    children: [
      {
        path: '',
        redirectTo: 'manage',
        pathMatch: 'full',
      },
      { path: 'manage', component: TaskManagementViewComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
  declarations: [],
})
export class AppRoutingModule {}
