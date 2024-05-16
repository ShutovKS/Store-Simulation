using TMPro;
using UnityEngine;

namespace UI.Manager
{
    public class CategoryRowUI : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Id { get; private set; }
        [field: SerializeField] public TMP_Text Name { get; private set; }
    }
}