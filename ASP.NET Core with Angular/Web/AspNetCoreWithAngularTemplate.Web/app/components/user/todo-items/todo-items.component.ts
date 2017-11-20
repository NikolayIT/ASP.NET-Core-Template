import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { TodoItemsDataService } from '../../../services/index';

import { TodoItem } from '../../../domain/index';

@Component({
    moduleId: module.id,
    selector: 'todo-items',
    templateUrl: 'todo-items.component.html'
})

export class TodoItemsComponent implements OnInit {
    constructor(private todoItemsDataService: TodoItemsDataService) { }

    public todoItems: TodoItem[] = [];
    public errorMessage: string = null;

    ngOnInit() {
        this.todoItemsDataService.getAll().subscribe((data: TodoItem[]) => this.todoItems = data);
    }

    public markAsDone(id: number): void {
        this.todoItemsDataService.markAsDone(id).subscribe(
            () => {
                this.errorMessage = null;

                const todoItem = this.todoItems.find(t => t.id == id);
                if (todoItem) {
                    todoItem.isDone = true;
                }
            },
            (error: HttpErrorResponse) => this.errorMessage = error.error || 'Mark TODO as done failed.');
    }
}
