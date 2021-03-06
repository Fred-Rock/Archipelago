using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField] private int _itemCode;

    private SpriteRenderer spriteRenderer;

    public int ItemCode
    {
        get { return _itemCode; }
        set { _itemCode = value; }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCodeParem)
    {
        if (itemCodeParem != 0)
        {
            ItemCode = itemCodeParem;

            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode);

            spriteRenderer.sprite = itemDetails.itemSprite;

            // if itemtype is reapable, add nudge component
            //if (itemDetails.itemType == ItemType.Reapable_scenery)
            //{
            //    gameObject.AddComponent<ItemNudge>();
            //}
        }
    }
}