import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import {RouterModule} from "@angular/router";
import {LocationStrategy, HashLocationStrategy} from '@angular/common';

import {ROUTES} from "./app.routes";
import { AppComponent } from './app.component';

// App views
import {DashboardsModule} from "./views/dashboards/dashboards.module";
import {AppviewsModule} from "./views/appviews/appviews.module";

// App modules/components
import {LayoutsModule} from "./components/common/layouts/layouts.module";
import { AgentsComponent } from './agents/agents.component';
import { CountryListComponent } from './country-list/country-list.component';

import { AgentService } from './agents/agent.service';
import { CountryService } from './country.service';

@NgModule({
  declarations: [
    AppComponent,
    AgentsComponent,
    CountryListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    DashboardsModule,
    LayoutsModule,
    AppviewsModule,
    RouterModule.forRoot(ROUTES)
  ],
  providers: [
    {provide: LocationStrategy, useClass: HashLocationStrategy},
    CountryService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
