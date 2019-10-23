using System.Threading.Tasks;

namespace Application.Data
{
    public class CampRepository : ICampRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<Camp> GetCampAsync(string moniker, bool includeTalks = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<Speaker[]> GetSpeakersByMonikerAysnc(string moniker)
        {
            throw new System.NotImplementedException();
        }

        public Task<Talk> GetTalkAsync(int talkId, bool includeSpeakers = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}