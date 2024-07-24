using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HuntManager : MonoBehaviour
{
    public GameObject huntMessage; // Assign in the Inspector

    public void ShowHuntMessageAtPosition(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        huntMessage.transform.position = screenPosition;
        huntMessage.SetActive(true);
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        huntMessage.SetActive(false);
    }
}

