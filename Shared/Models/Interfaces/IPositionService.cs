namespace Shared.Models.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IPositionService
    {
        IList<Position> GetAll();

        Position Get(long id);

        void Add(Position position);

        void Remove(long id);

        void Edit(Position position);
    }
}
