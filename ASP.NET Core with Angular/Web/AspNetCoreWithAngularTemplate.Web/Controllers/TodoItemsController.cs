namespace AspNetCoreWithAngularTemplate.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreWithAngularTemplate.Data.Common.Repositories;
    using AspNetCoreWithAngularTemplate.Data.Models;
    using AspNetCoreWithAngularTemplate.Web.Infrastructure.Mapping;
    using AspNetCoreWithAngularTemplate.Web.ViewModels.Settings;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TodoItemsController : BaseController
    {
        private readonly IDeletableEntityRepository<TodoItem> repository;

        public TodoItemsController(IDeletableEntityRepository<TodoItem> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        // [AllowAnonymous]
        public IActionResult All()
        {
            var todoItems = this.repository.All().To<TodoItemViewModel>().ToList();
            return this.Ok(todoItems);
        }

        [HttpPost]
        public Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }
    }
}
