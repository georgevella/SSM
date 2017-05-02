export class Agent {
    isEnabled: boolean;

    countries: string[];

    /**
     *
     */
    constructor(
        public hostname: string,
        public port: number
    ) {

        
    }
}
