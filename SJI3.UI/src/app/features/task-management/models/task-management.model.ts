import { FormControl } from '@angular/forms';
import { Moment } from 'moment/moment';
import { TaskUnitStatus } from 'src/app/common/models/common.model';

export interface TaskUnit {
  id: string;
  moniker: string;
  taskUnitType: string;
  fromDateTime: string;
  toDateTime: string;
  taskUnitStatus: TaskUnitStatus;
}

export interface TaskUnitViewModel {
  id: string;
  moniker: string;
  taskUnitType: string;
  dateTimeRange: string;
  taskUnitStatus: TaskUnitStatus;
}

export interface TaskUnitPostBody {
  id: string;
  moniker: string;
  taskUnitTypeId: number;
  fromDateTime: Moment | null;
  toDateTime: Moment | null;
}

export interface TaskUnitAddedResponse {
  result: boolean;
  errorMessage: any;
  timeGenerated: string;
}

export interface TaskStatusChangedResponse {
  result: boolean;
  errorMessage: any;
  timeGenerated: string;
}

export interface TaskUnitsResponse {
  result: TaskUnit[];
  errorMessage: any;
  timeGenerated: string;
}

export interface ResourceParameters {
  pageSize: number;
  pageNumber: number;
  sortBy?: string;
  sortDirection?: string;
  startDate: string | null | undefined;
  endDate: string | null | undefined;
}

export interface DateRange {
  fromDate: string | null | undefined;
  toDate: string | null | undefined;
}

export interface TaskUnitForm {
  id: FormControl<string>;
  moniker: FormControl<string>;
  fromDateTime: FormControl<Moment>;
  toDateTime: FormControl<Moment>;
  taskUnitTypeId: FormControl<string>;
}
