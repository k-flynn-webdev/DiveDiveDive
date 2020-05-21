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
    [SerializeField]
    private IReset[] ItemResets;
    [SerializeField]
    private IActive[] IActive;
    private INotActive[] INotActive;

    private void Awake()
    {
        Item = GetComponent<Item>();
        ItemResets = GetComponentsInChildren<IReset>();
        IActive = GetComponentsInChildren<IActive>();
        INotActive = GetComponentsInChildren<INotActive>();

        _isActive = false;
        this.gameObject.SetActive(false);
    }

    public void SetItemActive()
    {
        _isActive = true;
        this.gameObject.SetActive(true);

        for (int i = 0, max = IActive.Length; i < max; i++)
        {
            IActive[i].Active();
        }
    }

    public void SetItemNotActive()
    {
        _isActive = false;
        this.Reset();
        this.gameObject.SetActive(false);

        #if UNITY_EDITOR
            ServiceLocator.Resolve<ObjectPoolManager>().CheckCount();
        #endif

        for (int i = 0, max = INotActive.Length; i < max; i++)
        {
            INotActive[i].NotActive();
        }
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
