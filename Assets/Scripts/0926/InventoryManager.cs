using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//
public interface IItem 
{
    string Name { get;}
    int ID { get; }
    void Use();
}

public class Weapon : IItem
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int Damage { get; private set; }

    public Weapon(string name, int iD, int damage )
    {
        Name = name;
        ID = iD;
        Damage = damage;
    }

    public void Use()
    {
        Debug.Log($"Using weapon {Name} with damage {Damage}");
    }
}

public class HealthPotion : IItem
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int HealAmount { get; private set; }

    public HealthPotion(string name, int id, int healAmount)
    {
        Name = name;
        ID = id;
        HealAmount = healAmount;
    }
    public void Use()
    {
        Debug.Log($"Using weapon {Name} with Damage {HealAmount}");
    }
}

public class Inventory<T> where T : IItem
{
    private List<T> items = new List<T>();

    public void AddItem(T item)
    {
        items.Add(item);
        Debug.Log($"Add {item.Name} to inventory");
    }

    public void UseItem(int index)
    {
        if(index>=0&&index < items.Count)
        {
            items[index].Use();
        }
        else
        {
            Debug.Log("Invalid item index");
        }
    }
    public void ListItems()
    {
        foreach(var item in items)
        {
            Debug.Log($"Item: {item.Name}, ID : {item.ID}");
        }
    }
}

public class InventoryManager : MonoBehaviour
{
    private Inventory<IItem> playerInventory;
    public int UseBagIndex;

    private void Start()
    {
        playerInventory = new Inventory<IItem>();

        playerInventory.AddItem(new Weapon("Sword", 1, 10));
        playerInventory.AddItem(new HealthPotion("Small Potion", 2, 20));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerInventory.ListItems();            //인벤토리 내용 출력
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInventory.UseItem(UseBagIndex);                 //첫번째 아이템 사용
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerInventory.AddItem(new Weapon("Sword", 1, 10));            //아이템 생성
        }
    }
}
