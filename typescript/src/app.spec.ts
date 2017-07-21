/**
 * Created by Ivanov.Kirill on 21.07.2017.
 */

import App from './app';
import { expect } from 'chai';
import 'mocha';

describe('hello function', () => {
    it('should return hello world', () => {
        const result = App.hello("Kirill");
        expect(result).to.equal('Hello Kirill!');
    });
});