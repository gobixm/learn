/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

import {expect} from 'chai';
import 'mocha';
import {decorator, identity, digits, overload} from "./functions";

describe('decorator', () => {
    it('should frame default', () => {
        let decorate = decorator();
        let result = decorate('content');
        expect(result).to.equal('>\ncontent\n<');
    });
});

describe('decorator', () => {
    it('should frame', () => {
        let decorate = decorator('<<<', '>>>');
        let result = decorate('content');
        expect(result).to.equal('<<<content>>>');
    });
});

describe('decorator', () => {
    it('should frame undefined', () => {
        let decorate = decorator(undefined, undefined);
        let result = decorate('content');
        expect(result).to.equal('>\ncontent\n<');
    });
});

describe('identity', () => {
    it('should return empty aka', () => {
        let identify = identity('john');
        expect(identify()).to.equal('john');
    });
});

describe('identity', () => {
    it('should return aka', () => {
        let identify = identity('john', 'the snake', 'emperor of mankind');
        expect(identify()).to.equal('john aka the snake, emperor of mankind');
    });
});

describe('digits', () => {
    it('should bind this', () => {
        let onePicker = digits.picker(1);
        let ninePicker = digits.picker(9);
        expect(onePicker()).to.equal('one');
        expect(ninePicker()).to.equal('nine');
    });
});

describe('overload', () => {
    it('should guess type', () => {
        expect(overload(1)).to.equal('n');
        expect(overload('1')).to.equal('s');
    });
});