using UnityEngine;

namespace UI.Manager
{
    public class ContentRowUI : MonoBehaviour
    {
        [field: SerializeField] public GameObject ArgPrefab { get; private set; }
        [field: SerializeField] public Transform ArgsParent { get; private set; }
    }
}