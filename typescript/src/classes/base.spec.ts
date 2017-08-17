/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

import {expect} from 'chai';
import 'mocha';
import Base from "./base";

describe('get name', () => {
    it('should return name', () => {
        let base = new Base('name');
        expect(base.name).to.equal('name');
    });
});

describe('_name', () => {
    it('should be visible', () => {
        let base : any = new Base('name');
        expect(base._name).to.equal('name');
        expect(base['_name']).to.equal('name');
    });
});