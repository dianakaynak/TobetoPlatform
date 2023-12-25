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
using DataAccess.Concretes;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class SkillManager : ISkillService
    {
        ISkillDal _skillDal;
        IMapper _mapper;
        SkillBusinessRules _skillBusinessRules;

        public SkillManager(ISkillDal skillDal, IMapper mapper, SkillBusinessRules skillBusinessRules)
        {
            _skillDal = skillDal;
            _mapper = mapper;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<CreatedSkillResponse> AddAsync(CreateSkillRequest createSkillRequest)
        {

            Skill skill = _mapper.Map<Skill>(createSkillRequest);
            Skill addedSkill = await _skillDal.AddAsync(skill);
            CreatedSkillResponse createdSkillResponse = _mapper.Map<CreatedSkillResponse>(addedSkill);
            return createdSkillResponse;
        }

        public async Task<DeletedSkillResponse> DeleteAsync(DeleteSkillRequest deleteSkillRequest)
        {
            await _skillBusinessRules.IsExistsSkill(deleteSkillRequest.Id);
            Skill skill = await _skillDal.GetAsync(predicate:s=>s.Id == deleteSkillRequest.Id);
            Skill deletedSkill = await _skillDal.DeleteAsync(skill,false);
            DeletedSkillResponse deletedSkillResponse = _mapper.Map<DeletedSkillResponse>(deletedSkill);
            return deletedSkillResponse;
        }


        public async Task<UpdatedSkillResponse> UpdateAsync(UpdateSkillRequest updateSkillRequest)
        {
            await _skillBusinessRules.IsExistsSkill(updateSkillRequest.Id);
            Skill skill = _mapper.Map<Skill>(updateSkillRequest);
            Skill updatedSkill = await _skillDal.UpdateAsync(skill);
            UpdatedSkillResponse updatedSkillResponse = _mapper.Map<UpdatedSkillResponse>(updatedSkill);
            return updatedSkillResponse;
        }

        public async Task<IPaginate<GetListSkillResponse>> GetListAsync()
        {
            var skills = await _skillDal.GetListAsync();
            var mappedSkills = _mapper.Map<Paginate<GetListSkillResponse>>(skills);
            return mappedSkills;
        }

        public async Task<IPaginate<GetListSkillResponse>> GetByAccountIdAsync(Guid accountId)
        {
            var skills = await _skillDal.GetListAsync(
                include: s => s.Include(a => a.AccountSkills).ThenInclude(ask => ask.Account));
            var filteredSkills = skills.Items.Where(e => e.AccountSkills.Any(s => s.AccountId == accountId)).ToList();
            var mappedSkills = _mapper.Map<Paginate<GetListSkillResponse>>(skills);
            return mappedSkills;
        }

        public async Task<GetListSkillResponse> GetByIdAsync(Guid id)
        {
            var skill = await _skillDal.GetAsync(s => s.Id == id);
            var mappedSkill = _mapper.Map<GetListSkillResponse>(skill);
            return mappedSkill;
        }
    }
}
