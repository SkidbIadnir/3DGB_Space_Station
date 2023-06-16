using UnityEngine;

public class ActivateObjectOnCollision : MonoBehaviour
{
    public GameObject GPT_Canva;
    public GameObject TextField_Canva;
    public GameObject Info_Canva;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Astronaut")) // Replace "Player" with the appropriate tag
        {
            GPT_Canva.SetActive(true);
            TextField_Canva.SetActive(true);
            Info_Canva.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Astronaut")) // Replace "Player" with the appropriate tag
        {
            GPT_Canva.SetActive(false);
            TextField_Canva.SetActive(false);
            Info_Canva.SetActive(true);
        }
    }
}
