import { AgentService } from './agent.service';
import { Agent } from './agent';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-agents',
  providers: [AgentService],
  templateUrl: './agents.component.html'
})
export class AgentsComponent implements OnInit {

  // agents = [
  //   new Agent('129.0.45.1', 8091),
  //   new Agent('129.0.45.2', 8091),
  // ];

  activeAgents: Agent[];
  errorMessage: string;

  constructor(private agentService: AgentService) { }
  fetchAgents() {
    this.agentService.getAllApprovedAgents().subscribe(
      agents => this.activeAgents = agents,
      error => this.errorMessage = <any>error
    );
  }
  ngOnInit() {
    this.fetchAgents();
  }

}
