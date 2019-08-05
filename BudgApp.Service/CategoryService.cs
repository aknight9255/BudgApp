using BudgApp.Data;
using BudgApp.Data.Models;
using BudgApp.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgApp.Service
{
    public class CategoryService
    {
        private readonly Guid _userID;

        public CategoryService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    AccountID = _userID,
                    CategoryType = model.CategoryType,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.
                    Categories
                    .Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories() // Allows us to see the specific category that belong to a specific user
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Categories
                    .Where(entity => entity.AccountID == _userID)
                    .Select(
                        entity =>
                        new CategoryListItem
                        {
                            CategoryID = entity.CategoryID,
                            CategoryType = entity.CategoryType,
                        });
                return query.ToList();
            }
        }

        public CategoryDetails GetCategoryByID(int id)// Needs to be checked
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.
                    Categories
                    .Single(e => e.CategoryID == id && e.AccountID == _userID);
                return
                    new CategoryDetails
                    {
                        CategoryID = entity.CategoryID,
                        CategoryType = entity.CategoryType
                    };

            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.CategoryID == model.CategoryID && e.AccountID == _userID);

                entity.CategoryID = model.CategoryID;
                entity.CategoryType = model.CategoryType;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteCategory(int categoryID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == categoryID && e.AccountID == _userID);

                ctx.
                    Categories
                    .Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
