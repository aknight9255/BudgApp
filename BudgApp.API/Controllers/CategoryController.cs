using BudgApp.Service;
using BudgApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute; // Not sure about this reference... 

namespace BudgApp.API.Controllers
{
    [Authorize]

    public class CategoryController : ApiController
    {
        public IHttpActionResult GetAll() // Get all categories
        {
            CategoryService categoryService = CreateCategoryService();
            var category = categoryService.GetCategories();
            return Ok(category);
        }

        public IHttpActionResult Get(int id) // Get specific category by ID
        {
            CategoryService categoryService = CreateCategoryService();
            var category = categoryService.GetCategoryByID(id);
            return Ok(category);
        }

        public IHttpActionResult Post(CategoryCreate category) // Creates a new category
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(CategoryEdit category) // Updates an existing category
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.UpdateCategory(category))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id) // Deletes a selected category
        {
            var service = CreateCategoryService();

            if (!service.DeleteCategory(id))
                return InternalServerError();

            return Ok();
        }

        private CategoryService CreateCategoryService() // Obtains the user id for getting only thier categories
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var categoryService = new CategoryService(userID);
            return categoryService;
        }
    }
}