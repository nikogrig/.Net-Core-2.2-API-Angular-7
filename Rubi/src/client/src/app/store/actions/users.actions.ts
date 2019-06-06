import { Action } from '@ngrx/store';
import { UsersListModel } from 'src/app/models/read.model';

export const GET_ALL_USERS = '[USERS] Get All';
export const GET_USER_DETAIL = '[USERS] Get Detail';
export const EDIT_USER = '[USERS] Edit';

export class GetAllUsersAction implements Action {
    type: string = GET_ALL_USERS;   
    constructor(public payload: UsersListModel[]){}
}

export class GetUserDetailAction implements Action {
    type: string = GET_USER_DETAIL;   
    constructor(public payload: UsersListModel){}
}

export class EditUserAction implements Action {
    type: string = EDIT_USER;   
    constructor(public payload: UsersListModel){}
}

export type Types = GetAllUsersAction | GetUserDetailAction | EditUserAction;