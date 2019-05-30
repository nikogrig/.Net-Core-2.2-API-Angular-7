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

export enum Role {
    User = 'Customer',
    Admin = 'Administrator'
  }