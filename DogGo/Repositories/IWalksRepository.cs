using DogGo.Models;

namespace DogGo.Repositories
{
    public interface IWalksRepository
    {
        List<Walks> GetAllWalks();

        Walks GetWalksById(int id);
        List<Walks> GetWalksByWalker(int walkerId);

        void AddWalk(Walks walk);

        void DeleteWalk(int walkId);
    }
}
