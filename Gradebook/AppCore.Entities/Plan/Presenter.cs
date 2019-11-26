using System;
using System.Collections.Generic;

namespace AppCore.Entities
{
    public class Presenter
    {

        public int PresenterId { get; private set; }

        public string Bio { get; set; }
        public string Name { get; set; }

        #region Talks
        private List<Talk> _talks = new List<Talk>();
        public IEnumerable<Talk> Talks => _talks;
        public Talk NewTalk()
        {
            var talk = new Talk();
            _talks.Add(talk);
            return talk;
        }
        #endregion

    }
}