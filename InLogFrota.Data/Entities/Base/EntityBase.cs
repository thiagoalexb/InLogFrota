using System;

namespace InLogFrota.Data.Entities.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public void SetCreationDate()
        {
            CreationDate = DateTime.Now;
        }

        public void SetChangeDate()
        {
            ChangeDate = DateTime.Now;
        }
    }
}
