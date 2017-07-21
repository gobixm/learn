/**
 * Created by Ivanov.Kirill on 21.07.2017.
 */

interface Repeat {
    (i:number): string;
}

const repeat = function (repeater: Repeat, times: number): string {
    let result = '';
    for (let i: number = 0; i < times; i++) {
        result += repeater(i);
    }
    return result;
};

export default repeat;