using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // public string Name;
    // public int Level;
    // public int Experience;
    // public float Money;

    // Enable/disable player's interactive behaviours
    private bool interactive;
    public bool Interactive
    {
        get { return interactive; }
        set { interactive = value; }
    }

    // Data container of player's event flags
    public Dictionary<string, bool> playerFlags = new Dictionary<string, bool>();

    public void SetFlag(string key, bool value)
    {
        if (playerFlags.ContainsKey(key))
        {
            playerFlags[key] = value;
        }
        else
        {
            playerFlags.Add(key, value);
        }
    }

    public bool GetFlag(string key)
    {
        if (playerFlags.ContainsKey(key))
        {
            return playerFlags[key];
        }
        else
        {
            return false;
        }
    }
}