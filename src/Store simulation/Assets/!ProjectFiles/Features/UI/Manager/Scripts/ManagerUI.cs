using UnityEngine;
using UnityEngine.UI;

namespace UI.Manager
{
    public class ManagerUI : MonoBehaviour
    {
        [field: SerializeField] public Button BackButton { get; private set; }

        [field: SerializeField] public Button CategoryButton { get; private set; }
        [field: SerializeField] public Button CategoryProductButton { get; private set; }
        [field: SerializeField] public Button EmployeesButton { get; private set; }
        [field: SerializeField] public Button ProductsButton { get; private set; }
        [field: SerializeField] public Button ProductToPurchaseButton { get; private set; }
        [field: SerializeField] public Button ProductToStockButton { get; private set; }
        [field: SerializeField] public Button StoreButton { get; private set; }
        [field: SerializeField] public Button TransactionsButton { get; private set; }

        [field: SerializeField] public CategoryUI CategoryUI { get; private set; }
    }
}