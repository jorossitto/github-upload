using System.Collections.Generic;

namespace AppCore.Domain
{
    public class Samurai
    {
        #region constructor
        public Samurai()
        {
            SecretIdentity = new SecretIdentity();
            Quotes = new List<Quote>();
            SamuraiBattles = new List<SamuraiBattle>();
            //BetterName = new PersonFullName("", "");
        }
        #endregion

        #region Variables
        public int Id { get; set; }
        public string Name { get; set; }
        //public int PersonFullNameId { get; set; } adding this makes it require a PersonFullName for each object
        public virtual PersonFullName BetterName { get; set; }
        public virtual List<Quote> Quotes { get; set; }
        //public int BattleId { get; set; }
        public virtual List<SamuraiBattle> SamuraiBattles { get; set; }
        public virtual SecretIdentity SecretIdentity { get; set; }
        public virtual List<Battle> Battles()
        {
            var battles = new List<Battle>();
            foreach (var join in SamuraiBattles)
            {
                battles.Add(join.Battle);
            }
            return battles;
        }
        #endregion
    }
}
