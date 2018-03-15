import { NgModule } from '@angular/core';

import { CovalentCommonModule } from '@covalent/core/common';
import { CovalentSearchModule } from '@covalent/core/search';
import { CovalentLayoutModule } from '@covalent/core/layout';
import { CovalentDialogsModule } from '@covalent/core/dialogs';
import { CovalentMediaModule } from '@covalent/core/media';
import { CovalentLoadingModule } from '@covalent/core/loading';
import { CovalentDataTableModule } from '@covalent/core/data-table';
import { CovalentNotificationsModule } from '@covalent/core/notifications';
import { CovalentMenuModule } from '@covalent/core/menu';
import { CovalentPagingModule } from '@covalent/core/paging';
import { CovalentStepsModule } from '@covalent/core/steps';

// https://github.com/Teradata/covalent-quickstart/blob/develop/src/app/shared/shared.module.ts

@NgModule({
  imports: [
  ],
  exports: [
    CovalentDataTableModule,
    CovalentMediaModule,
    CovalentLoadingModule,
    CovalentNotificationsModule,
    CovalentLayoutModule,
    CovalentMenuModule,
    CovalentPagingModule,
    CovalentSearchModule,
    CovalentStepsModule,
    CovalentCommonModule,
    CovalentDialogsModule,
  ],
  declarations: []
})
export class CovalentModule { }
