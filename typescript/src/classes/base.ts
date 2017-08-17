/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

class Base {
    private readonly _name: string = 'beetle';
    protected _mass: number = 0;


    get name(): string {
        return this._name;
    }

    get mass(): number {
        return this._mass;
    }

    constructor(name: string) {
        this._name = name;
    }
}

export default Base;