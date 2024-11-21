using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;





    //������ ǰ�� ���
    public enum ItemQuality
    {
        Poor,
        Common,
        Uncommon,
        Rate,
        Epic,
        Legendary
    }
    public enum CraftingResult
    {
        Success,
        Failure,
        InsufficientMaterials,
        InvalidRecipe,
        LowSkillLevel
    }

    public interface ICraftable
    {
        ItemQuality Quality { get; }
        bool isStackable { get; }
        int MaxStackSize { get; }
    }


