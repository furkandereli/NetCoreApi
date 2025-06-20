﻿using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using App.Persistance;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
{
    public IQueryable<Category> GetCategoryWithProducts()
    {
        return context.Categories.Include(x => x.Products).AsQueryable();
    }

    public Task<Category?> GetCategoryWithProductsAsync(int id)
    {
        return context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Category>> GetCategoryWithProductsAsync()
    {
        return Context.Categories.Include(x => x.Products).ToListAsync();
    }
}
