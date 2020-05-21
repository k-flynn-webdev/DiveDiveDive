using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileRender : MonoBehaviour
{

    [SerializeField]
    private TileConfigObj _config;

    private float _distanceBias = 1f;

    [SerializeField]
    private List<ObjectPoolItem> _items = new List<ObjectPoolItem>();


    void Start()
    {
        ServiceLocator.Register<TileRender>(this);
    }

    public void SetupRender(TileConfigObj setup)
    {
        _config = setup;
        ImageRead.SetImage(_config._renderTexture);
    }

    public int PositionToHash(Vector3 position)
    {
        int[] posArray = PositionToPixel(position);
        return posArray[0] + posArray[1] + posArray[2];
    }

    /// <returns> Hash position of vector3 rendered </returns>
    public int Render(Vector3 position)
    {
        RenderClearItems(position);
        return RenderWorld(position);
    }

    public void RenderClear()
    {
        for (int i = 0, max = _items.Count; i < max; i++)
        {
            _items[i].SetItemNotActive();
        }

        _items.Clear();
    }


    public ObjectPoolItem GetItem(string item, Vector3 position)
    {
        ObjectPoolItem temp =
            ServiceLocator.Resolve<ObjectPoolManager>().GetItem(item, true, position);
        _items.Add(temp);
        return temp;
    }

    private int[] PositionToPixel(Vector3 position)
    {
        Vector3 newPos = position + _config._renderOffsetFloat;

        int[] tmp = new int[3];
        tmp[0] = (int)(newPos.x / _config._tileSize);
        tmp[1] = (int)(newPos.y / _config._tileSize);
        tmp[2] = (int)(newPos.z / _config._tileSize);

        return tmp;
    }

    private Vector3 PositionFromPixel(int x, int y, int z)
    {
        return new Vector3(
            x * _config._tileSize,
            y * _config._tileSize,
            z * _config._tileSize);
    }

    private ObjectPoolItem FindItemAtPosition(Vector3 position, string type)
    {
        float _tmpDistance = 0f;

        for (int i = 0, max = _items.Count; i < max; i++)
        {
            _tmpDistance = Vector3.Distance(_items[i].transform.position, position);

            if (_tmpDistance < _distanceBias && _items[i].name.Equals(type))
            {
                return _items[i];
            }
        }

        return null;
    }

    private void RenderPixel(int x, int y)
    {
        Color tmpCol = ImageRead.GetPixelXY(x, y);
        string objectType = _config._itemConfig.GetItemFromColour(tmpCol);

        if (objectType == "")
        {
            return;
        }

        Vector3 tmpPos =
            PositionFromPixel(x, y, 0)
            - _config._renderOffsetFloat
            + _config._offsetPost;

        ObjectPoolItem _itemFound = FindItemAtPosition(tmpPos, objectType);
        if (_itemFound != null)
        {
            return;
        }

        ObjectPoolItem tmp = GetItem(objectType, tmpPos);
    }


    /// <returns> Hash position of vector3 rendered </returns>
    private int RenderWorld(Vector3 position)
    {
        int[] posArray = PositionToPixel(position);
        int posHash = PositionToHash(position);

        int startX = posArray[0] - _config._renderSizeHalfInt;
        int endX = posArray[0] + _config._renderSizeHalfInt + 1;
        int startY = posArray[1] - _config._renderSizeHalfInt;
        int endY = posArray[1] + _config._renderSizeHalfInt + 1;

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                RenderPixel(x, y);
            }
        }

        return posHash;
    }


    private void RenderClearItems(Vector3 position)
    {
        float _tmpDistance = 0f;

        for (int i = _items.Count - 1; i >= 0; i--)
        {
            _tmpDistance = Vector3.Distance(_items[i].transform.position, position);

            if (_tmpDistance > _config._renderScreenDist)
            {
                _items[i].SetItemNotActive();
                _items.RemoveAt(i);
            }
        }
    }

}
