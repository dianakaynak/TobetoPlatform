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
public class AccountHomeworksController : ControllerBase
{
    IAccountHomeworkService _accountHomeworkService;

    public AccountHomeworksController(IAccountHomeworkService accountHomeworkService)
    {
        _accountHomeworkService = accountHomeworkService;
    }

    [Cache(60)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var result = await _accountHomeworkService.GetListAsync(pageRequest);
        return Ok(result);
    }

    [Cache]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _accountHomeworkService.GetByIdAsync(id);
        return Ok(result);
    }

    [CacheRemove("AccountHomeworks.Get")]
    [CustomValidation(typeof(CreateAccountHomeworkRequestValidator))]
    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateAccountHomeworkRequest createAccountHomeworkRequest)
    {
        var result = await _accountHomeworkService.AddAsync(createAccountHomeworkRequest);
        return Ok(result);
    }


    [CacheRemove("AccountHomeworks.Get")]
    [CustomValidation(typeof(UpdateAccountHomeworkRequestValidator))]
    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteAccountHomeworkRequest deleteAccountHomeworkRequest)
    {
        var result = await _accountHomeworkService.DeleteAsync(deleteAccountHomeworkRequest);
        return Ok(result);
    }


    [CacheRemove("AccountHomeworks.Get")]
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateAccountHomeworkRequest updateAccountHomeworkRequest)
    {
        var result = await _accountHomeworkService.UpdateAsync(updateAccountHomeworkRequest);
        return Ok(result);
    }
}
