/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

import {expect} from 'chai';
import 'mocha';
import Derived from "./derived";

describe('grow', () => {
    it('should increment', () => {
        let derived = new Derived('name');
        derived.grow(10);
        expect(derived.mass).to.equal(10);
        derived.grow(3);
        expect(derived.mass).to.equal(13);
    });
});