using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolItem : MonoBehaviour
{
    [SerializeField]
    public bool Active
    { get { return this._isActive; } }

    [SerializeField]
    private bool _isActive = false;

    public Item Item;
    public IReset[] ItemResets;

    private void Awake()
    {
        Item = GetComponent<Item>();
        ItemResets = GetComponentsInChildren<IReset>();
        _isActive = false;
        this.gameObject.SetActive(false);
    }

    public void SetItemActive()
    {
        _isActive = true;
        this.gameObject.SetActive(true);
    }

    public void SetItemNotActive()
    {
        _isActive = false;
        this.gameObject.SetActive(false);
        this.Reset();
        #if UNITY_EDITOR
            ServiceLocator.Resolve<ObjectPoolManager>().CheckCount();
        #endif
    }

    public void Reset()
    {
        if (ItemResets == null || ItemResets.Length < 1)
        {
            return;
        }

        for (int i = 0; i < ItemResets.Length; i++)
        {
            ItemResets[i].Reset();
        }
    }

    public ObjectPoolItem CreateItem(string name, bool activate, Vector3 position)
    {
        ObjectPoolItem tmpObj = Instantiate(this, position, Quaternion.identity);
        if (activate)
        {
            tmpObj.SetItemActive();
        }

        tmpObj.name = name;

        return tmpObj;
    }
}
