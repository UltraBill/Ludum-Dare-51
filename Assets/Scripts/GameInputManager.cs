using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public static class GameInputManager
{
    static Dictionary<string, KeyCode> keyMapping;
    static string[] keyMaps = new string[4]
    {
        "Hit",
        "Left",
        "Right",
        "Jump"
    };
    static KeyCode[] defaults = new KeyCode[4]
    {
        KeyCode.Mouse0,
        KeyCode.A,
        KeyCode.D,
        KeyCode.Space,
    };
 
    static GameInputManager()
    {
        InitializeDictionary();
    }
 
    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<string, KeyCode>();
        for(int i=0;i<keyMaps.Length;++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
        }
    }
 
    public static void SetKeyMap(string keyMap,KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;
    }
 
    public static bool GetKeyDown(string keyMap)
    {
        Debug.Log(keyMapping[keyMap]);
        Debug.Log(Input.GetKeyDown(keyMapping[keyMap]));
        return Input.GetKeyDown(keyMapping[keyMap]);
    }
}
