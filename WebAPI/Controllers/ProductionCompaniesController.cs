﻿using Business.Abstracts;
using Business.Dtos.Requests.CreateRequests;
using Business.Dtos.Requests.DeleteRequests;
using Business.Dtos.Requests.UpdateRequests;
using Business.Rules.ValidationRules.FluentValidation.CreateRequestValidators;
using Business.Rules.ValidationRules.FluentValidation.UpdateRequestValidators;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class ProductionCompaniesController : ControllerBase
{
    IProductionCompanyService _productionCompanyService;
    public ProductionCompaniesController(IProductionCompanyService productionCompanyService)
    {
        _productionCompanyService = productionCompanyService;
    }

    [Cache(60)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var result = await _productionCompanyService.GetListAsync(pageRequest);
        return Ok(result);
    }

    [Cache]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _productionCompanyService.GetByIdAsync(id);
        return Ok(result);
    }

    [CacheRemove("ProductionCompanies.Get")]
    [CustomValidation(typeof(CreateProductionCompanyRequestValidator))]
    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateProductionCompanyRequest createProductionCompanyRequest)
    {
        var result = await _productionCompanyService.AddAsync(createProductionCompanyRequest);
        return Ok(result);
    }

    [CacheRemove("ProductionCompanies.Get")]
    [CustomValidation(typeof(UpdateProductionCompanyRequestValidator))]
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductionCompanyRequest updateProductionCompanyRequest)
    {
        var result = await _productionCompanyService.UpdateAsync(updateProductionCompanyRequest);
        return Ok(result);
    }

    [CacheRemove("ProductionCompanies.Get")]
    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteProductionCompanyRequest deleteProductionCompanyRequest)
    {
        var result = await _productionCompanyService.DeleteAsync(deleteProductionCompanyRequest);
        return Ok(result);
    }
}

