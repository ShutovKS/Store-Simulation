using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Manager
{
    [Serializable]
    public class CategoryUI
    {
        // [field: SerializeField] public Button AddButton { get; private set; }
        // [field: SerializeField] public Button EditButton { get; private set; }
        // [field: SerializeField] public Button RemoveButton { get; private set; }

        [field: SerializeField] public Transform Content { get; private set; }
        [field: SerializeField] public GameObject ItemPrefab { get; private set; }

        [SerializeField] private GameObject parent;

        private Dictionary<int, GameObject> _items = new();

        public void SetActive(bool value)
        {
            parent.SetActive(value);
        }

        public void AddItem(int id, string name)
        {
            var item = Object.Instantiate(ItemPrefab, Content);

            var row = item.GetComponent<CategoryRowUI>();
            row.Id.text = id.ToString();
            row.Name.text = name;

            _items.Add(id, item);
        }

        public void RemoveItem(int id)
        {
            if (_items.TryGetValue(id, out var item))
            {
                Object.Destroy(item);
                _items.Remove(id);
            }
        }

        public void Clear()
        {
            foreach (var item in _items.Values)
            {
                Object.Destroy(item);
            }

            _items.Clear();
        }
    }
}