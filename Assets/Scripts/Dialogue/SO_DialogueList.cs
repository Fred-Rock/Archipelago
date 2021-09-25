using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue List", menuName = "Scriptable Objects/Dialogue/Dialogue List")]
public class SO_DialogueList : ScriptableObject
{
    [SerializeField] public List<DialogueDetails> dialogueArrayList;
}
