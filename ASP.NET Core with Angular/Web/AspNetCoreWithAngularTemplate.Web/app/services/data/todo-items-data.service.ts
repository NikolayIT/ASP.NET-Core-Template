import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { TodoItem } from '../../domain/todo-item';

@Injectable()
export class TodoItemsDataService {
    private static readonly URLS = {
        ALL: 'api/todoitems/all',
        CREATE: 'api/todoitems/create',
        MARK_AS_DONE: 'api/todoitems/markasdone/'
    };

    constructor(private httpClient: HttpClient) { }

    public getAll(): Observable<TodoItem[]> {
        return this.httpClient.get<TodoItem[]>(TodoItemsDataService.URLS.ALL);
    }

    public create(todoItem: TodoItem): Observable<any> {
        return this.httpClient.post(TodoItemsDataService.URLS.CREATE, todoItem);
    }

    public markAsDone(id: number): Observable<any> {
        return this.httpClient.post(`${TodoItemsDataService.URLS.MARK_AS_DONE}${id}`, null);
    }
}
