﻿using Core.Entities;

namespace Entities.Concretes;

public class Session : Entity<Guid>
{
    public Guid OccupationClassId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string RecordPath { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }
}