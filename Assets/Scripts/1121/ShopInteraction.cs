using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour , Ilnteractable
{
    public string GetInteractPrompt() => "���� ����";
    public float GetInteractionDistance() => 2f;

    public bool CanInteract(GameObject player) => true;
    public void OnInteract(GameObject player)
    {
        FloatingTextManager.Instance.ShowFloatingText("���� ����", transform.position);
    }
   
}
