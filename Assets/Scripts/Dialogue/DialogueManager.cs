using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : SingletonMonobehaviour<DialogueManager>
{
    [SerializeField] private SO_DialogueList genericDialogueList = null;

    private void Start()
    {
        GetRandomDialogueQueue();
    }

    public Queue<string> GetRandomDialogueQueue()
    {
        int index = Random.Range(0, genericDialogueList.dialogueArrayList.Count);

        Queue<string> dialogueQueue = new Queue<string>();

    DialogueDetails randomDialogue = genericDialogueList.dialogueArrayList[index];

        foreach (string sentence in randomDialogue.dialogueArray)
        {
            dialogueQueue.Enqueue(sentence);
        }

        return dialogueQueue;
    }

    public Queue<string> GetNPDialogueQueue()
    {
        Queue<string> dialogueQueue = new Queue<string>();

        return dialogueQueue;
    }
}
