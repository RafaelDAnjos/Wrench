import { TestBed } from '@angular/core/testing';

import { DemandaService } from './demanda.service';

describe('DemandaService', () => {
  let service: DemandaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DemandaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
