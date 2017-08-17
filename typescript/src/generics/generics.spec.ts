/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

import {expect} from 'chai';
import 'mocha';
import {Value} from "./generics";

describe('construct', () => {
    it('should construct from value', () => {
        let value = new Value<number>(1);
        expect(value.value).to.equal(1);

    });
});
