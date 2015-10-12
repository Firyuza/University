using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models.Entities;

namespace Shared.Models.Interfaces
{
    public interface IStudentService
    {
        IList<Student> GetAll();

        Student Get(long id);

        void Add(Student student);

        void Remove(long id);

        void Edit(Student student);

        IQueryable<Student> GetByGroup(long id);
    }
}
