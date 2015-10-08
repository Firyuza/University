namespace Shared.Models.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IGroupService
    {
        IList<Group> GetAll();

        Group Get(long id);

        void Add(Group group);

        void Remove(long id);
    }
}
