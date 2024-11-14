using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



//
public interface IItem 
{
    string Name { get;}
    int ID { get; }
    void Use();
}

//CraftingMaterial 클래스 추가
public class CraftingMaterial : IItem
{
    public string Name { get; private set; }
    public int ID { get; private set; }

    public CraftingMaterial(string name, int id)
    {
        Name = name;
        ID = id;
    }

    public void Use()
    {
        Debug.Log($"This is a  crafting material : {Name}");
    }

    public class Weapon : IItem
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public int Damage { get; private set; }

        public Weapon(string name, int iD, int damage)
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
            if (index >= 0 && index < items.Count)
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
            foreach (var item in items)
            {
                Debug.Log($"Item: {item.Name}, ID : {item.ID}");
            }
        }
        public void RemoveItems(int itemId, int amount)
        {
            int removed = 0;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].ID == itemId)
                {
                    items.RemoveAt(i);
                    removed++;
                    if (removed > amount)
                        break;
                }
            }
        }

        public bool HasEnough(int itemId, int amount)               //아이템이 충분한지 검사
        {
            return GetItemCount(itemId) >= amount;
        }

        public int GetItemCount(int itemId)                         //아이템 카운트 함수
        {
            return items.Count(item => item.ID == itemId);
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

            playerInventory.AddItem(new CraftingMaterial("Iron Ingot", 101));       //ID 101: 철 주괴
            playerInventory.AddItem(new CraftingMaterial("Iron Ingot", 101));       //ID 101: 철 주괴
            playerInventory.AddItem(new CraftingMaterial("Wood", 102));       //ID 102: 나무

            playerInventory.AddItem(new CraftingMaterial("Herb", 201));       //ID 201: 약초
            playerInventory.AddItem(new CraftingMaterial("Herb", 201));       //ID 201: 약초
            playerInventory.AddItem(new CraftingMaterial("Water", 202));       //ID 202: 물
        }
        //인벤토리 접근자 메서드 추가
        public Inventory<IItem> GetInventory()
        {

        return playerInventory; 
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerInventory.AddItem(new Weapon("Sword", 1, 10));            //아이템 생성
            }
        }
    }
}
