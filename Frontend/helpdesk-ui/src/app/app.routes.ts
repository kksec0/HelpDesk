import { Routes } from '@angular/router';
import { Layout } from './admin/layout/layout';
import { Dashboard } from './admin/dashboard/dashboard';
import { Tickets } from './admin/tickets/tickets';

export const routes: Routes = [

    {
    path: 'admin',
    component: Layout,
    children: [
        {
        path: 'dashboard',
        component: Dashboard
        },
        {
        path: 'tickets',
        component: Tickets
        },

        {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
        }
    ]
    },

    {
        path: '',
        redirectTo: 'admin/dashboard',
        pathMatch: 'full'
    }
];
