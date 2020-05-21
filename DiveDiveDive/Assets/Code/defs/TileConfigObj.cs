using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileConfigObj
{
    public float _tileSize;
    public float _renderSize;
    public Vector2 _offsetPre;
    public Vector3 _offsetPost;
    public ItemConfig _itemConfig;
    public Texture2D _renderTexture;

    public int[] _renderOffsetInt = new int[3];
    public Vector3 _renderOffsetFloat;

    public int _renderSizeInt;
    public int _renderSizeHalfInt;
    public float _renderScreenDist;

    public TileConfigObj(
    float tileSize,
    float renderSize,
    Vector3 offsetPost,
    ItemConfig config,
    Texture2D texture)
    {
        this._tileSize = tileSize;
        this._renderSize = renderSize;
        this._offsetPre = Vector2.zero;
        this._offsetPost = offsetPost;
        this._itemConfig = config;
        this._renderTexture = texture;

        this._renderSizeInt = Mathf.RoundToInt(this._renderSize / this._tileSize);
        this._renderSizeHalfInt = Mathf.RoundToInt(this._renderSizeInt / 2);
        this._renderScreenDist = this._renderSize * 1.2f;

        this._renderOffsetInt[0] = 0;
        this._renderOffsetInt[1] = 0;
        this._renderOffsetInt[2] = 0;

        this._renderOffsetFloat = Vector3.zero;
    }

    public TileConfigObj(
    float tileSize,
    float renderSize,
    Vector3 offsetPost,
    ItemConfig config,
    Texture2D texture,
    Vector2 offsetPre)
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

        this._renderOffsetInt[0] = (int)(offsetPre.x * (float)texture.width);
        this._renderOffsetInt[1] = (int)(offsetPre.y * (float)texture.height);
        this._renderOffsetInt[2] = 0;

        this._renderOffsetFloat = new Vector3(
            (int)(offsetPre.x * (float)texture.width) * tileSize,
            (int)(offsetPre.y * (float)texture.height) * tileSize,
            0f * tileSize);
    }
}
