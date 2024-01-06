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
public class SocialMediasController : ControllerBase
{
    ISocialMediaService _socialMediaService;

    public SocialMediasController(ISocialMediaService socialMediaService)
    {
        _socialMediaService = socialMediaService;
    }

    [Cache(60)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var result = await _socialMediaService.GetListAsync(pageRequest);
        return Ok(result);
    }

    [Cache]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] Guid id)
    {
        var result = await _socialMediaService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("GetByAccountId")]
    public async Task<IActionResult> GetByAccountIdAsync([FromQuery] Guid id)
    {
        var result = await _socialMediaService.GetByAccountIdAsync(id);
        return Ok(result);
    }

    [CacheRemove("SocialMedias.Get")]
    [CustomValidation(typeof(CreateSocialMediaRequestValidator))]
    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateSocialMediaRequest createSocialMediaRequest)
    {
        var result = await _socialMediaService.AddAsync(createSocialMediaRequest);
        return Ok(result);
    }

    [CacheRemove("SocialMedias.Get")]
    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteSocialMediaRequest deleteSocialMediaRequest)
    {
        var result = await _socialMediaService.DeleteAsync(deleteSocialMediaRequest);
        return Ok(result);
    }

    [CacheRemove("SocialMedias.Get")]
    [CustomValidation(typeof(UpdateSocialMediaRequestValidator))]
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateSocialMediaRequest updateSocialMediaRequest)
    {
        var result = await _socialMediaService.UpdateAsync(updateSocialMediaRequest);
        return Ok(result);
    }
}