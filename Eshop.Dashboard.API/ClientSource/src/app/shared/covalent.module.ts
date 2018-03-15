import { NgModule } from '@angular/core';
import { CovalentCommonModule } from '@covalent/core/common';
import { CovalentLayoutModule } from '@covalent/core/layout';
import { CovalentStepsModule  } from '@covalent/core/steps';
import { CovalentDataTableModule } from '@covalent/core/data-table';
import { CovalentPagingModule } from '@covalent/core/paging';
import { CovalentSearchModule } from '@covalent/core/search';

// https://github.com/Teradata/covalent-quickstart/blob/develop/src/app/shared/shared.module.ts

@NgModule({
  imports: [
  ],
  exports: [
    CovalentCommonModule,
    CovalentLayoutModule,
    CovalentStepsModule,
    CovalentDataTableModule,
    CovalentPagingModule,
    CovalentSearchModule
  ],
  declarations: []
})
export class CovalentModule { }
