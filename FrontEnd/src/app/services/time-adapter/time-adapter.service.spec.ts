import { TestBed } from '@angular/core/testing';

import { TimeAdapterService } from './time-adapter.service';

describe('TimeAdapterService', () => {
  let service: TimeAdapterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TimeAdapterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
