// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Transactions;
using Oxite.Modules.Conferences.Repositories;

namespace Oxite.Modules.Conferences.Services
{
    public class UserScheduleService : IUserScheduleService
    {
        private readonly IUserScheduleRepository repository;

        public UserScheduleService(IUserScheduleRepository repository)
        {
            this.repository = repository;
        }

        public bool IsUserSchedulePublic(Guid userID)
        {
            return repository.IsSchedulePublic(userID);
        }

        public void MakeUserSchedulePublic(Guid userID)
        {
            using (var transaction = new TransactionScope())
            {
                repository.MakeSchedulePublic(userID);

                transaction.Complete();
            }
        }

        public void MakeUserSchedulePrivate(Guid userID)
        {
            using (var transaction = new TransactionScope())
            {
                repository.MakeSchedulePrivate(userID);

                transaction.Complete();
            }
        }
    }
}