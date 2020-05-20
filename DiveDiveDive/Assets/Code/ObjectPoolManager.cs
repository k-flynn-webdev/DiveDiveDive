using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager : MonoBehaviour
{

    [SerializeField]
    private readonly Dictionary<string, ObjectPoolInfoObj>
    ItemPrefabs = new Dictionary<string, ObjectPoolInfoObj>();

    [SerializeField]
    private ObjectPoolAssets ItemPrefabsAsset;

    [HideInInspector]
    public int _types = 0;
    [HideInInspector]
    public int _active = 0;
    [HideInInspector]
    public int _total = 0;

    void Awake()
    {
        ServiceLocator.Register<ObjectPoolManager>(this);
    }

    void Start()
    {
        SetupObjectPrefabs();
    }

    private void SetupObjectPrefabs()
    {
        for (int i = 0, max = ItemPrefabsAsset.Assets.Count; i < max; i++)
        {
            ItemPrefabs[ItemPrefabsAsset.Assets[i].name] = new ObjectPoolInfoObj(ItemPrefabsAsset.Assets[i]);
        }
    }

    public ObjectPoolItem GetItem(string item)
    {
        return GetItem(item, true, Vector3.zero);
    }

    public ObjectPoolItem GetItem(string item, bool activate)
    {
        return GetItem(item, activate, Vector3.zero);
    }

    public ObjectPoolItem GetItem(string item, bool activate, Vector3 position)
    {
        if (item == null || item.Length < 1)
        {
            throw new System.Exception("No Item key given");
        }

        if (!ItemPrefabs.ContainsKey(item))
        {
            throw new System.Exception(item + " key does not exist - object manager");
        }

        return ItemPrefabs[item].GetItem(activate, position);
    }

    public void CheckCount()
    {
        _types = 0;
        _active = 0;
        _total = 0;

        foreach (var item in ItemPrefabs)
        {
            _total += item.Value.items.Count;

            if (item.Value.items.Count > 0)
            {
                _types += 1;
            }
            
            for (int i = 0; i < item.Value.items.Count; i++)
            {
                if (item.Value.items[i].Active)
                {
                    _active += 1;
                }
            }
        }
    }
}
