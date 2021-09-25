using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIInteract : MonoBehaviour
{
    [SerializeField] RectTransform npcPrompt;
    [SerializeField] TextMeshProUGUI npcPromptNameText;
    [SerializeField] Button npcPromptTalkButton;
    [SerializeField] Button npcPromptTradeButton;

    private NPC npc;
    private Vendor vendor;
    private UIBarter uiBarter;
    private UIDialogue uiDialogue;

    private void OnEnable()
    {
        EventHandler.NearNPCEvent += ShowInteractPrompt;
        EventHandler.ExitNPCEvent += HideInteractPrompt;
    }

    private void OnDisable()
    {
        EventHandler.NearNPCEvent -= ShowInteractPrompt;
        EventHandler.ExitNPCEvent -= HideInteractPrompt;
    }

    private void Start()
    {
        npcPrompt.gameObject.SetActive(false);

        uiBarter = FindObjectOfType<UIBarter>();

        uiDialogue = FindObjectOfType<UIDialogue>();
    }

    public void ShowInteractPrompt(NPC npc)
    {
        this.npc = npc;

        NPCDetails npcDetails = NPCManager.Instance.GetNPCDetails(npc.NPCCode);

        npcPromptNameText.text = npcDetails.npcName;

        npcPrompt.gameObject.SetActive(true);

        bool isVendor = NPCManager.Instance.GetNPCDetails(npc.NPCCode).isVendor;

        if (isVendor)
        {
            npcPromptTradeButton.gameObject.SetActive(true);
        }
        else
        {
            npcPromptTradeButton.gameObject.SetActive(false);
        }
    }

    public void HideInteractPrompt()
    {
        npcPrompt.gameObject.SetActive(false);
    }

    public void HandleTradeButton()
    {
        vendor = npc.vendor;

        if (uiBarter.gameObject.activeInHierarchy == false)
        {
            uiBarter.gameObject.SetActive(true);
        }

        uiBarter.StartTradeWithVendor(vendor);

        HideInteractPrompt();
    }

    public void HandleTalkButton()
    {
        if (uiDialogue.gameObject.activeInHierarchy == false)
        {
            uiDialogue.gameObject.SetActive(true);
        }

        uiDialogue.ShowDialogue(npc);

        HideInteractPrompt();
    }
}
