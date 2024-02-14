import { Component, OnInit } from '@angular/core'
import { ContactService } from '../../services/contact.service'
import { ContactModel } from '../../models/Contacts/contactModel'
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { IdentityService } from '../../services/identity.service';

@Component({
  templateUrl: './details.component.html',
  styleUrl: './details.component.css',
  imports: [CommonModule],
  standalone: true
})
export class DetailsComponent implements OnInit{
static contactId:string;
contact:ContactModel;
logged: boolean;
static detailsContact:ContactModel;
constructor(private contactService:ContactService,identityService:IdentityService,private router: Router) {
    this.contact = {id:"",name:"",surname:"",email:"",password:"",phoneNumber:"",category:""};
    this.logged = identityService.isLogged();
 }
ngOnInit(): void{
  this.contactService.getContact(DetailsComponent.contactId).subscribe((data: any) => {
  this.contact = data});
  DetailsComponent.detailsContact = this.contact;
}
update(){
  DetailsComponent.detailsContact = this.contact;
  this.router.navigate(['/update']);
}
}