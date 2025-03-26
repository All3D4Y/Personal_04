using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
