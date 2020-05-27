import { FiltroPipe } from './first-module/filtro.pipe';

describe('FiltroPipe', () => {
  it('create an instance', () => {
    const pipe = new FiltroPipe();
    expect(pipe).toBeTruthy();
  });
});
