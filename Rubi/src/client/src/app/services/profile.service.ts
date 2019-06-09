import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { UserProfileModel } from '../models/read.model';
import { EditProfileModel } from '../models/write.model';


const url = "https://localhost:5001/api/profile"

@Injectable()
export class ProfileService {

    constructor(private http: HttpClient, private router: Router){ }

    getUser(id: string) : Observable<UserProfileModel>{
        return this.http.get<UserProfileModel>(`${url}/${id}`);
    }

    updateProfileData(id: string, user: EditProfileModel) : Observable<EditProfileModel> {
        return this.http.patch<EditProfileModel>(`${url}/edit/${id}`, user);      
    }
}