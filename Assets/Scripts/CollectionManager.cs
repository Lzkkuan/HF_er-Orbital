using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public GameObject collectionMessage; 

    public void ShowCollectionMessageAtPosition(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        collectionMessage.transform.position = screenPosition;
        collectionMessage.SetActive(true);
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        collectionMessage.SetActive(false);
    }
}
