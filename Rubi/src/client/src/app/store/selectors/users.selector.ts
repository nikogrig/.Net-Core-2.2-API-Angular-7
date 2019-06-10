import { createSelector } from '@ngrx/store';
import { AdminState } from '../admin.state';

const selectItems = (state: AdminState) => state.users.all;

export const getItemById = (id : string) => createSelector(selectItems, (allUsers) => {
  if (allUsers) {
    return allUsers.find(item =>  item.id == id);
  } else {
    return {};
  }
});