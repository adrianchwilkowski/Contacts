export interface AddContact {
    name: string;
    surname: string;
    password: string;
    email: string;
    phoneNumber: string;
    category: string;
    subcategory?: string;
  }