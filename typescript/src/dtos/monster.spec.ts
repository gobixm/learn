/**
 * Created by Ivanov.Kirill on 21.07.2017.
 */

import Monster from './monster';
import {expect} from 'chai';
import 'mocha';
import Mood from "./mood";

describe('constructor', () => {
    it('should return dto with default values', () => {
        const monster = new Monster();

        expect(monster.isAlive).to.equal(true);
        expect(monster.age).to.equal(0);
        expect(monster.kills).to.deep.equal([]);
        expect(monster.metadeta).to.be.undefined;
        expect(monster.mood).to.equal(Mood.Happy);
        expect(monster.name).to.equal('gogi');
        expect(monster.parents[0]).to.be.undefined;
        expect(monster.parents[1]).to.be.null;
    });
});

describe('kills', () => {
    it('should destructurize', () => {
        const monster = new Monster();
        monster.kills = ['a', 'b', 'c', 'd'];

        let [first, second, ...tail] = monster.kills;

        expect(first).to.equal('a');
        expect(second).to.equal('b');
        expect(tail).to.deep.equal(['c', 'd']);
    });
});

describe('kills', () => {
    it('should spread', () => {
        const monster = new Monster();
        const kills = ['a', 'b', 'c', 'd'];
        monster.kills = [...kills, ...kills];

        expect(monster.kills).to.deep.equal(['a', 'b', 'c', 'd', 'a', 'b', 'c', 'd']);
    });
});

describe('metadata', () => {
    it('should destructurize', () => {
        const monster = new Monster();
        monster.metadeta = {
            a: 'a',
            b: 'b',
            c: 'c',
            d: 'd'
        };

        let {c, d: alias, e = 'e', ...other} = monster.metadeta;

        expect(c).to.equal('c');
        expect(alias).to.equal('d');
        expect(e).to.equal('e');
        expect(other).to.deep.equal({a: 'a', b: 'b'});
    });
});

describe('metadata', () => {
    it('should spread', () => {
        const monster = new Monster();
        let first = {
            a: 'a',
            b: 'b'
        };

        let second = {
            c: 'c',
            d: 'd'
        };

        monster.metadeta = {
            ...first,
            ...second
        };

        expect(monster.metadeta).to.deep.equal({
            a: 'a',
            b: 'b',
            c: 'c',
            d: 'd'
        });
    });
});