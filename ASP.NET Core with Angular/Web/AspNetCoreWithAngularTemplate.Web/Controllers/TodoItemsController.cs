namespace AspNetCoreWithAngularTemplate.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreWithAngularTemplate.Data.Common.Repositories;
    using AspNetCoreWithAngularTemplate.Data.Models;
    using AspNetCoreWithAngularTemplate.Web.Infrastructure.Extensions;
    using AspNetCoreWithAngularTemplate.Web.Infrastructure.Mapping;
    using AspNetCoreWithAngularTemplate.Web.ViewModels.TodoItems;

    using AutoMapper;

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
        [AllowAnonymous]
        public IActionResult All()
        {
            var todoItems = this.repository.All().To<TodoItemViewModel>().ToList();
            return this.Ok(todoItems);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TodoItemBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.GetFirstError());
            }

            var todoItem = Mapper.Map<TodoItem>(model);
            this.repository.Add(todoItem);
            await this.repository.SaveChangesAsync();

            return this.Json(Mapper.Map<TodoItemViewModel>(todoItem));
        }
    }
}
