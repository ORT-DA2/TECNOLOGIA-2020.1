import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeworkListComponent } from './homework-list.component';

describe('HomeworkListComponent', () => {
  let component: HomeworkListComponent;
  let fixture: ComponentFixture<HomeworkListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeworkListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeworkListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
