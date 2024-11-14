using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.CraftingSystem
{
    [System.Serializable]
    public class Recipe 
    {

        public string recipeId;
        public IItem resultItem;
        public int resultAmount;
        public Dictionary<int, int> requiredMaterials;
        public int requiredLevel;
        public float baseSuccessRate;
        public float craftTime;

        public Recipe(string id, IItem result, int amount)
        {
            recipeId = id;
            resultItem = result;
            resultAmount = amount;
            requiredMaterials = new Dictionary<int, int>();
            requiredLevel = 1;
            baseSuccessRate = 1;
            craftTime = 0;
        }
        public void AddRequirdMaterial(int itemId, int amount)
        {
            if(requiredMaterials.ContainsKey(itemId))
            {
                requiredMaterials[itemId] = amount;
            }
            else
            {
                requiredMaterials[itemId]= amount;
            }
        }
    }
}
