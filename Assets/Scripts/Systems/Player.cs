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
    
    // Data container of player's clue collections
    public Dictionary<int, int> playerClues = new Dictionary<int, int>();

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

    public void UpdateClue(int id, int childId)
    {
        var pm = GameObject.Find("ClueManager");
        var ps = pm.GetComponent<ClueSystem>();
        if (playerClues.ContainsKey(id))
        {
            if (playerClues[id] != childId)
            {
                playerClues[id] = childId;
                ps.UpdateClue(id, childId);
            }
        }
        else
        {
            playerClues.Add(id, childId);
            ps.InsertClue(id, childId);
        }
    }
}