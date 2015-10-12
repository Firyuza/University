using System.Collections.Generic;
using System.Linq;
using Shared.Models.Entities;

namespace Shared.Models.Interfaces
{
    public interface IAcademicProgressService
    {
        IList<AcademicProgress> GetAll();

        AcademicProgress Get(long id);

        void Add(AcademicProgress academicProgress);

        void Remove(long id);

        void Edit(AcademicProgress academicProgress);

        IQueryable<AcademicProgress> GetByGroupCourse(long gId, long cId);
    }
}
