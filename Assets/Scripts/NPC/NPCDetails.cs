using UnityEngine;

[System.Serializable]
public class NPCDetails
{
    public int npcCode;
    public string npcName;
    public Sprite npcSprite;
    // public Animation npcIdleAnimation;
    public bool isVendor;
    public InventoryLocation vendorInventoryLocation;
    public int currencyModifier = 1;
    public int itemQuantityModifier = 1;
}