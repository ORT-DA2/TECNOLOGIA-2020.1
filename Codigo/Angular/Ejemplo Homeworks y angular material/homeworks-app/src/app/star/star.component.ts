import { Component, OnChanges, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.css']
})

export class StarComponent implements OnChanges {

  @Input() rating: number;
  @Output() ratingClicked: EventEmitter<string> = new EventEmitter<string>();
  starWidth: number;
  Arr = Array;

  ngOnChanges():void {
    this.starWidth = this.rating * 86/5;
  }

  onClick(): void {
    this.ratingClicked.emit(`El raiting es ${this.rating}!`);
  }

}
