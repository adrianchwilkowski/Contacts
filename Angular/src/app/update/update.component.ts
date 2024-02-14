import { Component, OnInit } from '@angular/core'
import { ContactService } from '../../services/contact.service'
import { ContactModel } from '../../models/Contacts/contactModel'
import { CommonModule } from '@angular/common';
import { DetailsComponent } from '../details/details.component';
import { ReactiveFormsModule, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Categories} from '../../enums/categories'
import { Subcategories } from '../../enums/subcategories';

@Component({
  templateUrl: './update.component.html',
  styleUrl: './update.component.css',
  imports: [CommonModule, ReactiveFormsModule],
  standalone: true
})
export class UpdateComponent implements OnInit{
updateForm: FormGroup;
contact: ContactModel;
name: string = "";
categories = Categories;
subcategories = Subcategories;
category:string ="";
constructor(private contactService:ContactService,  private formBuilder: FormBuilder) {
    this.updateForm = this.formBuilder.group({});
    this.contact = {
      id:"",
      name:"",
      surname:"",
      email:"",
      password:"",
      phoneNumber:"",
      category:""}
 }
ngOnInit(): void{
    this.updateForm = this.formBuilder.group({
        name: [DetailsComponent.detailsContact.name, Validators.required],
        surname: [DetailsComponent.detailsContact.surname, Validators.required],
        email: [DetailsComponent.detailsContact.email, Validators.required],
        password: [DetailsComponent.detailsContact.password, Validators.required],
        phoneNumber: [DetailsComponent.detailsContact.phoneNumber, Validators.required],
        category: [DetailsComponent.detailsContact.category, Validators.required],
        subcategory: [DetailsComponent.detailsContact.subcategory, Validators.required],
      });
      this.name = DetailsComponent.detailsContact.name;
      this.category = DetailsComponent.detailsContact.category;
}
update(): void{
  this.contactService.getContactList();
  this.contact = {
  id: DetailsComponent.detailsContact.id,
  name: this.updateForm.get('name')?.value,
  surname: this.updateForm.get('surname')?.value,
  email: this.updateForm.get('email')?.value,
  password: this.updateForm.get('password')?.value,
  phoneNumber: this.updateForm.get('phoneNumber')?.value,
  category: this.updateForm.get('category')?.value,
  subcategory: this.updateForm.get('subcategory')?.value
  };
  this.contactService.updateContact(this.contact).subscribe();
  
}

onCategoryChange(category: any) {
  this.category = category.value;
}
}