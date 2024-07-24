using UnityEngine;

public class OpenWebPage : MonoBehaviour
{
    // URL to open when the button is clicked
    public string url = "https://www.bilibili.com/video/BV1GJ411x7h7/?share_source=copy_web&vd_source=51af7fc9f7fd73471bdd57057f742bc0";

    // Method to be called on button click
    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
