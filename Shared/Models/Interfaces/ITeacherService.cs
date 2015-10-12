using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Shared.Models.Interfaces
{
    using Entities;
    public interface ITeacherService
    {
        IList<Teacher> GetAll();

        Teacher Get(long id);

        void Add(Teacher teacher);

        void Remove(long id);

        void Edit(Teacher teacher);
    }
}
