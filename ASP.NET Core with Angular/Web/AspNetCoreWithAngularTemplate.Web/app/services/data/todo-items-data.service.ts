import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { TodoItem } from '../../domain/todo-item';

@Injectable()
export class TodoItemsDataService {
    public static readonly URLS = {
        ALL: 'api/todoitems/all'
    };

    constructor(private httpClient: HttpClient) { }

    public getAll(): Observable<TodoItem[]> {
        return this.httpClient.get<TodoItem[]>(TodoItemsDataService.URLS.ALL);
    }
}
