using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Shared.Models.Interfaces
{
    using Entities;

    public interface ICourseService
    {
        IList<Course> GetAll();

        Course Get(long id);

        void Add(Course course);

        void Remove(long id);

        void Edit(Course course);
    }
}
