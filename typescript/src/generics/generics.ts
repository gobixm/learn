/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

class Value<T> {
    private readonly _value: T;

    get value(): T {
        return this._value;
    }

    constructor(value: T) {
        this._value = value;
    }
}

function identity<T>(arg: T): T {
    return arg;
}

interface Trait {
    name: string;
}

interface GenericIdentity<T> {
    (arg: T): T;
}

function traitIdentity<T extends Trait>(arg: T) {
    return identity<T>(arg);
}

export {Value, identity, GenericIdentity, Trait, traitIdentity};
