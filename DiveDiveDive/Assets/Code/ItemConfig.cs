using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemConfig : ScriptableObject
{
    [SerializeField]
    public List<ItemColorObj> Items = new List<ItemColorObj>();


    public string GetItemFromColour(Color col)
    {

        for (int i = 0, max = Items.Count; i < max; i++)
        {
            if (Items[i]._color.r == col.r &&
                Items[i]._color.g == col.g &&
                Items[i]._color.b == col.b)
            {
                if (Items[i]._item == null)
                {
                    throw new System.Exception("No item attached to color - item config");
                }

                return Items[i]._item.name;
            }
        }

        return "";
    }
}
