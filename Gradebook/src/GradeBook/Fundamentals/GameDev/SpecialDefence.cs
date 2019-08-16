using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    public abstract class SpecialDefence
    {
        public abstract int CalculateDamageReduction(int totalDamage);

        public static SpecialDefence Null { get; } = new NullDefence();
        public static SpecialDefence DiamondSkin { get; } = new DiamondSkinDefence();
        public static SpecialDefence IronBones { get; } = new IronBonesDefence();


        private class NullDefence : SpecialDefence
        {
            public override int CalculateDamageReduction(int totalDamage)
            {
                return 0; //Do nothing
            }
        }

        private class DiamondSkinDefence : SpecialDefence
        {
            public override int CalculateDamageReduction(int totalDamage)
            {
                return 1;
            }
        }

        private class IronBonesDefence : SpecialDefence
        {
            public override int CalculateDamageReduction(int totalDamage)
            {
                return 5;
            }
        }
    }
}