/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

import {expect} from 'chai';
import 'mocha';
import {Value, identity, GenericIdentity, traitIdentity} from "./generics";

describe('construct', () => {
    it('should construct from value', () => {
        let value = new Value<number>(1);
        expect(value.value).to.equal(1);

    });
});

describe('identity', () => {
    it('should return identity', () => {
        let i = identity<string>("1");
        let numericIdentity = (arg: number) => identity<number>(arg);
        let myIdentity: <T>(arg: T) => T = identity;
        expect(i).to.equal('1');
        expect(numericIdentity(1)).to.equal(1);
        expect(myIdentity([1])).to.deep.equal([1]);
    });
});

describe('identity interface', () => {
    it('should return identity', () => {
        let numberIdentity: GenericIdentity<number> = identity;
        expect(numberIdentity(1)).to.equal(1);
    });
});

describe('generic constraints', () => {
    it('should work', () => {
        expect(traitIdentity({name: 'aaa'})).to.deep.equal({name: 'aaa'});
    });
});