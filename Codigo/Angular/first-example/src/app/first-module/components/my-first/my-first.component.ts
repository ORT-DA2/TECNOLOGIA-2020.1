import { Component, OnInit } from '@angular/core';
import { FirstModuleModuleService } from '../../../my-service/first-module-module.service';

@Component({
  selector: 'app-my-first',
  templateUrl: './my-first.component.html',
  styleUrls: ['./my-first.component.css'],
})
export class MyFirstComponent implements OnInit {
  names: string[];
  shows: boolean;
  input: string;
  text: string;
  title: string;

  constructor(private _service: FirstModuleModuleService) {
    this.names = [];
    this.shows = true;
    this.input = '';
    this.text = 'AaBbCcDd';
    this.title = 'titlecase';
  }

  ngOnInit(): void {
    /*this.names.push('Nico');
    this.names.push('Ale');*/
    this.names = this._service.getNames();
  }

  clickButton() {
    alert('Acaso no sabes leer!');
  }

  clickList(name: string) {
    alert('Nombre: ' + name);
  }
}
