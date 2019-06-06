import { UsersState } from "../states/users.state";
import * as UsersActions from "../actions/users.actions";

const initialState: UsersState = {
    all: [],
    detail: null,
    edit: null
}

function getAllUsers(state, allUsers) {
    return Object.assign({}, state, {
        all: allUsers                     
    }); 
}

function getUserDetail(state, userDetail) {
    return Object.assign({}, state, { //(baseObject, dataObject, ObjectForUpdate)
        detail: userDetail                    // create new obj, get state and put users in him 
    }); 
}

function editUser(state, user){
    return Object.assign({}, state, {
        edit: user 
    });
}

export function usersReducer (
    state: UsersState = initialState, 
    action: UsersActions.Types) {  
        switch(action.type){
        case UsersActions.EDIT_USER:
            return editUser(state, action.payload);
        case UsersActions.GET_USER_DETAIL:
            return getUserDetail(state, action.payload)
        case UsersActions.GET_ALL_USERS:
            return getAllUsers(state, action.payload)
        default: return state; // return initial state - don't forget!
    }
}