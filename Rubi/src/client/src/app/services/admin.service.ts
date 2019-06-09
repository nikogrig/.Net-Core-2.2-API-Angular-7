import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Store } from "@ngrx/store";
import { AdminState } from "../store/admin.state";
import { GetAllUsersAction } from "../store/actions/users.actions";
import { Observable } from "rxjs";
import { CreateUserModel } from '../models/write.model';
import { UsersListModel } from '../models/read.model';


const url = "https://localhost:5001/api/admin"

@Injectable()
export class AdminService {

    constructor(private http: HttpClient, private store: Store<AdminState>) {
     }

    getRoles(){
        return this.http.get(`${url}/get-roles`);
    }


    getAllUsers() {
        return this.http.get(`${url}/get-users`);
    }

    createUser(registerModel: CreateUserModel) : Observable<CreateUserModel> {
        return this.http.post<CreateUserModel>(`${url}/create-user`,  registerModel);
    }

    deleteUser(id: string){
        return this.http.delete(`${url}/delete-user/${id}`);       
    }

    // getUserDetail(id: string){
    //     return this.http.get(`${url}/user-detail/${id}`);
    // }

    editUser(id: string, user: UsersListModel){
        return this.http.patch(`${url}/edit-user/${id}`, user);
    }

    // TODO: show updated response in pop-up modal component
    // getEditedUserData(response){
    //     let updatedUser : UsersListModel = new UsersListModel( response.id,
    //         response.username, 
    //         response.email, 
    //         response.address, 
    //         response.phoneNumber, 
    //         response.firstName, 
    //         response.lastName, 
    //         response.birthdate);
    //     return updatedUser;
    // }

    // takeUserDetail(response) {
    //     let user : UsersListModel = new UsersListModel( response.id,
    //         response.username, 
    //         response.email, 
    //         response.address, 
    //         response.phoneNumber, 
    //         response.firstName, 
    //         response.lastName, 
    //         response.birthdate);

    //     this.store.dispatch(new GetUserDetailAction(user))
    // }

    getUsersFromStore(response: HttpResponse<any>): void {
        console.log(response)
        const users: UsersListModel[]  = [];             
                for (let i of Object.keys(response)) {
                    users.push(new UsersListModel(
                        response[i].id,
                        response[i].username, 
                        response[i].email, 
                        response[i].address, 
                        response[i].phoneNumber, 
                        response[i].firstName, 
                        response[i].lastName, 
                        response[i].birthdate))
          }
          this.store.dispatch(new GetAllUsersAction(Object.values(users)))
    }
}