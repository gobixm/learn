/**
 * Created by Ivanov.Kirill on 21.07.2017.
 */

import Mood from './mood'

class Monster {
    isAlive: boolean = true;
    age: number = 0;
    name: string = 'gogi';
    kills: string[] = [];
    parents: [Monster, Monster] = [undefined, null];
    mood: Mood = Mood.Happy;
    metadeta: any;
}

export default Monster;
