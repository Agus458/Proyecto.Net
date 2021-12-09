import { TestBed } from '@angular/core/testing';

import { NoveltiesService } from './novelties.service';

describe('NoveltiesService', () => {
  let service: NoveltiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NoveltiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
