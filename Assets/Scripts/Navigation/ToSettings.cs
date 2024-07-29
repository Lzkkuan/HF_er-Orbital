using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToSettings()
    {
        SceneManager.LoadScene(5);
    }

}
