import { Component, OnInit, ViewChild } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  FormGroupDirective,
  NgForm,
  Validators,
} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Guid } from 'guid-typescript';
import { TaskType } from '../../models/task-type.model';
import { TaskManagementDataService } from '../../services/task-management-data-services';
import { EnumBase } from 'src/app/common/models/common.model';
import * as moment from 'moment';
import {
  fromDateTimeCompareValidator,
  markFormGroupDirty,
  toDateTimeCompareValidator,
} from 'src/app/common/validators/custom-validators';
import { TaskUnitForm } from '../../models/task-management.model';
import { ErrorStateMatcher } from '@angular/material/core';
import { AuthService } from 'src/app/common/services/auth.service';
import { SnackbarService } from 'src/app/common/services/snackbar.service';

class CrossFieldErrorMatcher implements ErrorStateMatcher {
  isErrorState(
    control: AbstractControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    return control &&
      form &&
      control.dirty &&
      form.hasError('fromDateTimeInvalid')
      ? true
      : false;
  }
}

@Component({
  selector: 'app-task-management-add-modal',
  templateUrl: './task-management-add-modal.component.html',
  styleUrls: ['./task-management-add-modal.component.scss'],
})
export class TaskManagementAddModalComponent implements OnInit {
  @ViewChild('formRef') formRef: FormGroupDirective = {} as FormGroupDirective;

  errorMatcher = new CrossFieldErrorMatcher();
  taskTypeLists: EnumBase[] = [];
  eventTypeLists: EnumBase[] = [];
  taskForm: FormGroup<TaskUnitForm>;
  showSeconds: boolean = true;
  enableMeridian: boolean = true;
  hideDateControls: boolean = false;

  get id() {
    return this.taskForm.controls.id;
  }

  get moniker() {
    return this.taskForm.controls.moniker;
  }

  get taskUnitTypeId() {
    return this.taskForm.controls.taskUnitTypeId;
  }

  get fromDateTime() {
    return this.taskForm.controls.fromDateTime;
  }

  get toDateTime() {
    return this.taskForm.controls.toDateTime;
  }

  constructor(
    private dataService: TaskManagementDataService,
    fb: FormBuilder,
    public dialogRef: MatDialogRef<TaskManagementAddModalComponent>,
    private authService: AuthService,
    private snackbarService: SnackbarService
  ) {
    this.taskForm = fb.nonNullable.group(
      {
        id: new FormControl(Guid.create().toString(), { nonNullable: true }),
        moniker: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
        fromDateTime: new FormControl(moment().utc(true), {
          nonNullable: true,
          validators: [Validators.required],
        }),
        toDateTime: new FormControl(moment().utc(true), {
          nonNullable: true,
          validators: [Validators.required, toDateTimeCompareValidator],
        }),
        taskUnitTypeId: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
      },
      {
        updateOn: 'blur',
        validators: [fromDateTimeCompareValidator],
      }
    );
  }

  ngOnInit() {
    this.taskTypeLists = [
      { id: TaskType.TypeOne.toString(), name: 'TypeOne' },
      { id: TaskType.TypeTwo.toString(), name: 'TypeTwo' },
    ];
  }

  onSave() {
    this.formRef.onSubmit(new Event('submit'));

    if (this.formRef.invalid) {
      markFormGroupDirty(this.taskForm);
      return;
    }

    if (!this.authService.isAuthenticated$.value) {
      this.snackbarService.error(
        'You are not authorized to perform this action'
      );
      return;
    }

    this.dataService.postTaskUnit(this.formRef.value).subscribe((response) => {
      if (response.body?.errorMessage)
        throw new Error(response.body?.errorMessage);
      if (response.body?.result) this.dialogRef.close({ added: true });
    });
  }

  onTaskSelected(taskTypeId: string) {
    if (taskTypeId === TaskType.TypeTwo.toString()) {
      this.hideDateControls = true;
      this.fromDateTime.clearValidators();
      this.toDateTime.clearValidators();
      this.taskForm.clearValidators();
      this.taskForm.updateValueAndValidity();
    } else {
      this.hideDateControls = false;
      this.fromDateTime.addValidators(Validators.required);
      this.toDateTime.addValidators([
        Validators.required,
        toDateTimeCompareValidator,
      ]);
      this.taskForm.addValidators([fromDateTimeCompareValidator]);
      this.taskForm.updateValueAndValidity();
    }
  }
}
