/**
 * Created by Ivanov.Kirill on 10.08.2017.
 */

class Animal {
    readonly name: string;
    private static _population: number = 0;
    private _overallDistance: number;

    static get population(): number {
        return this._population;
    }

    get overallDistance(): number {
        return this._overallDistance;
    }

    constructor(name: string) {
        this.name = name;
        this._overallDistance = 0;
        Animal._population++;
    }

    move(distance: number) {
        this._overallDistance += distance;
    }
}

export default Animal;