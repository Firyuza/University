using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Shared.Models.Interfaces
{
    using Entities;

    public interface IDepartmentService
    {
        IList<Department> GetAll();

        Department Get(long id);

        void Add(Department department);

        void Remove(long id);

        void Edit(Department department);
    }
}
