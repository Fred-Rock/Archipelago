using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_NPCList", menuName = "Scriptable Objects/NPC/NPC List")]
public class SO_NPCList : ScriptableObject
{
    [SerializeField] public List<NPCDetails> npcDetails;
}