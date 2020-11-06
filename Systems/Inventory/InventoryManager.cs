﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Components
    [SerializeField] private ItemDatabase database;
    [SerializeField] private Transform inventoryUI;
    [SerializeField] private GameObject inventoryItemSlot;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform itemDetails;
    [SerializeField] private Transform confirmation;
    [SerializeField] public PlayerInputHandler InputHandler;
    public Player player;
    #endregion

    #region Other Variables
    const float MinInputValue = 0.7f;
    private bool isInventoryActive, isConfirmationActive, confirmUse;
    private int selectedIndex;
    private float lastInputTime = -100f;
    public float waitTime = 0.1f;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        isInventoryActive = isConfirmationActive = false;
        selectedIndex = 0;
        inventoryUI.gameObject.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (isConfirmationActive)
        {
            UpdateConfirmInput();
            confirmation.gameObject.SetActive(true);
            confirmation.Find("Purchase").GetComponent<TextMeshProUGUI>().text = "Use " + GetSelectedItem().name + "?";
        }
        else
        {
            UpdateNormalInput();
            confirmation.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Main Inventory Functions
    public virtual void ActivateInventory()
    {
        selectedIndex = 0;
        inventoryUI.gameObject.SetActive(true);
        InputHandler.SetActionMapToInteractions();
        isInventoryActive = confirmUse = true;

        foreach (InventorySlot item in GameStatus.GetInstance().playerInventory.itemList)
        {
            GameObject workspace = Instantiate(inventoryItemSlot, itemContainer);
            workspace.GetComponentInChildren<InventoryItemSlot>().item = database.items[item.itemID];
            workspace.transform.Find("Sprite").GetComponent<Image>().sprite = database.items[item.itemID].image;
            workspace.transform.Find("Sprite").GetComponent<Image>().SetNativeSize();
            workspace.transform.Find("Frame").gameObject.GetComponent<Image>().color = Color.white;
        }

        if (itemContainer.childCount > 0) { itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Select(); }
    }
    public virtual void DeactivateInventory()
    {
        isInventoryActive = false;
        inventoryUI.gameObject.SetActive(false);
        InputHandler.SetActionMapToGameplay();
        RemoveItems();
    }
    private void UpdateItemDetails()
    {
        if (itemContainer.childCount > 0)
        {
            itemDetails.Find("Item Name").GetComponent<TextMeshProUGUI>().text = GetSelectedItem().name;
            itemDetails.Find("Item Description").GetComponent<TextMeshProUGUI>().text = GetSelectedItem().description;

            if (GetSelectedItem().type == ItemType.Consumable)
            {
                itemDetails.Find("Item Amount").gameObject.SetActive(true);
                itemDetails.Find("Item Amount").GetComponent<TextMeshProUGUI>().text = 
                    "Amount: (" + GameStatus.GetInstance().playerInventory.CheckAmount(database.items.IndexOf(GetSelectedItem())) + ")";
            }
            else
            {
                itemDetails.Find("Item Amount").gameObject.SetActive(false);
            }
        }
        else
        {
            itemDetails.Find("Item Name").GetComponent<TextMeshProUGUI>().text = "";
            itemDetails.Find("Item Description").GetComponent<TextMeshProUGUI>().text = "";
            itemDetails.Find("Item Amount").gameObject.SetActive(false);
        }
    }
    #endregion

    #region Input Functions

    #region Move Select Functions
    private void MoveSelectRight()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Deselect();
        selectedIndex++;
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Select();
    }
    private void MoveSelectLeft()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Deselect();
        selectedIndex--;
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Select();
    }
    private void MoveSelectUp()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Deselect();
        selectedIndex -= 3;
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Select();
    }
    private void MoveSelectDown()
    {
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Deselect();
        selectedIndex += 3;
        itemContainer.GetChild(selectedIndex).GetComponent<InventoryItemSlot>().Select();
    }
    #endregion
    private void UpdateNormalInput()
    {
        if (isInventoryActive)
        {
            UpdateItemDetails();
            if (itemContainer.childCount > 0)
            {
                if (InputHandler.MovementInput.x >= MinInputValue && selectedIndex < itemContainer.childCount - 1 && Time.time >= lastInputTime + waitTime)
                {
                    MoveSelectRight();
                    lastInputTime = Time.time;
                }
                else if (InputHandler.MovementInput.x <= -MinInputValue && selectedIndex > 0 && Time.time >= lastInputTime + waitTime)
                {
                    MoveSelectLeft();
                    lastInputTime = Time.time;
                }
                else if (InputHandler.MovementInput.y <= -MinInputValue && selectedIndex + 3 <= itemContainer.childCount - 1 && Time.time >= lastInputTime + waitTime)
                {
                    MoveSelectDown();
                    lastInputTime = Time.time;
                }
                else if (InputHandler.MovementInput.y >= MinInputValue && selectedIndex - 3 >= 0 && Time.time >= lastInputTime + waitTime)
                {
                    MoveSelectUp();
                    lastInputTime = Time.time;
                }
                else if (InputHandler.ContinueInput)
                {
                    InputHandler.UseContinueInput();
                    if (Time.time >= lastInputTime + waitTime && GetSelectedItem().type == ItemType.Consumable && 
                        GameStatus.GetInstance().currentHealth <  GameStatus.GetInstance().maxHealth)
                    {
                        isConfirmationActive = true;
                    }
                }
            }
            if (InputHandler.ExitInput && Time.time >= lastInputTime + waitTime)
            {
                InputHandler.UseExitInput();
                lastInputTime = Time.time;
                DeactivateInventory();
            }
            else if (InputHandler.InventoryInput && Time.time >= lastInputTime + waitTime)
            {
                InputHandler.UseInventoryInput();
                lastInputTime = Time.time;
                DeactivateInventory();
            }
        }
    }
    private void UpdateConfirmInput()
    {
        if (InputHandler.MovementInput.x >= MinInputValue && confirmUse && Time.time >= lastInputTime + waitTime)
        {
            confirmUse = false;
            lastInputTime = Time.time;
        }
        else if (InputHandler.MovementInput.x <= -MinInputValue && !confirmUse && Time.time >= lastInputTime + waitTime)
        {
            confirmUse = true;
            lastInputTime = Time.time;
        }
        else if (InputHandler.ContinueInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseContinueInput();
            lastInputTime = Time.time;
            if (confirmUse)
            {
                Consumable consumable = (Consumable)GetSelectedItem();
                player.AddHealth(consumable.healValue);
                GameStatus.GetInstance().playerInventory.RemoveItem(database.items.IndexOf(GetSelectedItem()), 1);
                if (GameStatus.GetInstance().playerInventory.CheckAmount(database.items.IndexOf(GetSelectedItem())) == 0)
                {
                    if (selectedIndex == 0) { GameObject.Destroy(itemContainer.GetChild(selectedIndex).gameObject); }
                    else
                    {
                        MoveSelectLeft();
                        GameObject.Destroy(itemContainer.GetChild(selectedIndex + 1).gameObject);
                    }
                }
                isConfirmationActive = false;
            }
            else
            {
                isConfirmationActive = false;
                confirmUse = true;
            }
        }
        else if (InputHandler.ExitInput && Time.time >= lastInputTime + waitTime)
        {
            InputHandler.UseExitInput();
            isConfirmationActive = false;
            confirmUse = true;
        }

        if (confirmUse)
        {
            confirmation.Find("Yes").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            confirmation.Find("No").GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            confirmation.Find("Yes").GetComponent<TextMeshProUGUI>().color = Color.white;
            confirmation.Find("No").GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
    }
    #endregion

    #region Other Functions
    public bool CheckIfInventoryActive() => isInventoryActive;
    private Item GetSelectedItem() => itemContainer.GetChild(selectedIndex).GetComponentInChildren<InventoryItemSlot>().item;
    private void RemoveItems()
    {
        foreach (Transform child in itemContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    #endregion
}
