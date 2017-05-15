import { Routes } from "@angular/router";

import { Dashboard1Component } from "./views/dashboards/dashboard1.component";
import { Dashboard2Component } from "./views/dashboards/dashboard2.component";
import { Dashboard3Component } from "./views/dashboards/dashboard3.component";
import { Dashboard4Component } from "./views/dashboards/dashboard4.component";
import { Dashboard41Component } from "./views/dashboards/dashboard41.component";
import { Dashboard5Component } from "./views/dashboards/dashboard5.component";

import { StarterViewComponent } from "./views/appviews/starterview.component";
import { LoginComponent } from "./views/appviews/login.component";

import { BlankLayoutComponent } from "./components/common/layouts/blankLayout.component";
import { BasicLayoutComponent } from "./components/common/layouts/basicLayout.component";
import { TopNavigationLayoutComponent } from "./components/common/layouts/topNavigationlayout.component";
import { AgentsComponent } from "app/agents/agents.component";
import { CountryListComponent } from "app/country-list/country-list.component";

export const ROUTES: Routes = [
    // Main redirect
    { path: '', redirectTo: 'starterview', pathMatch: 'full' },
    {
        path: '',
        component: BasicLayoutComponent,
        children: [
            { path: 'agents', component: AgentsComponent }
        ]
    },

    {
        path: '',
        component: BasicLayoutComponent,
        children: [
            { path: 'starterview', component: StarterViewComponent }
        ]
    },
    {
        path: '',
        component: BlankLayoutComponent,
        children: [
            { path: 'login', component: LoginComponent },
        ]
    },

    // App views
    {
        path: 'configuration',
        component: BasicLayoutComponent,
        children: [
            { path: 'countries', component: CountryListComponent },
        ]
    },
    {
        path: 'dashboards',
        component: TopNavigationLayoutComponent,
        children: [
            { path: 'dashboard41', component: Dashboard41Component }
        ]
    },

    // Handle all other routes
    { path: '**', redirectTo: 'starterview' }
];
