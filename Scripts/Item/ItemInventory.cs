using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Item
{
    NONE,
    MINE,
    TURRET
}




public class ItemInventory : MonoBehaviour
{
    GameObject invenUI;

    [SerializeField]
    Item[] inven = new Item[3];
    [SerializeField]
    GameObject[] item;
    [SerializeField]
    Sprite[] images;

    // Start is called before the first frame update
    void Start()
    {
        invenUI = GameObject.Find("InvenUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            UseItem(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            UseItem(1);
        }
        else if (Input.GetKeyDown("3"))
        {
            UseItem(2);
        }
    }

    public void GetItem(Item _item)
    {
        for(int i = 0; i< inven.Length; i++)
        {
            if(inven[i]== Item.NONE)
            {
                inven[i] = _item;
                SetUI(_item, i);
                break;
            }
        }
    }
    
    void SetUI(Item _item, int idx)
    {
        Text txt = invenUI.transform.GetChild(idx).GetComponentInChildren<Text>();
        Image img = invenUI.transform.GetChild(idx).GetComponent<Image>();

        if (_item == Item.NONE)
        {
            txt.text = (idx + 1).ToString();
            img.sprite = null;
        }

        else if (_item == Item.MINE)
        {
            txt.text = "MINE";
            img.sprite = images[0];
        }

        else if(_item == Item.TURRET)
        {
            txt.text = "TURRET";
            img.sprite = images[1];
        }
    }

    void UseItem(int idx)
    {
        if (inven[idx] != Item.NONE)
        {
            Instantiate(item[(int)inven[idx]-1], transform.position, Quaternion.identity);
            inven[idx] = Item.NONE;
            SetUI(Item.NONE, idx);
        }
    }
}
