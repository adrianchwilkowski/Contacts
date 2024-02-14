import { Component, OnInit } from '@angular/core'
import { ContactService } from '../../services/contact.service'
import { ContactModel } from '../../models/Contacts/contactModel'
import { CommonModule } from '@angular/common';
import { DetailsComponent } from '../details/details.component';
import { Router } from '@angular/router';
import { IdentityService } from '../../services/identity.service';

@Component({
  templateUrl: './contactList.component.html',
  styles: [`.contact-headers {
    display: flex;
    align-items: center;
    font-weight: bold;
    margin-bottom: 0.5em;
  }
  
  .contact-data {
    display: flex;
    align-items: center;
    margin-bottom: 0.5em;
  }
  .contact-column,
  .contact-value {
    flex: 1; /* Distribute space evenly */
  }
  .contact-actions {
  display: flex;
  justify-content: flex-end; /* Align buttons to right */
  margin-left: auto; /* Adjust as needed */
}
.contact-actions button {
  margin-left: 0.5em;
}`],
  imports: [CommonModule],
  standalone: true
})
export class ContactListComponent implements OnInit{
contactList:ContactModel[] = [] ;
logged: boolean = false;

constructor(private contactService:ContactService,private router: Router
  , private identityService: IdentityService) { }

ngOnInit(): void{
  this.contactService.getContactList().subscribe((data: any[]) => {
  this.contactList = data});
  this.logged = this.identityService.isLogged();
}
onDelete(contactId: string) {
  this.contactService.deleteContact(contactId).subscribe(
    ()=>{
      this.contactService.getContactList().subscribe((data: any[]) => {this.contactList = data})
    }
  );
  
  }
onDetail(contactId: string) {
  DetailsComponent.contactId = contactId;
  this.router.navigate(['/details']);
  }
}