/**
 * Created by Ivanov.Kirill on 17.08.2017.
 */

const decorator = (header: string = ">\n", footer: string = "\n<") => {
    return (content: string) => `${header}${content}${footer}`;
};

const identity = (name: string, ...aliases: string[]) => {
    let aka = aliases.length ? ` aka ${aliases.join(', ')}` : '';
    return () => `${name}${aka}`
};

const digits = {
    digits: ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'],
    picker: function (digit: number) {
        return () => this.digits[digit];
    }
};

export function overload(x: string): string;
export function overload(x: number): string;
export function overload(x): any {
    if (typeof x == 'number') {
        return 'n';
    }
    if (typeof x == 'string') {
        return 's';
    }
    return typeof x;
}

export {decorator, identity, digits};