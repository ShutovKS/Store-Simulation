using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Manager
{
    [Serializable]
    public class ContentManagerUI
    {
        [SerializeField] protected Transform content;
        [SerializeField] protected GameObject itemPrefab;
        [SerializeField] protected GameObject parent;

        protected Dictionary<int, GameObject> Items = new();

        public void SetActive(bool value)
        {
            parent.SetActive(value);
        }

        public void AddItem(object[] args)
        {
            var item = Object.Instantiate(itemPrefab, content);
            item.SetActive(true);

            var row = item.GetComponent<ContentRowUI>();
            foreach (var arg in args)
            {
                var instantiate = Object.Instantiate(row.ArgPrefab, row.ArgsParent);
                instantiate.GetComponentInChildren<TMP_Text>().text = arg.ToString();
                instantiate.SetActive(true);
            }

            if (args[0] is int id)
            {
                Items.Add(id, item);
            }
            else
            {
                Items.Add(Items.Count + 1, item);
            }
        }

        public void RemoveItem(object arg)
        {
            var id = (int)arg;
            if (Items.TryGetValue(id, out var item))
            {
                Object.Destroy(item);
                Items.Remove(id);
            }
        }

        public void Clear()
        {
            foreach (var item in Items.Values)
            {
                Object.Destroy(item);
            }

            Items.Clear();
        }
    }
}