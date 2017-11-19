import { Component, OnInit } from '@angular/core';

import { TodoItemsDataService } from '../services/index';

import { TodoItem } from '../domain/index';

@Component({
    moduleId: module.id,
    selector: 'home',
    templateUrl: 'home.component.html', 
})

export class HomeComponent implements OnInit {
    constructor(private todoItemsDataService: TodoItemsDataService) { }

    public todoItems: TodoItem[] = [];

    ngOnInit() {
        this.todoItemsDataService.getAll().subscribe(
            data => this.todoItems = data);
    }
}
