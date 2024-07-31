using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInputManager : MonoBehaviour
{
    public InputField nameInputField; // Reference to the InputField UI element
    public Button confirmButton; // Reference to the Button UI element

    private void Start()
    {
        confirmButton.onClick.AddListener(SaveNameAndStartGame); // Add listener to the button
    }

    /// <summary>
    /// Saves the player's formatted name and starts the game by loading the next scene.
    /// </summary>
    private void SaveNameAndStartGame()
    {
        string playerName = nameInputField.text; // Get the player's name from the InputField
        string formattedName = $"Polar Bear <size=80>{playerName}</size>"; // Format the player's name
        PlayerPrefs.SetString("PlayerName", formattedName); // Save the formatted name using PlayerPrefs
        SceneManager.LoadScene("Level 1");
    }
}
