import { Injectable } from '@angular/core';
import { environment } from '../config/environment';
import { ApiPaths } from '../config/apiPaths';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { ContactModel } from '../models/Contacts/contactModel';
import { AddContact } from '../models/Contacts/addContact';
import { GetContactList } from '../models/Contacts/getContactList';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private readonly apiUrl = environment.baseUrl + ApiPaths.Contacts;

  constructor(private httpClient: HttpClient) { }

  getContact(contactId:string) : Observable<ContactModel>{
    return this.httpClient.get<ContactModel>(`${this.apiUrl}/Get/${contactId}`);
  }
  getContactList() : Observable<GetContactList[]>{
    return this.httpClient.get<GetContactList[]>(`${this.apiUrl}/GetList`);
  }
  addContact(contact:AddContact) : Observable<string>{
    return this.httpClient.post<string>(`${this.apiUrl}/Add`, contact);
  }
  updateContact(contact:ContactModel) : Observable<void>{
    return this.httpClient.put<void>(`${this.apiUrl}/Update`, contact);
  }
  deleteContact(contactId:string) : Observable<void>{
    return this.httpClient.delete<void>(`${this.apiUrl}/Delete/${contactId}`);
  }
}