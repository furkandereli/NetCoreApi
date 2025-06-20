﻿using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProducts(int categoryId)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);

        if (category is null)
            return ServiceResult<CategoryWithProductsDto>.Fail("Kategori bulunamadı.", HttpStatusCode.NotFound);

        var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

        return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProducts()
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync();

        var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await categoryRepository.GetAllAsync();

        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);

        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category is null)
            return ServiceResult<CategoryDto>.Fail("Kategori bulunamadı.", HttpStatusCode.NotFound);

        var categoryAsDto = mapper.Map<CategoryDto>(category);

        return ServiceResult<CategoryDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
    {
        var anyCategory = await categoryRepository.AnyAsync(x => x.Name == request.Name);

        if (anyCategory)
            return ServiceResult<int>.Fail("Kategori ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);

        var newCategory = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var isCatregoryNameExist = await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);

        if (isCatregoryNameExist)
            return ServiceResult.Fail("Kategori ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);

        var category = mapper.Map<Category>(request);
        category.Id = id;

        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        categoryRepository.Delete(category!);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
