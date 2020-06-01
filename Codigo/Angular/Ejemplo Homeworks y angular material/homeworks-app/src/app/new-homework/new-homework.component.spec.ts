import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewHomeworkComponent } from './new-homework.component';

describe('NewHomeworkComponent', () => {
  let component: NewHomeworkComponent;
  let fixture: ComponentFixture<NewHomeworkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewHomeworkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewHomeworkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
