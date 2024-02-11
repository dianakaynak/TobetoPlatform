﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.UserOperationClaimRequests;
using Business.Dtos.Responses.UserOperationClaimResponses;
using Business.Rules.BusinessRules;
using Core.DataAccess.Paging;
using Core.Entities;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;
        IMapper _mapper;
        UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<CreatedUserOperationClaimResponse> AddAsync(CreateUserOperationClaimRequest createUserOperationClaimRequest)
        {
            UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(createUserOperationClaimRequest);
            UserOperationClaim addedUserOperationClaim = await _userOperationClaimDal.AddAsync(userOperationClaim);
            CreatedUserOperationClaimResponse createdUserOperationClaimResponse = _mapper.Map<CreatedUserOperationClaimResponse>(addedUserOperationClaim);
            return createdUserOperationClaimResponse;
        }

        public async Task<DeletedUserOperationClaimResponse> DeleteAsync(DeleteUserOperationClaimRequest deleteUserOperationClaimRequest)
        {
            await _userOperationClaimBusinessRules.IsExistsUserOperationClaim(deleteUserOperationClaimRequest.Id);
            UserOperationClaim userOperationClaim = await _userOperationClaimDal.GetAsync(predicate: uop => uop.Id == deleteUserOperationClaimRequest.Id);
            UserOperationClaim deletedUserOperationClaim = await _userOperationClaimDal.DeleteAsync(userOperationClaim);
            DeletedUserOperationClaimResponse deletedUserOperationClaimResponse = _mapper.Map<DeletedUserOperationClaimResponse>(deletedUserOperationClaim);
            return deletedUserOperationClaimResponse;
        }

        public async Task<IPaginate<GetListUserOperationClaimResponse>> GetListAsync(PageRequest pageRequest)
        {
            var userOperationClaim = await _userOperationClaimDal.GetListAsync(
                           index: pageRequest.PageIndex,
                           size: pageRequest.PageSize
                           );
            var mappedUserOperationClaim = _mapper.Map<Paginate<GetListUserOperationClaimResponse>>(userOperationClaim);
            return mappedUserOperationClaim;
        }

        public async Task<UpdatedUserOperationClaimResponse> UpdateAsync(UpdateUserOperationClaimRequest updateUserOperationClaimRequest)
        {
            UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(updateUserOperationClaimRequest);
            UserOperationClaim updatedUserOperationClaim = await _userOperationClaimDal.UpdateAsync(userOperationClaim);
            UpdatedUserOperationClaimResponse updatedUserOperationClaimResponse = _mapper.Map<UpdatedUserOperationClaimResponse>(updatedUserOperationClaim);
            return updatedUserOperationClaimResponse;
        }
    }
} 