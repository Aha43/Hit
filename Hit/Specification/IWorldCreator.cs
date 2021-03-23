﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Specification
{
    public interface IWorldCreator<World> : IHitType<World>
    {
        World Create();
    }
}
