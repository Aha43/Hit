﻿using Hit.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.TrivialSyncTestData
{
    [UseAs(test: "TrivialSyncTestA")]
    public class TestA : TestImplementationBase<TrivialSyncTestWorld>
    {
        public override void Act(TrivialSyncTestWorld world)
        {
       
        }
    }

}
