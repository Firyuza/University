using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Shared.Models.Interfaces
{
    using Entities;

    public interface IScheduleService
    {
        IList<Schedule> GetAll();

        Schedule Get(long id);

        void Add(Schedule schedule);

        void Remove(long id);

        void Edit(Schedule schedule);

        IQueryable<Schedule> GetByGroup(long id);
    }
}
