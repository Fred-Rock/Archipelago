using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [NPCCodeDescription]
    [SerializeField] private int _npcCode;

    private NPCDetails npcDetails;
    public Vendor vendor;
    private SpriteRenderer spriteRenderer;
    
    public int NPCCode
    {
        get { return _npcCode; }
        set { _npcCode = value; }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (NPCCode != 0)
        {
            Init(NPCCode);
        }
    }

    private void Init(int npcCodeParem)
    {
        if (npcCodeParem != 0)
        {
            NPCCode = npcCodeParem;

            npcDetails = NPCManager.Instance.GetNPCDetails(NPCCode);

            spriteRenderer.sprite = npcDetails.npcSprite;

            if (npcDetails.isVendor)
            {
                vendor = new Vendor(npcDetails.vendorInventoryLocation, npcDetails.currencyModifier, npcDetails.itemQuantityModifier);
                vendor.SetNPCVendor(npcDetails);
                vendor.PopulateVendorInventory();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventHandler.CallNearNPCEvent(this);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        EventHandler.CallExitNPCEvent();
    }
}
