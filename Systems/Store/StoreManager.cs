using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager: MonoBehaviour
{
    #region Components
    public StoreData storeData;
    [SerializeField] private ItemDatabase database;
    [SerializeField] private Transform store;
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform itemDetails;
    [SerializeField] private Transform bulkDetails;
    [SerializeField] private Transform confirmation;
    [SerializeField] public PlayerInputHandler InputHandler;
    [SerializeField] PlayerData playerData;
    public DialogueManager dialogueManager;
    public InventoryManager inventoryManager;
    #endregion

    #region Other Variables
    const float MIN_INPUT_VALUE = 0.7f;
    private bool isStoreActive, isBulkBuyingActive, isConfirmationActive, confirmPurchase;
    private int selectedIndex, bulkAmount;
    private float lastInputTime = -100f;
    public float waitTime = 0.25f;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        isStoreActive = isBulkBuyingActive = isConfirmationActive = confirmPurchase = false;
        bulkAmount = 1;
        selectedIndex = 0;
        store.gameObject.SetActive(false);
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    private void Update()
    {
        if (isStoreActive)
        {
            UpdateItemDetails();
            if (isBulkBuyingActive)
            {
                UpdateBulkBuyingInput();
                bulkDetails.gameObject.SetActive(true);
                bulkDetails.Find("Bulk Amount").GetComponent<TextMeshProUGUI>().text = bulkAmount + "   (" + bulkAmount * GetSelectedItem().price + " BC)";
                bulkDetails.Find("Purchase").GetComponent<TextMeshProUGUI>().text = "Purchase " + GetSelectedItem().name + "?";
            }
            else if (isConfirmationActive)
            {
                UpdateConfirmInput();
                confirmation.gameObject.SetActive(true);
                confirmation.Find("Purchase").GetComponent<TextMeshProUGUI>().text = "Purchase " + GetSelectedItem().name + "?";
            }
            else
            {
                UpdateNormalInput();
                bulkDetails.gameObject.SetActive(false);
                confirmation.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Main Store Functions
    public virtual void ActivateStore(StoreData storeData)
    {
        selectedIndex = 0;
        bulkAmount = 1;
        this.storeData = storeData;
        store.gameObject.SetActive(true);
        foreach (Item item in storeData.inventory.items)
        {
            GameObject workspace = Instantiate(itemSlot, itemContainer);
            workspace.GetComponentInChildren<ItemSlot>().item = item;
            workspace.GetComponentInChildren<TextMeshProUGUI>().text = item.price + " BC";
            workspace.transform.Find("Sprite").GetComponent<Image>().sprite = item.image;
            workspace.transform.Find("Sprite").GetComponent<Image>().SetNativeSize();
            workspace.transform.Find("Select").gameObject.SetActive(false);
        }
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Select();
        dialogueManager.StartDialogue(storeData.welcomeDialogue);
        InputHandler.SetActionMapToInteractions();
        isStoreActive = true;
    }
    public virtual void DeactivateStore()
    {
        isStoreActive = false;
        store.gameObject.SetActive(false);
        dialogueManager.ExitDialogue();
        RemoveSlots();
    }
    private void UpdateItemDetails()
    {
        itemDetails.Find("Item Name").GetComponent<TextMeshProUGUI>().text = GetSelectedItem().name;
        itemDetails.Find("Item Description").GetComponent<TextMeshProUGUI>().text = GetSelectedItem().description;
        if (GetSelectedItem().type == ItemType.Consumable)
        {
            itemDetails.Find("Item Amount").gameObject.SetActive(true);
            itemDetails.Find("Item Amount").GetComponent<TextMeshProUGUI>().text = "Inventory: (" + GameStatus.GetInstance().playerInventory.CheckAmount(database.items.IndexOf(GetSelectedItem())) + ")";
        }
        else
        {
            itemDetails.Find("Item Amount").gameObject.SetActive(false);
        }
    }
    private void Purchase()
    {
        GameStatus.GetInstance().playerInventory.AddItem(database.items.IndexOf(GetSelectedItem()), 1);
        GameStatus.GetInstance().coinCount -= GetSelectedItem().price;
        MoveSelectLeft();
        itemContainer.GetChild(selectedIndex + 1).GetComponent<ItemSlot>().Remove();
        dialogueManager.StartDialogue(storeData.purchaseDialogue);
    }
    private void PurchaseInBulk(int amount)
    {
        GameStatus.GetInstance().playerInventory.AddItem(database.items.IndexOf(GetSelectedItem()), amount);
        GameStatus.GetInstance().coinCount -= amount * GetSelectedItem().price;
        isBulkBuyingActive = false;
        dialogueManager.StartDialogue(storeData.purchaseDialogue);
    }
    #endregion

    #region Input Functions
   
    #region Move Select Specific
    private void MoveSelectRight()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Deselect();
        selectedIndex++;
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Select();
    }
    private void MoveSelectLeft()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Deselect();
        selectedIndex--;
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Select();
    }
    private void MoveSelectUp()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Deselect();
        selectedIndex -= 6;
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Select();
    }
    public void MoveSelectDown()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Deselect();
        selectedIndex += 6;
        itemContainer.GetChild(selectedIndex).GetComponent<ItemSlot>().Select();
    }
    #endregion
    private void UpdateNormalInput()
    {
        if (InputHandler.MovementInput.x >= MIN_INPUT_VALUE && selectedIndex < itemContainer.childCount - 1 && Time.time >= lastInputTime + waitTime)
        {
            MoveSelectRight();
            lastInputTime = Time.time;
        }
        else if (InputHandler.MovementInput.x <= -MIN_INPUT_VALUE && selectedIndex > 0 && Time.time >= lastInputTime + waitTime)
        {
            MoveSelectLeft();
            lastInputTime = Time.time;
        }
        else if (InputHandler.MovementInput.y <= -MIN_INPUT_VALUE && selectedIndex + 6 <= itemContainer.childCount - 1 && Time.time >= lastInputTime + waitTime)
        {
            MoveSelectDown();
            lastInputTime = Time.time;
        }
        else if (InputHandler.MovementInput.y >= MIN_INPUT_VALUE && selectedIndex - 6 >= 0 && Time.time >= lastInputTime + waitTime)
        {
            MoveSelectUp();
            lastInputTime = Time.time;
        }
        else if (InputHandler.ContinueInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseContinueInput();
            lastInputTime = Time.time;

            if (dialogueManager.CheckTypingDone())
            {
                if (GetSelectedItem().price <= GameStatus.GetInstance().coinCount)
                {
                    if (GetSelectedItem().type == ItemType.Consumable)
                    {
                        isBulkBuyingActive = true;
                        bulkAmount = 1;
                    }

                    else
                    {
                        isConfirmationActive = true;
                        confirmPurchase = true;

                    }
                }
                else
                {
                    dialogueManager.StartDialogue(storeData.noMoneyDialogue);
                }
            }
        }
        else if (InputHandler.ExitInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseExitInput();
            lastInputTime = Time.time;
            DeactivateStore();
        }
    }
    private void UpdateBulkBuyingInput()
    {        
        if (InputHandler.MovementInput.y >= MIN_INPUT_VALUE && (bulkAmount + 1) * GetSelectedItem().price <= 
            GameStatus.GetInstance().coinCount && Time.time >= lastInputTime + waitTime)
        {
            bulkAmount++;
            lastInputTime = Time.time;
        }
        else if (InputHandler.MovementInput.y <= -MIN_INPUT_VALUE && bulkAmount > 1 && Time.time >= lastInputTime + waitTime)
        {
            bulkAmount--;
            lastInputTime = Time.time;
        }
        else if (InputHandler.ContinueInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseContinueInput();
            lastInputTime = Time.time;
            PurchaseInBulk(bulkAmount);
        }
        else if (InputHandler.ExitInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseExitInput();
            lastInputTime = Time.time;
            isBulkBuyingActive = false;
        }
    }
    private void UpdateConfirmInput()
    {
        if (InputHandler.MovementInput.x >= MIN_INPUT_VALUE && confirmPurchase && Time.time >= lastInputTime + waitTime)
        {
            confirmPurchase = false;
            lastInputTime = Time.time;
        }
        else if (InputHandler.MovementInput.x <= MIN_INPUT_VALUE && !confirmPurchase && Time.time >= lastInputTime + waitTime)
        {
            confirmPurchase = true;
            lastInputTime = Time.time;
        }
        else if (InputHandler.ContinueInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseContinueInput();
            if (confirmPurchase)
            {
                Purchase();
                isConfirmationActive = false;
            }
            else
            {
                isConfirmationActive = false;
            }
        }
        else if (InputHandler.ExitInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseExitInput();
            isConfirmationActive = false;
        }

        if (confirmPurchase)
        {
            confirmation.Find("Yes").GetComponent<Text>().color = Color.red;
            confirmation.Find("No").GetComponent<Text>().color = Color.black;
        }
        else
        {
            confirmation.Find("Yes").GetComponent<Text>().color = Color.black;
            confirmation.Find("No").GetComponent<Text>().color = Color.red;
        }
    }
    #endregion

    #region Other Functions
    public void RemoveSlots()
    {
        foreach (Transform child in itemContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public bool CheckIfStoreActive() => isStoreActive;
    private Item GetSelectedItem() => itemContainer.GetChild(selectedIndex).GetComponentInChildren<ItemSlot>().item;
    #endregion
}
