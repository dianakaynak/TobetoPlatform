﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.CreateRequests;
using Business.Dtos.Requests.DeleteRequests;
using Business.Dtos.Requests.UpdateRequests;
using Business.Dtos.Responses.CreatedResponses;
using Business.Dtos.Responses.DeletedResponses;
using Business.Dtos.Responses.GetListResponses;
using Business.Dtos.Responses.UpdatedResponses;
using Business.Rules;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes;

public class ProductionCompanyManager : IProductionCompanyService
{
    IProductionCompanyDal _productionCompanyDal;
    ILessonDal _lessonDal;
    IMapper _mapper;
    ProductionCompanyBusinessRules _productionCompanyBusinessRules;

    public ProductionCompanyManager(IProductionCompanyDal productionCompanyDal, IMapper mapper, ProductionCompanyBusinessRules productionCompanyBusinessRules, ILessonDal lessonDal)
    {
        _productionCompanyDal = productionCompanyDal;
        _mapper = mapper;
        _productionCompanyBusinessRules = productionCompanyBusinessRules;
        _lessonDal = lessonDal;
    }

    public async Task<CreatedProductionCompanyResponse> AddAsync(CreateProductionCompanyRequest createProductionCompanyRequest)
    {
        ProductionCompany productionCompany = _mapper.Map<ProductionCompany>(createProductionCompanyRequest);
        ProductionCompany addedProductionCompany = await _productionCompanyDal.AddAsync(productionCompany);
        CreatedProductionCompanyResponse createdProductionCompanyResponse = _mapper.Map<CreatedProductionCompanyResponse>(addedProductionCompany);
        return createdProductionCompanyResponse;
    }
    public async Task<DeletedProductionCompanyResponse> DeleteAsync(DeleteProductionCompanyRequest deleteProductionCompanyRequest)
    {
        await _productionCompanyBusinessRules.IsExistsProductionCompany(deleteProductionCompanyRequest.Id);
        ProductionCompany productionCompany = _mapper.Map<ProductionCompany>(deleteProductionCompanyRequest);
        ProductionCompany deletedProductionCompany = await _productionCompanyDal.DeleteAsync(productionCompany);
        DeletedProductionCompanyResponse deletedProductionCompanyResponse = _mapper.Map<DeletedProductionCompanyResponse>(deletedProductionCompany);
        return deletedProductionCompanyResponse;
    }

    public async Task<IPaginate<GetListProductionCompanyResponse>> GetListAsync()
    {
        var ProductionCompanies = await _productionCompanyDal.GetListAsync();
        var mappedProductionCompanies = _mapper.Map<Paginate<GetListProductionCompanyResponse>>(ProductionCompanies);
        return mappedProductionCompanies;
    }
    public async Task<GetListProductionCompanyResponse> GetByIdAsync(Guid id)
    {
        var ProductionCompanies = await _productionCompanyDal.GetAsync(p => p.Id == id);
        var mappedProductionCompanies = _mapper.Map<GetListProductionCompanyResponse>(ProductionCompanies);
        return mappedProductionCompanies;
    }

    public async Task<UpdatedProductionCompanyResponse> UpdateAsync(UpdateProductionCompanyRequest updateProductionCompanyRequest)
    {
        await _productionCompanyBusinessRules.IsExistsProductionCompany(updateProductionCompanyRequest.Id);
        ProductionCompany productionCompany = _mapper.Map<ProductionCompany>(updateProductionCompanyRequest);
        ProductionCompany updatedProductionCompany = await _productionCompanyDal.UpdateAsync(productionCompany);
        UpdatedProductionCompanyResponse updatedProductionCompanyResponse = _mapper.Map<UpdatedProductionCompanyResponse>(updatedProductionCompany);
        return updatedProductionCompanyResponse;
    }
}
