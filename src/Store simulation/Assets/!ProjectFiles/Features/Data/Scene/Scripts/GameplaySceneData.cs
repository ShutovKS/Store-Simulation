using UnityEngine;

namespace Data.Scene
{
    [CreateAssetMenu(fileName = "GameplaySceneData", menuName = "Data/SceneData/Gameplay", order = 0)]
    public class GameplaySceneData : SceneData
    {
        [field: SerializeField] public Vector3 NpcSpawnPoint { get; private set; }
        [field: SerializeField] public Vector3[] GroceryOutletPoints { get; private set; }
        [field: SerializeField] public Vector3 CashRegisterPoint { get; private set; }
        [field: SerializeField] public Vector3 ExitPoint { get; private set; }
    }
}