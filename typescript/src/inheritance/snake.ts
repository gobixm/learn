/**
 * Created by Ivanov.Kirill on 10.08.2017.
 */

import Animal from './animal'

class Snake extends Animal {
    move(distance = 5) {
        super.move(distance);
    }
}

export default Snake;