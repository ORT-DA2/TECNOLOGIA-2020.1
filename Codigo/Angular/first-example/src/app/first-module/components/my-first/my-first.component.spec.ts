import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyFirstComponent } from './my-first.component';

describe('MyFirstComponent', () => {
  let component: MyFirstComponent;
  let fixture: ComponentFixture<MyFirstComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyFirstComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyFirstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
