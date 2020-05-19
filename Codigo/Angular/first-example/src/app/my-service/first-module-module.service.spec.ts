import { TestBed } from '@angular/core/testing';

import { FirstModuleModuleService } from './first-module-module.service';

describe('FirstModuleModuleService', () => {
  let service: FirstModuleModuleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FirstModuleModuleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
