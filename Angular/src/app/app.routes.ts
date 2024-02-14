import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ContactListComponent } from './contactList/contactList.component';
import { DetailsComponent } from './details/details.component';
import { UpdateComponent } from './update/update.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: '', component: ContactListComponent },
    { path: 'details', component: DetailsComponent },
    { path: 'update', component: UpdateComponent }
];
