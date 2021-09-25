using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIBarter : MonoBehaviour
{
    [SerializeField] private GameObject itemButtonPrefab;
    
    [SerializeField] private RectTransform playerInventoryPanel;
    [SerializeField] private TextMeshProUGUI playerCurrencyText;

    [SerializeField] private RectTransform vendorInventoryPanel;
    [SerializeField] private TextMeshProUGUI vendorCurrencyText;

    [SerializeField] private RectTransform background;
    
    public Vendor vendor;
    private InventoryLocation vendorInventoryLocation;
    
    private List<InventoryItem> inventoryList;
    private List<GameObject> buttonList;

    private void OnEnable()
    {
        EventHandler.ItemTradedEvent += RefreshInventoryDisplay;
    }

    private void OnDisable()
    {
        EventHandler.ItemTradedEvent -= RefreshInventoryDisplay;
    }

    private void Awake()
    {
        DeactivateBarterMenu();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Init()
    {
        inventoryList = InventoryManager.Instance.GetInventoryByInventoryLocation(InventoryLocation.player);
        buttonList = new List<GameObject>();
        playerCurrencyText.text = "$" + Player.Instance.Currency.ToString();
    }

    public void StartTradeWithVendor(Vendor vendor)
    {
        ClearInventoryPanel();

        SetVendor(vendor);

        PopulateInventoryPanel(vendorInventoryLocation, vendorInventoryPanel);

        PopulateInventoryPanel(InventoryLocation.player, playerInventoryPanel);

        ActivateBarterMenu();
    }

    private void SetVendor(Vendor vendor)
    {
        this.vendor = vendor;

        vendorInventoryLocation = vendor.InventoryLocation;

        vendorCurrencyText.text = "$" + vendor.Currency.ToString();
    }

    private void PopulateInventoryPanel(InventoryLocation inventoryLocation, RectTransform inventoryContentPanel)
    {
        inventoryList = InventoryManager.Instance.GetInventoryByInventoryLocation(inventoryLocation);

        foreach (InventoryItem inventoryItem in inventoryList)
        {
            GameObject button = Instantiate(itemButtonPrefab, transform.position, Quaternion.identity) as GameObject;
            button.transform.SetParent(inventoryContentPanel);
            button.GetComponent<UIItemButton>().SetItemButton(vendor, inventoryItem, inventoryLocation);
            buttonList.Add(button);
        }
    }

    private void RefreshCurrencyDisplay()
    {
        playerCurrencyText.text = "$" + Player.Instance.Currency.ToString();
        vendorCurrencyText.text = "$" + vendor.Currency.ToString();
    }

    private void ClearInventoryPanel()
    {
        foreach (GameObject button in buttonList)
        {
            Destroy(button);
        }
    }

    private void RefreshInventoryDisplay()
    {
        ClearInventoryPanel();

        PopulateInventoryPanel(vendorInventoryLocation, vendorInventoryPanel);

        PopulateInventoryPanel(InventoryLocation.player, playerInventoryPanel);

        RefreshCurrencyDisplay();
    }

    private void ActivateBarterMenu()
    {
        background.gameObject.SetActive(true);
    }

    public void DeactivateBarterMenu()
    {
        background.gameObject.SetActive(false);
    }
}