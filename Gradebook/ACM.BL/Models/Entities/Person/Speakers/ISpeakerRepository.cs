using System;

namespace ACM.BL
{
    public interface ISpeakerRepository
    {
        Speaker Get(string name);

        Speaker Get(Guid id);
    }
}