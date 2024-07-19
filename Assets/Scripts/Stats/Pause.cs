using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menuPanel; // Reference to your panel

    void Update()
    {
        // Check if the Esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the panel's active state
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive);

            // Pause the game when the menu is active, resume when it is not
            if (isActive)
            {
                Time.timeScale = 1; // Resume game
            }
            else
            {
                Time.timeScale = 0; // Pause game
            }
        }
    }
}
