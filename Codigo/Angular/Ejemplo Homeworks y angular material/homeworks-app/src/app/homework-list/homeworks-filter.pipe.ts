import { Homework } from './../models/Homework';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'homeworksFilter'
})
export class HomeworksFilterPipe implements PipeTransform {

  transform(list: Array<Homework>, args: string): Array<Homework> {
    return list.filter(
      x => x.description.toLocaleLowerCase()
      .includes(args.toLocaleLowerCase())
    );
  }

}
