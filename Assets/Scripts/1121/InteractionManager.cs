using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private float checkRadius =3f;
    [SerializeField] private LayerMask interactableLayers;

    private Ilnteractable currentInteractable;
    private GameObject player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckInteractables();
        if(Input.GetKeyDown(KeyCode.E)&& currentInteractable != null)
        {
            currentInteractable.OnInteract(player);
        }
    }
    private void CheckInteractables()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, checkRadius, interactableLayers);
        Ilnteractable closest=null;
        float closetsDistance=float.MaxValue;

        foreach(var col in colliders)
        {
            if(col.TryGetComponent<Ilnteractable>(out var interactable))
            {
                float distance = Vector3.Distance(player.transform.position, col.transform.position);

                if(distance <= interactable.GetInteractionDistance() && distance < closetsDistance && interactable.CanInteract(player))
                {
                    closest = interactable;
                    closetsDistance= distance;
                }
            }
        }

        currentInteractable=closest;
        UpdatePrompt();
    }
    private void UpdatePrompt()
    {
        if(currentInteractable != null)
        {
            promptText.text = $"[E]{currentInteractable.GetInteractPrompt()}";
            promptText.gameObject.SetActive(true);
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
   
}
