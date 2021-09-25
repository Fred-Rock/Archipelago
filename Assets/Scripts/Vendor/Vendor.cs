using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor
{
    private InventoryLocation inventoryLocation;
    private int currency;
    private int itemQuantity;

    public InventoryLocation InventoryLocation { get; set; }
    public int Currency { get; set; }
    public int ItemQuantity { get; set; }

    public Vendor(InventoryLocation inventoryLocation, int currency, int itemQuantity)
    {
        InventoryLocation = inventoryLocation;
        Currency = currency;
        ItemQuantity = itemQuantity;
    }

    public void SetNPCVendor(NPCDetails npcDetails)
    {
        Currency = SetVendorCurrency(npcDetails.currencyModifier);
        ItemQuantity = SetVendorItemQuantity(npcDetails.itemQuantityModifier);
    }

    private int SetVendorCurrency(int currencyModifier)
    {
        int baseCurrency = Settings.vendorBaseCurrency;

        int vendorCurrency = Random.Range(baseCurrency, baseCurrency * currencyModifier);

        return vendorCurrency;
    }

    private int SetVendorItemQuantity(int itemQuantityModifier)
    {
        int baseItemQuantity = Settings.vendorBaseItemQuantity;

        int vendorItemQuantity = Random.Range(baseItemQuantity, baseItemQuantity * itemQuantityModifier);
        Debug.Log(vendorItemQuantity);

        return vendorItemQuantity;
    }

    public void PopulateVendorInventory()
    {
        InventoryManager.Instance.PopulateInventory(InventoryLocation, ItemQuantity);
    }

    public void SellItemToVendor(ItemDetails item)
    {
        if (item != null)
        {
            if (Currency >= item.basePrice)
            {
                InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.itemCode);
                InventoryManager.Instance.AddItem(InventoryLocation, item);

                Player.Instance.Currency += item.basePrice;
                Currency -= item.basePrice;
            }
            else
            {
                Debug.Log("Vendor does not have enough money.");
            }
        }

        // Send event that item has been traded
        EventHandler.CallItemTradedEvent();
    }

    public void BuyItemFromVendor(ItemDetails item)
    {
        if (item != null)
        {
            if (Player.Instance.Currency >= item.basePrice)
            {
                InventoryManager.Instance.RemoveItem(InventoryLocation, item.itemCode);
                InventoryManager.Instance.AddItem(InventoryLocation.player, item);

                Currency += item.basePrice;
                Player.Instance.Currency -= item.basePrice;
            }
            else
            {
                Debug.Log("You don't have enough money.");
            }
        }

        // Send event that item has been traded
        EventHandler.CallItemTradedEvent();
    }
}
