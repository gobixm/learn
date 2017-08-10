/**
 * Created by Ivanov.Kirill on 10.08.2017.
 */

import Snake from "./snake";
import {expect} from 'chai';
import 'mocha';
import Animal from "./animal";

describe('move', () => {
    it(' should accumulate distance', () => {
        let animal = new Snake("gogi");
        expect(animal.overallDistance).to.equal(0);
        animal.move(10);
        expect(animal.overallDistance).to.equal(10);
        animal.move(10);
        expect(animal.overallDistance).to.equal(20);
    });
});

describe('get population', () => {
    it(' should accumulate instances', () => {
        let population = Animal.population;
        let animal1 = new Snake("gogi");
        expect(Animal.population).to.equal(population + 1);
        let animal2 = new Snake("bobby");
        expect(Animal.population).to.equal(population + 2);
    });
});