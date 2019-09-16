namespace Fundamentals
{
    public class NormalEnemy : Enemy
    {
        public string Name { get; set; }

        public override double TotalSpecialPower => throw new System.NotImplementedException();

        public override double SpecialPowerUses => throw new System.NotImplementedException();
    }
}