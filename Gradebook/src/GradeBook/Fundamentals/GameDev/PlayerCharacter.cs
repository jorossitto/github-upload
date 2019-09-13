using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    public class PlayerCharacter : INotifyPropertyChanged
    {
        private int _health = 100;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName}{LastName}";
        public string Nickname { get; set; }
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnPropertyChanged();
            }

        }


        public bool? IsNoob { get; set; }

        public List<string> Weapons { get; set; }

        public event EventHandler<EventArgs> PlayerSlept;

        public PlayerCharacter(SpecialDefence specialDefence)
        {
            FirstName = GenerateRandomFirstName();
            IsNoob = true;
            CreateStartingWeapons();
            _specialDefence = specialDefence;
        }

        public void Sleep()
        {
            int healthIncrease = CalculateHealthIncrease();
            Health += healthIncrease;
        }

        private int CalculateHealthIncrease()
        {
            return 0;
            //throw new NotImplementedException();
        }

        private void CreateStartingWeapons()
        {
            //throw new NotImplementedException();
        }

        private string GenerateRandomFirstName()
        {
            return "";
            //throw new NotImplementedException();
        }

        private readonly SpecialDefence _specialDefence;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Hit(int damage)
        {
            int totalDamageTaken = damage - _specialDefence.CalculateDamageReduction(damage);
            Health -= totalDamageTaken;
            Console.WriteLine($"{FirstName}'s health has been reduced by {totalDamageTaken} to {Health}");

        }
        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }
        public int? DaysSinceLastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}