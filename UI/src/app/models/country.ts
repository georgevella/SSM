export class Country {
    public id: string;
    public displayname: string;
    public isEnabled: boolean;

    constructor (id: string, displayname: string, isEnabled: boolean)
    {
        this.id = id;
        this.displayname = displayname;
        this.isEnabled = isEnabled;
    }
}
