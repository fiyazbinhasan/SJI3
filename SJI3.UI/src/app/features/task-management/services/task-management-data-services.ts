import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  DateRange,
  TaskStatusChangedResponse,
  ResourceParameters,
  TaskUnitAddedResponse,
  TaskUnitPostBody,
  TaskUnitsResponse,
} from '../models/task-management.model';
import { TaskUnitStatus } from '../../../common/models/common.model';
import * as moment from 'moment';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AuthService } from 'src/app/common/services/auth.service';

@Injectable({ providedIn: 'root' })
export class TaskManagementDataService {
  public dateRange = new BehaviorSubject<DateRange | undefined>({
    toDate: undefined,
    fromDate: undefined,
  });

  public currentDateRange = this.dateRange.asObservable();

  private headers: HttpHeaders = new HttpHeaders();
  userDataResult: any;

  setDateRange(dateRange: DateRange | undefined) {
    this.dateRange.next(dateRange);
  }

  private readonly requestUrl: string;

  constructor(
    private httpClient: HttpClient,
    @Inject('PORTAL_API_URL') private portalApiUrl: string,
    private authService: AuthService
  ) {
    this.requestUrl = `${this.portalApiUrl}/sji3/api/TaskUnit`;
  }

  private setHeaders(): any {
    this.headers = new HttpHeaders();
    this.headers = this.headers.set('Content-Type', 'application/json');
    this.headers = this.headers.set('Accept', 'application/json');

    this.authService.getAccessToken().subscribe((token) => {
      if (token !== '') {
        const tokenValue = 'Bearer ' + token;
        this.headers = this.headers.append('Authorization', tokenValue);
      }
    });
  }

  postTaskUnit(
    taskUnit: TaskUnitPostBody
  ): Observable<HttpResponse<TaskUnitAddedResponse>> {
    this.setHeaders();
    const requestUrl = `${this.requestUrl}`;

    return this.httpClient.post<TaskUnitAddedResponse>(
      requestUrl,
      {
        applicationUserId: this.authService.userData$.value.sub,
        ...taskUnit,
      },
      {
        headers: this.headers,
        observe: 'response',
      }
    );
  }

  getTaskUnits(
    parameters: ResourceParameters
  ): Observable<HttpResponse<TaskUnitsResponse>> {
    const sortBy = `${parameters.sortBy}${
      parameters.sortDirection === 'asc' ? '' : ' desc'
    }`;
    const startFilter =
      parameters.startDate === undefined ||
      parameters.startDate === null ||
      parameters.startDate === ''
        ? ''
        : `&start=${moment(parameters.startDate).format('YYYY-MM-DD')}`;
    const endFilter =
      parameters.endDate === undefined ||
      parameters.endDate === null ||
      parameters.endDate === ''
        ? ''
        : `&end=${moment(parameters.endDate).format('YYYY-MM-DD')}`;

    const filters = `${startFilter}${endFilter}`;

    const requestUrl = `${this.requestUrl}?${
      filters === '' ? '' : `${filters}&`
    }orderBy=${sortBy}&pageSize=${parameters.pageSize}&pageNumber=${
      parameters.pageNumber + 1
    }`;
    return this.httpClient.get<TaskUnitsResponse>(requestUrl, {
      observe: 'response',
    });
  }

  updateTaskUnitStatus(
    taskUnitId: string,
    taskUnitStatus: TaskUnitStatus
  ): Observable<HttpResponse<TaskStatusChangedResponse>> {
    this.setHeaders();
    const requestUrl = `${this.requestUrl}/${taskUnitId}/UpdateTaskUnitStatus`;

    return this.httpClient.put<TaskStatusChangedResponse>(
      requestUrl,
      {
        id: taskUnitId,
        taskUnitStatusId: taskUnitStatus,
      },
      {
        headers: this.headers,
        observe: 'response',
      }
    );
  }
}
