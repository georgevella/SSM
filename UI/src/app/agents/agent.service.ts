import { Agent } from './agent';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AgentService {
    private agentsApi = 'api/agents/';
    private agentRegistrationQueueApi = 'api/agent-registration-queue/';

    constructor(private http: Http) {

    }

    getAllApprovedAgents(): Observable<Agent[]> {
        return this.http.get(this.agentsApi).catch(this.handleError)
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
