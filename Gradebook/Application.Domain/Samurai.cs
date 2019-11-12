﻿using System.Collections.Generic;

namespace AppFramework.Domain
{
    public class Samurai
    {
        #region constructor
        public Samurai()
        {
            SecretIdentity = new SecretIdentity();
            Quotes = new List<Quote>();
            SamuraiBattles = new List<SamuraiBattle>();
            BetterName = new PersonFullName("", "");
        }
        #endregion

        #region Variables
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonFullNameId { get; set; }
        public PersonFullName BetterName { get; set; }
        public List<Quote> Quotes { get; set; }
        //public int BattleId { get; set; }
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
        public List<Battle> Battles()
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
