using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private bool inventoryActivated = false;
    private bool statDetailActivated = false;

    [SerializeField]
    private GameObject go_statDetail;
    [SerializeField] 
    private GameObject go_inventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private GameObject go_itemDetail;

    [SerializeField]
    public GameObject go_player;

    private Slot[] slots;
    private int index;

    void Awake()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        index = 0;
    }

    void Start()
    {
        UpdateStatDetail();
    }
    void Update()
    {
        UseInventory();
        UseStatDetail();
    }

    private void UseInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryActivated)
            {
                CloseInventory();
            } 
            else
            {
                OpenInventory();
            }
        }
    }

    private void UseStatDetail()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (statDetailActivated)
            {
                CloseStatDetail();
            } 
            else
            {
                OpenStatDetail();
            }
        }
    }
    
    // Stat UI
    private void OpenStatDetail()
    {
        go_statDetail.SetActive(true);
        statDetailActivated = true;
    }
    public void CloseStatDetail()
    {
        go_statDetail.SetActive(false);
        statDetailActivated = false;
    }
    public void UpdateStatDetail()
    {
        go_statDetail.transform.GetChild(1).GetComponent<Text>().text = go_player.GetComponent<Player>()._stat.ToString();
    }

    // Inventory UI
    private void OpenInventory()
    {
        go_inventoryBase.SetActive(true);
        inventoryActivated = true;
    }

    public void CloseInventory()
    {
        go_inventoryBase.SetActive(false);
        inventoryActivated = false;

        // 아이템 디테일 창도 닫음
        CloseItemDetail();
    }

    public bool AcquireItem(int _id, int _count = 1)
    {
        if (index >= slots.Length)
        {
            return false;
        }

        slots[index].AddItem(_id, _count);
        index += 1;
        return true;
    }

    public void OpenItemDetail(Item item, Image itemImage)
    {
        go_itemDetail.transform.GetChild(1).GetComponent<Image>().sprite = itemImage.sprite;
        go_itemDetail.transform.GetChild(2).GetComponent<Text>().text = item.ItemName;
        go_itemDetail.transform.GetChild(3).GetComponent<Text>().text = item.ToString();

        go_itemDetail.SetActive(true);
    }

    private void CloseItemDetail()
    {
        go_itemDetail.SetActive(false);
        go_itemDetail.transform.GetChild(1).GetComponent<Image>().sprite = null;
        go_itemDetail.transform.GetChild(2).GetComponent<Text>().text = "";
        go_itemDetail.transform.GetChild(3).GetComponent<Text>().text = "";
    }
}
