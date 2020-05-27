import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ejemplo'
})
export class EjemploPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
