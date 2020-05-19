using System.Collections;
using System.Collections.Generic;

public struct GameEventObj
{
    public string _type;
    public object _value;

    public GameEventObj(string type, object value)
    {
        this._type = type;
        this._value = value;
    }
}
