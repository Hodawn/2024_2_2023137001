using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ilnteractable
{
    string GetInteractPrompt();
    void OnInteract(GameObject player);
    float GetInteractionDistance();
    bool CanInteract(GameObject player);

}

