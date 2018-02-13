using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumExtension {
    
    public static char ToChar(this keyState source)
    {
        return ((char)((int)source));
    }
}
