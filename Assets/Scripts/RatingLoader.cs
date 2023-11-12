using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class RatingLoader
{
    private static int _record = 0;
    public static bool IsNewRecord(int newSize) {
        Debug.Log(newSize + " / " + _record);
        if (newSize > _record) {
            _record = newSize;
            return true;
        }

        return false;
    
    }
}
