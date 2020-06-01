import { TestBed } from '@angular/core/testing';

import { HomeworksService } from './homeworks.service';

describe('HomeworksService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HomeworksService = TestBed.get(HomeworksService);
    expect(service).toBeTruthy();
  });
});
