import { Injectable } from '@angular/core';
import { ɵNAMESPACE_URIS } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class FirstModuleModuleService {

  constructor() { }

  getNames(): string[]{
    let names: string[] = [];
    names.push('Nico');
    names.push('Ale');
    return names;
  }
}
