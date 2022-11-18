import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'humanize',
})
export class HumanizePipe implements PipeTransform {
  transform(value: string, ...args: any[]): string {
    return value ? value.replace(/([a-z0-9])([A-Z])/g, '$1 $2') : value;
  }
}
