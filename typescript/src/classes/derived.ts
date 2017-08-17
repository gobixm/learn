/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

import Base from './base';

export default  class Derived extends Base {
    constructor(name: string) {
        super(name);
    }

    grow(mass: number) {
        this._mass += mass;
    }
}
