import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filtros'
})
export class FiltroPipe implements PipeTransform {

  transform(lista: string[], filtro: string): string[] {
    return lista.filter(
      x => x.toLocaleLowerCase()
      .includes(filtro.toLocaleLowerCase())
    );
  }

}
