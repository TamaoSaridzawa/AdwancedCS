using System;
using System.Collections.Generic;

namespace CleanCode_Task6
{
    class Player { }

    class Gun { }

    class Stalker { }

    class Unit { }

    class Army
    {
        public IReadOnlyCollection<Unit> UnitsToGet { get; private set; }
    }
}
