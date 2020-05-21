using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileConfigObj
{
    public float _tileSize;
    public float _renderSize;
    public Vector3 _offsetPre;
    public Vector3 _offsetPost;
    public ItemConfig _itemConfig;
    public Texture2D _renderTexture;
    public Vector2 _renderTexturePivot;

    public int[] _renderTexturePivotInt = new int[3];
    public Vector3 _renderTexturePivotOffset;

    public int _renderSizeInt;
    public int _renderSizeHalfInt;
    public float _renderScreenDist;

    public TileConfigObj(
    float tileSize,
    float renderSize,
    ItemConfig config,
    Texture2D texture)
    {
        this._tileSize = tileSize;
        this._renderSize = renderSize;
        this._offsetPre = Vector3.zero;
        this._offsetPost = Vector3.zero;
        this._itemConfig = config;
        this._renderTexture = texture;

        this._renderSizeInt = Mathf.RoundToInt(this._renderSize / this._tileSize);
        this._renderSizeHalfInt = Mathf.RoundToInt(this._renderSizeInt / 2);
        this._renderScreenDist = this._renderSize * 1.2f;

        this._renderTexturePivotInt[0] = 0;
        this._renderTexturePivotInt[1] = 0;
        this._renderTexturePivotInt[2] = 0;

        this._renderTexturePivotOffset = Vector3.zero;
    }

    public TileConfigObj(
    float tileSize,
    float renderSize,
    Vector3 offsetPre,
    Vector3 offsetPost,
    ItemConfig config,
    Texture2D texture,
    Vector2 texturePivot)
    {
        this._tileSize = tileSize;
        this._renderSize = renderSize;
        this._offsetPre = offsetPre;
        this._offsetPost = offsetPost;
        this._itemConfig = config;
        this._renderTexture = texture;

        this._renderSizeInt = Mathf.RoundToInt(this._renderSize / this._tileSize);
        this._renderSizeHalfInt = Mathf.RoundToInt(this._renderSizeInt / 2);
        this._renderScreenDist = this._renderSize * 1.2f;

        this._renderTexturePivotInt[0] = (int)(texturePivot.x * (float)texture.width);
        this._renderTexturePivotInt[1] = (int)(texturePivot.y * (float)texture.height);
        this._renderTexturePivotInt[2] = 0;

        this._renderTexturePivotOffset = new Vector3(
            this._renderTexturePivotInt[0] * tileSize,
            this._renderTexturePivotInt[1] * tileSize,
            this._renderTexturePivotInt[2] * tileSize);
    }
}
