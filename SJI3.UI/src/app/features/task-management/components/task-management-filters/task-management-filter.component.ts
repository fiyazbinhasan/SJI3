import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EnumBase } from '../../../../common/models/common.model';
import { TaskManagementDataService } from '../../services/task-management-data-services';

@Component({
  selector: 'app-task-management-filter',
  templateUrl: './task-management-filter.component.html',
  styleUrls: ['./task-management-filter.component.scss'],
})
export class TaskManagementFilterComponent implements OnInit {
  eventTypeLists: EnumBase[] = [];

  selectedCamera: any;
  selectedEventTypes: any;

  range = new FormGroup({
    start: new FormControl(''),
    end: new FormControl(''),
  });

  constructor(private taskManagementDataService: TaskManagementDataService) {}

  ngOnInit() {}

  clearDatePicker() {
    this.range.get('start')?.setValue('');
    this.range.get('end')?.setValue('');
    this.taskManagementDataService.setDateRange(undefined);
  }

  dateTimeRangeChanged() {
    this.taskManagementDataService.setDateRange({
      fromDate: this.range.get('start')?.value,
      toDate: this.range.get('end')?.value,
    });
  }
}
