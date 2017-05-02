export class Agent {
    public isEnabled: boolean;

    public countries: string[];

    public id: string;

    /**
     *
     */
    constructor(
        public hostname: string,
        public port: number
    ) {

        
    }
}
