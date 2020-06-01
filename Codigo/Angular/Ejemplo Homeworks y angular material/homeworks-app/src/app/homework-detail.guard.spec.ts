import { TestBed, async, inject } from '@angular/core/testing';

import { HomeworkDetailGuard } from './homework-detail.guard';

describe('HomeworkDetailGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HomeworkDetailGuard]
    });
  });

  it('should ...', inject([HomeworkDetailGuard], (guard: HomeworkDetailGuard) => {
    expect(guard).toBeTruthy();
  }));
});
