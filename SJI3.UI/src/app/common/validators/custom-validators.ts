import {
  AbstractControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import * as moment from 'moment';

export const fromDateTimeCompareValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const fromDateTime = moment(control.get('fromDateTime')?.value).format(
    'YYYY-MM-DDTHH:mm:ss'
  );
  const toDateTime = moment(control.get('toDateTime')?.value).format(
    'YYYY-MM-DDTHH:mm:ss'
  );

  return fromDateTime &&
    toDateTime &&
    moment(fromDateTime).isSameOrAfter(toDateTime)
    ? { fromDateTimeInvalid: true }
    : null;
};

export const toDateTimeCompareValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const toDateTime = moment(control.value).format('YYYY-MM-DDTHH:mm:ss');
  const currentDateTime = moment().format('YYYY-MM-DDTHH:mm:ss');

  return toDateTime && moment(toDateTime).isAfter(moment(currentDateTime))
    ? { toDateTimeInvalid: true }
    : null;
};

export function markFormGroupDirty(formGroup: FormGroup) {
  (<any>Object).values(formGroup.controls).forEach((control: any) => {
    control.markAsDirty();
  });
}
