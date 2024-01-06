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
[ApiController]
public class MediaNewsController : ControllerBase
{
    IMediaNewService _mediaNewService;

    public MediaNewsController(IMediaNewService mediaNewService)
    {
        _mediaNewService = mediaNewService;
    }

    [Cache(60)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var result = await _mediaNewService.GetListAsync(pageRequest);
        return Ok(result);
    }

    [Cache]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _mediaNewService.GetByIdAsync(id);
        return Ok(result);
    }


    [CacheRemove("MediaNews.Get")]
    [CustomValidation(typeof(CreateMediaNewRequestValidator))]
    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateMediaNewRequest createMediaNewRequest)
    {
        var result = await _mediaNewService.AddAsync(createMediaNewRequest);
        return Ok(result);
    }


    [CacheRemove("MediaNews.Get")]
    [CustomValidation(typeof(UpdateMediaNewRequestValidator))]
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateMediaNewRequest updateMediaNewRequest)
    {
        var result = await _mediaNewService.UpdateAsync(updateMediaNewRequest);
        return Ok(result);
    }

    [CacheRemove("MediaNews.Get")]
    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteMediaNewRequest deleteMediaNewRequest)
    {
        var result = await _mediaNewService.DeleteAsync(deleteMediaNewRequest);
        return Ok(result);
    }
}
