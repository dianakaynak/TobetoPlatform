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
public class LessonSubjectsController : ControllerBase
{
    ILessonSubjectService _lessonSubjectService;

    public LessonSubjectsController(ILessonSubjectService lessonSubjectService)
    {
        _lessonSubjectService = lessonSubjectService;
    }

    [Cache(60)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var result = await _lessonSubjectService.GetListAsync(pageRequest);
        return Ok(result);
    }

    [Cache]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _lessonSubjectService.GetByIdAsync(id);
        return Ok(result);
    }

    [CacheRemove("LessonSubjects.Get")]
    [CustomValidation(typeof(CreateLessonSubjectRequestValidator))]
    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateLessonSubjectRequest createLessonSubjectRequest)
    {
        var result = await _lessonSubjectService.AddAsync(createLessonSubjectRequest);
        return Ok(result);
    }

    [CacheRemove("LessonSubjects.Get")]
    [CustomValidation(typeof(UpdateLessonSubjectRequestValidator))]
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateLessonSubjectRequest updateLessonSubjectRequest)
    {
        var result = await _lessonSubjectService.UpdateAsync(updateLessonSubjectRequest);
        return Ok(result);
    }

    [CacheRemove("LessonSubjects.Get")]
    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteAsync(DeleteLessonSubjectRequest deleteLessonSubjectRequest)
    {
        var result = await _lessonSubjectService.DeleteAsync(deleteLessonSubjectRequest);
        return Ok(result);
    }
}
