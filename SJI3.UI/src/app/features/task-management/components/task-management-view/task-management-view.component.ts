import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { TaskManagementAddModalComponent } from '../task-management-add-modal/task-management-add-modal.component';
import {
  ResourceParameters,
  TaskUnit,
  TaskUnitsResponse,
  TaskUnitViewModel,
} from '../../models/task-management.model';
import { TaskManagementDataService } from '../../services/task-management-data-services';
import { MatSort } from '@angular/material/sort';
import { HttpResponse } from '@angular/common/http';
import { SnackbarService } from '../../../../common/services/snackbar.service';
import { SignalrService } from '../../../../common/services/signalr.service';
import { TaskUnitStatus } from '../../../../common/models/common.model';
import * as moment from 'moment';
import { AuthService } from 'src/app/common/services/auth.service';

@Component({
  selector: 'app-task-management-view',
  templateUrl: 'task-management-view.component.html',
  styleUrls: ['./task-management-view.component.scss'],
})
export class TaskManagementViewComponent
  implements OnInit, AfterViewInit, OnDestroy
{
  @ViewChild(MatPaginator) paginator: MatPaginator = {} as MatPaginator;
  @ViewChild(MatSort) sort: MatSort = {} as MatSort;
  displayedColumns = [
    'moniker',
    'taskUnitTypeId',
    'dateTimeRange',
    'taskUnitStatusId',
    'actions',
  ];
  resultsLength = 0;
  isLoadingResults = true;
  dataSource: TaskUnitViewModel[] = [];
  TaskUnitStatus = TaskUnitStatus;

  constructor(
    private dialog: MatDialog,
    private dataService: TaskManagementDataService,
    private snackbarService: SnackbarService,
    private signalrService: SignalrService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.signalrService.msgReceived$.subscribe((_) => this.loadDataSource());
  }

  ngAfterViewInit() {
    this.loadDataSource();
  }

  loadDataSource() {
    merge(
      this.paginator.page,
      this.sort.sortChange,
      this.dataService.currentDateRange
    )
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;

          const parameters: ResourceParameters = {
            pageNumber: this.paginator.pageIndex,
            pageSize: this.paginator.pageSize,
            sortBy: this.sort.active,
            sortDirection: this.sort.direction,
            startDate: this.dataService.dateRange.value?.fromDate,
            endDate: this.dataService.dateRange.value?.toDate,
          };

          return this.dataService
            .getTaskUnits(parameters)
            .pipe(catchError(() => observableOf(null)));
        }),
        map((response: HttpResponse<TaskUnitsResponse> | null) => {
          this.isLoadingResults = false;

          if (response?.body) {
            const totalCount = JSON.parse(
              response.headers.get('x-pagination') || '{ totalCount: 0 }'
            )['totalCount'];

            if (response.body.errorMessage) {
              throw new Error(response.body.errorMessage);
            }

            this.resultsLength = totalCount;

            const result: TaskUnitViewModel[] = response.body.result.map(
              (t: TaskUnit) => ({
                id: t.id,
                moniker: t.moniker,
                taskUnitType: t.taskUnitType,
                dateTimeRange:
                  t.fromDateTime && t.toDateTime
                    ? `${moment(t.fromDateTime).format(
                        'DD MMM, YYYY, HH:mm:ss A'
                      )} - ${moment(t.toDateTime).format(
                        'DD MMM, YYYY, HH:mm:ss A'
                      )}`
                    : '',
                taskUnitStatus: t.taskUnitStatus,
              })
            );

            return result;
          } else {
            return [];
          }
        })
      )
      .subscribe((dataSource: TaskUnitViewModel[]) => {
        this.dataSource = dataSource;
      });
  }

  openAddDialog() {
    const dialogRef = this.dialog.open(TaskManagementAddModalComponent, {
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result.added) this.loadDataSource();
    });
  }

  ngOnDestroy(): void {
    this.dataService.setDateRange(undefined);
  }

  updateTaskUnitStatusToTwo(taskUnit: TaskUnit) {
    if (!this.authService.isAuthenticated$.value) {
      this.snackbarService.error(
        'You are not authorized to perform this action'
      );
      return;
    }

    this.dataService
      .updateTaskUnitStatus(taskUnit.id, TaskUnitStatus.TaskStatusTwo)
      .subscribe((response) => {
        if (response.body?.errorMessage)
          throw new Error(response.body?.errorMessage);
        if (response.body?.result) {
          this.snackbarService.success(`Task (${taskUnit.id}) status changed`);
        } else
          this.snackbarService.error(
            `Task (${taskUnit.id}) status changed failed`
          );
      });
  }
}
