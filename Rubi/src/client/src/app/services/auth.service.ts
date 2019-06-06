import { Injectable } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RegisterModel, LoginModel } from '../models/read.model';

const ulr = "https://localhost:5001/api/account"

@Injectable()
export class AuthService {
  
    private currentAuthtoken: string;
    
    constructor(private http: HttpClient, private router: Router) { 
    }
 
    get authtoken() : string {
        return this.currentAuthtoken;
    }
    set authtoken(value: string) {
        if (this.checkIfLoggedIn){
            this.currentAuthtoken = value;
        }
        this.currentAuthtoken = '';
    }

    register(registerModel: RegisterModel) : Observable<RegisterModel> {
        return this.http.post<RegisterModel>(`${ulr}/register`,  registerModel);
    }

    login(loginModel: LoginModel) : Observable<LoginModel> {
        return this.http.post<LoginModel>(`${ulr}/login`, loginModel)   
    }

    logout() : Observable<Object> {
        return this.http.post(`${ulr}/logout`, "" );
    }

     authentificate(data) {
        console.log(data)
        this.currentAuthtoken =  data.token;
        localStorage.setItem('currentUser', JSON.stringify({ 
             id: data.id,
             userName: data.username,
             userRole: data.role,
             email: data.email,
             token: data.token
        }));
        localStorage.setItem('userRole', data.role);
    }

    checkIfUserIsAdmin(): boolean {
        if (localStorage.getItem('userRole') == "Administrator"){
            return true;
        } 
        return false;
    }

    checkIfLoggedIn(): boolean {
        if (localStorage.getItem('currentUser')!= null){ //&& !this.jwtHelper.isTokenExpired(this.currentAuthtoken)) {          
            return true;
        }
        return false;
    }
}