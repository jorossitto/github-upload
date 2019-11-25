using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities
{
    public abstract class EntityBase
    {

        public EntityStateOption EntityState { get; set; }
        public bool HasChanges { get; set; }

        public bool IsNew { get; private set; }

        public bool IsValid => Validate();

        public abstract bool Validate();

    }
}
