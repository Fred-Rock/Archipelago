using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemHighlight : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescText;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Sprite transparentSprite;
    
    public void SetItemHighlight(InventoryItem inventoryItem)
    {
        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode);

        itemImage.sprite = itemDetails.itemSprite;
        itemNameText.text = itemDetails.itemDescription;
        itemTypeText.text = itemDetails.itemType.ToString();
        itemDescText.text = itemDetails.itemLongDescription;
        itemPrice.text = "$" + itemDetails.basePrice.ToString();
    }

    public void ClearItemHighlight()
    {
        itemImage.sprite = transparentSprite;
        itemNameText.text = "";
        itemTypeText.text = "";
        itemDescText.text = "";
        itemPrice.text = "";
    }
}
