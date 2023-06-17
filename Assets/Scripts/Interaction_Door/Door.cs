using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;


    public string InteractionPrompt => _promt;

    bool IInteractable.Interact(Interactor interactor)
    {
        Debug.Log("TO MARS STATION");
        return true;
    }
}
