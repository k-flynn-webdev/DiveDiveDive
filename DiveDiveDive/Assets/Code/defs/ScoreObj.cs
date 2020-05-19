using System;
using System.Collections;
using System.Collections.Generic;

public struct ScoreObj
{
    public string _valueText;
    public float _valueFloat;

    public ScoreObj(float valueFloat)
    {
        this._valueText = valueFloat.ToString();
        this._valueFloat = valueFloat;
    }

    public ScoreObj(string valueText)
    {
        this._valueText = valueText;
        this._valueFloat = 0f;
    }
}
