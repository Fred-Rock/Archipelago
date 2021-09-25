using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : SingletonMonobehaviour<NPCManager>
{
    [SerializeField] private SO_NPCList npcList = null;

    private Dictionary<int, NPCDetails> npcDetailsDictionary = null;

    protected override void Awake()
    {
        base.Awake();

        // Create npcDetails dictionary
        CreateNPCDetailsDictionary();
    }

    /// <summary>
    /// Populates npcDetailsDictionary from scriptable object npc list
    /// </summary>
    public void CreateNPCDetailsDictionary()
    {
        npcDetailsDictionary = new Dictionary<int, NPCDetails>();

        foreach (NPCDetails npcDetails in npcList.npcDetails)
        {
            npcDetailsDictionary.Add(npcDetails.npcCode, npcDetails);
        }
    }

    /// <summary>
    /// Returns the npcDetails for the npcCode, or null if the npc code doesn't exist
    /// </summary>
    public NPCDetails GetNPCDetails(int npcCode)
    {
        NPCDetails npcDetails;

        if (npcDetailsDictionary.TryGetValue(npcCode, out npcDetails))
        {
            return npcDetails;
        }
        else
        {
            return null;
        }
    }
}
