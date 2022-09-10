import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {

  // If find big letter adds space before it
  transform(enumObject): object {
    return Object.keys(enumObject).filter(e => isNaN(+e)).map(o => { return {name: o, value: enumObject[o]}});
  }

}
