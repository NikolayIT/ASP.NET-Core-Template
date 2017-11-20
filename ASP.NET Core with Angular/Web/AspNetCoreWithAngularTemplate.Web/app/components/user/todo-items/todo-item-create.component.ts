import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { TodoItemsDataService, RouterService } from '../../../services/index';

import { TodoItem } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'todo-item-create',
    templateUrl: 'todo-item-create.component.html'
})

export class TodoItemCreateComponent {
    constructor(private todoItemsDataService: TodoItemsDataService, private routerService: RouterService) { }

    public todoItem: TodoItem = new TodoItem();
    public todoItemErrorMessage: string = null;

    public create(): void {
        this.todoItemsDataService.create(this.todoItem).subscribe(
            () => this.routerService.navigateByUrl('/user/todo-items'),
            (error: HttpErrorResponse) => this.todoItemErrorMessage = error.error || 'Create TODO item failed.');
    }
}
