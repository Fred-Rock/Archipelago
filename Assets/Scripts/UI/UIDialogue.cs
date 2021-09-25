using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image avatar;
    [SerializeField] private RectTransform background;

    private Queue<string> dialogueQueue = new Queue<string>();
    private int index = 0;

    private void Awake()
    {
        DeactivateDialogueBox();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ShowDialogue(NPC npc)
    {
        ActivateDialogueBox();

        SetDialogueBox(npc);
    }

    private void SetDialogueBox(NPC npc)
    {
        NPCDetails npcDetails = NPCManager.Instance.GetNPCDetails(npc.NPCCode);

        nameText.text = npcDetails.npcName.ToString();
        avatar.sprite = npcDetails.npcSprite;

        dialogueQueue.Clear();

        dialogueQueue = DialogueManager.Instance.GetRandomDialogueQueue();

        string sentence = dialogueQueue.Dequeue();
        dialogueText.text = sentence;
    }

    public void AdvanceDialogue()
    {
        if (dialogueQueue.Count != 0)
        {
            string sentence = dialogueQueue.Dequeue();
            dialogueText.text = sentence;
        }
        else
        {
            DeactivateDialogueBox();
        }
    }

    private void ActivateDialogueBox()
    {
        background.gameObject.SetActive(true);
    }

    private void DeactivateDialogueBox()
    {
        background.gameObject.SetActive(false);
    }
}
