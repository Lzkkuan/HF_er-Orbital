using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public GameObject collectionMessage; ///< The UI element that displays the collection message.

    /// <summary>
    /// Displays the collection message at the specified world position.
    /// </summary>
    /// <param name="position">The world position where the message should be shown.</param>
    public void ShowCollectionMessageAtPosition(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        collectionMessage.transform.position = screenPosition;
        collectionMessage.SetActive(true);
        StartCoroutine(HideMessage());
    }

    /// <summary>
    /// Hides the collection message after a short delay.
    /// </summary>
    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        collectionMessage.SetActive(false);
    }
}
