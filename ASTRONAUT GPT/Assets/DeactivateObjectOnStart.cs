using UnityEngine;

public class DeactivateObjectOnStart : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
