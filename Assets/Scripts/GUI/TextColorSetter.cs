using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextColorSetter
{
    static string end = "</color>";

    public static string Green(string _str) {
        string f = "<color=#00FF01FF>";
        string newstr = f + _str + end;
        return newstr;
    }

    public static string Red(string _str)
    {
        string f = "<color=#FF0000FF>";
        string newstr = f + _str + end;
        return newstr;
    }

    public static string Blue(string _str)
    {
        string f = "<color=#0000FFFF>";
        string newstr = f + _str + end;
        return newstr;
    }

    public static string White(string _str)
    {
        string f = "<color=#FFFFFFFF>";
        string newstr = f + _str + end;
        return newstr;
    }

    public static string Black(string _str)
    {
        string f = "<color=#000000FF>";
        string newstr = f + _str + end;
        return newstr;
    }

    public static string Cyan(string _str)
    {
        string f = "<color=#C4FFF6FF>";
        string newstr = f + _str + end;
        return newstr;
    }
}
