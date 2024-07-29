using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HuntManager : MonoBehaviour
{
    public GameObject huntMessage; ///< The UI element that displays the hunt message. Assign in the Inspector.

    /// <summary>
    /// Displays the hunt message at the specified world position.
    /// </summary>
    /// <param name="position">The world position where the message should be shown.</param>
    public void ShowHuntMessageAtPosition(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        huntMessage.transform.position = screenPosition;
        huntMessage.SetActive(true);
        StartCoroutine(HideMessage());
    }

    /// <summary>
    /// Hides the hunt message after a short delay.
    /// </summary>
    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        huntMessage.SetActive(false);
    }
}
