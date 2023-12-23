﻿using Business.Messages;
using Core.Business.Rules;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    public class AccountSessionBusinessRules : BaseBusinessRules
    {
        private readonly IAccountSessionDal _accountSessionDal;

        public AccountSessionBusinessRules(IAccountSessionDal accountSessionDal)
        {
            _accountSessionDal = accountSessionDal;
        }

        public async Task IsExistsAccountSession(Guid accountSessionId)
        {
            var result = await _accountSessionDal.GetListAsync(
                predicate: a => a.Id == accountSessionId
                );
            if (result.Count == 0)
            {
                throw new Exception(BusinessMessages.DataNotFound);
            }
        }
    }
}