using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolInfoObj
{
    public ObjectPoolItem prefab;
    public List<ObjectPoolItem> items = new List<ObjectPoolItem>();

    public ObjectPoolInfoObj(ObjectPoolItem item)
    {
        this.prefab = item;
    }

    public ObjectPoolItem GetItem(bool activate)
    {
        return GetItem(activate, Vector3.zero);
    }

    public ObjectPoolItem GetItem(bool activate, Vector3 position)
    {
        for (int i = 0, max = items.Count; i < max; i++)
        {
            if (!items[i].Active)
            {
                if (activate)
                {
                    items[i].transform.position = position;
                    items[i].SetItemActive();
                    #if UNITY_EDITOR
                        ServiceLocator.Resolve<ObjectPoolManager>().CheckCount();
                    #endif
                }
                return items[i];
            }
        }

        ObjectPoolItem tmpItem = this.prefab.CreateItem(this.prefab.name, activate, position);
        this.items.Add(tmpItem);
        #if UNITY_EDITOR
            ServiceLocator.Resolve<ObjectPoolManager>().CheckCount();
        #endif
        return tmpItem;
    }
}
