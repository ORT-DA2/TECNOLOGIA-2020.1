import { TestBed } from '@angular/core/testing';

import { EjemploInterceptorService } from './ejemplo-interceptor.service';

describe('EjemploInterceptorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EjemploInterceptorService = TestBed.get(EjemploInterceptorService);
    expect(service).toBeTruthy();
  });
});
