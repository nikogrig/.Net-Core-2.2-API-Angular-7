import { UsersListModel } from 'src/app/models/read.model';

export interface UsersState {
    all: UsersListModel[],
    detail: UsersListModel,
    edit: UsersListModel
}