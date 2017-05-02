import { Agent } from './agent';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AgentService {
    private agentsApi = 'api/v1/agents/';
    private agentRegistrationQueueApi = 'api/v1/agent-registration-queue/';

    constructor(private http: Http) {

    }

    getAllApprovedAgents(): Observable<Agent[]> {
        return this.http.get(this.agentsApi)
            .map(this.extractData)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        const body : any[] = res.json();
        const result = [];

        // tslint:disable-next-line:forin
        for (const i of body)
        {
            const agent = new Agent(i['hostname'], i['port']);
            agent.isEnabled = true;
            agent.id = i['id'];
            result.push( agent );
        }
        return result;
    }

    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
