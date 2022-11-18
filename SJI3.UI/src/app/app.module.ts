import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppMaterialModule } from './app-material.module';
import { NgxEchartsModule } from 'ngx-echarts';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
} from '@angular-material-components/datetime-picker';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ImagePreviewDialogComponent } from './common/components/image-preview-dialog/image-preview-dialog.component';
import { TaskManagementPageComponent } from './features/task-management/task-management-page.component';
import { TaskManagementAddModalComponent } from './features/task-management/components/task-management-add-modal/task-management-add-modal.component';
import { TaskManagementViewComponent } from './features/task-management/components/task-management-view/task-management-view.component';
import { TaskManagementFilterComponent } from './features/task-management/components/task-management-filters/task-management-filter.component';
import { HumanizePipe } from './common/pipes/humanize.pipe';
import { AuthConfigModule } from './auth-config.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TaskManagementPageComponent,
    TaskManagementViewComponent,
    TaskManagementAddModalComponent,
    TaskManagementFilterComponent,
    ImagePreviewDialogComponent,
    HumanizePipe,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts'),
    }),
    AppRoutingModule,
    AuthConfigModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
