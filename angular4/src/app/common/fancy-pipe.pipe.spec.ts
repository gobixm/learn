import {FancyPipePipe} from './fancy-pipe.pipe';

describe('FancyPipePipe', () => {
    it('create an instance', () => {
        const pipe = new FancyPipePipe();
        expect(pipe).toBeTruthy();
    });
});
