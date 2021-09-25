using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private Image itemIconImage;

    private UIItemHighlight itemHighlight;

    private Vendor vendor;
    private InventoryItem inventoryItem;
    private InventoryLocation inventoryLocation;

    private void Start()
    {
        itemHighlight = FindObjectOfType<UIItemHighlight>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemHighlight != null)
        {
            itemHighlight.SetItemHighlight(inventoryItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemHighlight != null)
        {
            itemHighlight.ClearItemHighlight();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            HandleItemTrade();
        }
    }

    private void HandleItemTrade()
    {
        if (inventoryLocation == InventoryLocation.player)
        {
            var itemToSell = InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode);
            vendor.SellItemToVendor(itemToSell);
        }
        else
        {
            var itemToBuy = InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode);
            vendor.BuyItemFromVendor(itemToBuy);
        }
    }

    public void SetItemButton(Vendor vendor, InventoryItem inventoryItem, InventoryLocation inventoryLocation)
    {
        this.vendor = vendor;
        this.inventoryItem = inventoryItem;
        this.inventoryLocation = inventoryLocation;
        
        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode);

        itemQuantityText.text = inventoryItem.itemQuantity.ToString();
        itemIconImage.sprite = itemDetails.itemSprite;
    }
}