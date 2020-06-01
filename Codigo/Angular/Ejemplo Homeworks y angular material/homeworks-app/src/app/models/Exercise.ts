export class Exercise {
    id: number;
    problem: string;
    score: number;

    constructor(id:number = 0, problem:string = "", score:number = 0) {
        this.id = id;
        this.problem = problem;
        this.score = score;
    }
}
