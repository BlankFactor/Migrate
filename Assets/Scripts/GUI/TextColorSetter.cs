using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextColorSetter
{
    static string end = "</color>";

    static string Green(string _str) {
        string f = "<color=#00FF01FF>";
        string newstr = f + _str + end;
        return newstr;
    }

    static string Red(string _str)
    {
        string f = "<color=#FF0000FF>";
        string newstr = f + _str + end;
        return newstr;
    }

    static string Blue(string _str)
    {
        string f = "<color=#0000FFFF>";
        string newstr = f + _str + end;
        return newstr;
    }

    static string Cyan(string _str)
    {
        string f = "<color=#C4FFF6FF>";
        string newstr = f + _str + end;
        return newstr;
    }
}
