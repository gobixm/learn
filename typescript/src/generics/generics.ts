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

export {Value};
