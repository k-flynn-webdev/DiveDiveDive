using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileConfigObj
{
    public float _tileSize;
    public float _renderSize;
    public Vector3 _offset;
    public ItemConfig _itemConfig;
    public Texture2D _renderTexture;

    public int _renderSizeInt;
    public int _renderSizeHalfInt;
    public float _renderScreenDist;

    public TileConfigObj(
        float tileSize,
        float renderSize,
        Vector3 offset,
        ItemConfig config,
        Texture2D texture)
    {
        this._tileSize = tileSize;
        this._renderSize = renderSize;
        this._offset = offset;
        this._itemConfig = config;
        this._renderTexture = texture;

        this._renderSizeInt = Mathf.RoundToInt(this._renderSize / this._tileSize);
        this._renderSizeHalfInt = Mathf.RoundToInt(this._renderSizeInt / 2);
        this._renderScreenDist = this._renderSize * 1.25f;
    }
}
