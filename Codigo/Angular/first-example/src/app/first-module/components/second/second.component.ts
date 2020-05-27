import { Component, OnInit, Input, DoCheck, OnChanges, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-second',
  templateUrl: './second.component.html',
  styleUrls: ['./second.component.css']
})
export class SecondComponent implements OnInit {

  constructor() { }

  @Input() texto: string;

  @Output() aEnviar = new EventEmitter<string>();

  textoEnviar :string = "";

  ngOnInit(): void {
  }

  enviar(event :any) {
    this.aEnviar.emit(this.textoEnviar)
  }

}
