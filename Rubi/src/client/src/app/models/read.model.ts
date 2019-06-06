export enum Role {
  User = 'Customer',
  Admin = 'Administrator'
}

export class LoginModel {
    constructor( 
      public email: string,
      public password: string){      
      }
   }

   export class RegisterModel {
    constructor(
    public username: string,
    public password: string,
    public confirmPassword: string,
    public email: string,
    public address: string,
    public phoneNumber: string,
    public firstName: string,
    public lastName: string,
    public birthDate: string) {}
}   

export class UsersListModel {
  constructor(
      public id: string,
      public username: string,
      public email: string,
      public address: string,
      public phoneNumber: string,
      public firstName: string,
      public lastName: string,
      public birthDate: string) {}
}

export class UserProfileModel {
  constructor(
      public id: string,
      public username: string,
      //public profilePicture: [],
      public email: string,
      public firstName: string,
      public lastName: string,
      public birthDate: string,
      public address: string,
      public phoneNumber: string,
      public role: string) {}
}
