using System;
using System.Runtime.Serialization;

namespace Fundamentals
{
    [Serializable]
    public class EnemyCreationException : Exception
    {
        public string RequestedEnemyName { get; private set; }

        public EnemyCreationException(string message, string enemyName) :base(message)
        {
            RequestedEnemyName = enemyName;
        }
    }
}