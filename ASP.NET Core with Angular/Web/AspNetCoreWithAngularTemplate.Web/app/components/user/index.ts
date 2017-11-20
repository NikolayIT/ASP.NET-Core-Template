import { TodoItemCreateComponent } from './todo-items/todo-item-create.component';
import { TodoItemsComponent } from './todo-items/todo-items.component';

import { UserComponent } from './user.component';

export * from './todo-items/todo-item-create.component';
export * from './todo-items/todo-items.component';

export * from './user.component';

export const USER_COMPONENTS = [
    TodoItemCreateComponent,
    TodoItemsComponent,

    UserComponent
];
