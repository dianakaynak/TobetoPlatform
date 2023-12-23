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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class EducationProgramLevelManager : IEducationProgramLevelService
    {
        IEducationProgramLevelDal _educationProgramLevelDal;
        IMapper _mapper;
        EducationProgramLevelBusinessRules _educationProgramLevelBusinessRules;

        public EducationProgramLevelManager(IEducationProgramLevelDal educationProgramLevelDal, IMapper mapper, EducationProgramLevelBusinessRules educationProgramLevelBusinessRules)
        {
            _educationProgramLevelDal = educationProgramLevelDal;
            _mapper = mapper;
            _educationProgramLevelBusinessRules = educationProgramLevelBusinessRules;
        }

        public async Task<CreatedEducationProgramLevelResponse> AddAsync(CreateEducationProgramLevelRequest createEducationProgramLevelRequest)
        {
            EducationProgramLevel educationProgramLevel = _mapper.Map<EducationProgramLevel>(createEducationProgramLevelRequest);
            EducationProgramLevel addedEducationProgramLevel = await _educationProgramLevelDal.AddAsync(educationProgramLevel);
            CreatedEducationProgramLevelResponse mapperEducationProgram = _mapper.Map<CreatedEducationProgramLevelResponse>(addedEducationProgramLevel);
            return mapperEducationProgram;
        }

        public async Task<DeletedEducationProgramLevelResponse> DeleteAsync(DeleteEducationProgramLevelRequest deleteEducationProgramLevelRequest)
        {
            await _educationProgramLevelBusinessRules.IsExistsEducationProgramLevel(deleteEducationProgramLevelRequest.Id);
            EducationProgramLevel educationProgramLevel = _mapper.Map<EducationProgramLevel>(deleteEducationProgramLevelRequest);
            EducationProgramLevel deletedEducationProgramLevel = await _educationProgramLevelDal.DeleteAsync(educationProgramLevel);
            DeletedEducationProgramLevelResponse mapperEducationProgram = _mapper.Map<DeletedEducationProgramLevelResponse>(deletedEducationProgramLevel);
            return mapperEducationProgram;
        }

        public async Task<IPaginate<GetListEducationProgramLevelResponse>> GetListAsync()
        {
            var educationProgramLevelResponse = await _educationProgramLevelDal.GetListAsync();
            var mappedEducationProgramLevelList = _mapper.Map<Paginate<GetListEducationProgramLevelResponse>>(educationProgramLevelResponse);
            return mappedEducationProgramLevelList;
        }

        public async Task<UpdatedEducationProgramLevelResponse> UpdateAsync(UpdateEducationProgramLevelRequest updateEducationProgramLevelRequest)
        {
            await _educationProgramLevelBusinessRules.IsExistsEducationProgramLevel(updateEducationProgramLevelRequest.Id);
            EducationProgramLevel educationProgramLevel = _mapper.Map<EducationProgramLevel>(updateEducationProgramLevelRequest);
            EducationProgramLevel updatedEducationProgramLevel = await _educationProgramLevelDal.UpdateAsync(educationProgramLevel);
            UpdatedEducationProgramLevelResponse mapperEducationProgram = _mapper.Map<UpdatedEducationProgramLevelResponse>(updatedEducationProgramLevel);
            return mapperEducationProgram;
        }
    }
}