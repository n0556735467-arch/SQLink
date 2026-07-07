import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'create-ticket', pathMatch: 'full' },
  { path: 'create-ticket', loadComponent: () => import('./features/create-ticket/create-ticket').then(m => m.CreateTicketComponent) },
  { path: 'ticket/:id', loadComponent: () => import('./features/view-ticket/view-ticket').then(m => m.ViewTicket) },
  { path: 'login', loadComponent: () => import('./features/login/login').then(m => m.Login) },
  { path: 'admin', loadComponent: () => import('./features/admin-dash/admin-dash').then(m => m.AdminDash), canActivate: [authGuard] }
];