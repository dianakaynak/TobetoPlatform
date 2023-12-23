﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.CreateRequests;
using Business.Dtos.Requests.DeleteRequests;
using Business.Dtos.Requests.UpdateRequests;
using Business.Dtos.Responses.CreatedResponses;
using Business.Dtos.Responses.DeletedResponses;
using Business.Dtos.Responses.GetListResponses;
using Business.Dtos.Responses.UpdatedResponses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamOccupationClassesController : ControllerBase
    {
        IExamOccupationClassService _examOccupationClassService;

        public ExamOccupationClassesController(IExamOccupationClassService examOccupationClassService)
        {
            _examOccupationClassService = examOccupationClassService;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _examOccupationClassService.GetListAsync();
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateExamOccupationClassRequest createExamOccupationClassRequest)
        {
            var result = await _examOccupationClassService.AddAsync(createExamOccupationClassRequest);
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateExamOccupationClassRequest updateExamOccupationClassRequest)
        {
            var result = await _examOccupationClassService.UpdateAsync(updateExamOccupationClassRequest);
            return Ok(result);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteExamOccupationClassRequest deleteExamOccupationClassRequest)
        {
            var result = await _examOccupationClassService.DeleteAsync(deleteExamOccupationClassRequest);
            return Ok(result);
        }
    }
}