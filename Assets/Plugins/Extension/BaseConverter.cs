using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseConverter {

    public static string HexToDec(this string srcString,char spliter = ' ') {
        string[] temp = srcString.Split(spliter);
        for(int i = 0; i < temp.Length; i++) {
            temp[i] = Convert.ToInt16(temp[i], 16).ToString();
        }

        return temp.Join(spliter.ToString());
    }

}