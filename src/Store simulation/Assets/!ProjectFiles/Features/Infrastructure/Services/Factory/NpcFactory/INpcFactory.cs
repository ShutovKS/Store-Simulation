using Data.Scene;
using NonPlayerCharacter;

namespace Infrastructure.Services.Factory.NpcFactory
{
    public interface INpcFactory
    {
        void Spawn(GameplaySceneData gameplaySceneData);
        void DestroyNpc(NpcController npcController);
    }
}