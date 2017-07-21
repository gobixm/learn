/**
 * Created by Ivanov.Kirill on 21.07.2017.
 */

import repeater from "./repeat";
import {expect} from 'chai';
import 'mocha';

describe('repeat', () => {
    it('should repeat', () => {
        const result = repeater((i:number) => `${i};`, 10);

        expect(result).to.equal('0;1;2;3;4;5;6;7;8;9;');
    });
});