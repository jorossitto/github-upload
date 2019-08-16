using System;
using System.Collections.Generic;

namespace Fundamentals
{
    public enum Player { Noone = 0, Noughts, Crosses }
    public struct Square
    {
        public Player Owner { get; }
        public Square(Player owner)
        {
            Owner = owner;
        }
        public override string ToString()
        {
            switch (Owner)
            {
                case Player.Noone:
                    return ".";
                case Player.Crosses:
                    return "X";
                case Player.Noughts:
                    return "O";
                default:
                    throw new Exception("Invalid state");
            }
        }
    }
}