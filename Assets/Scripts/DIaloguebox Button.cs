using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogueboxButton : MonoBehaviour
{
    public GameObject dialogueBox1;
    // Start is called before the first frame update
    public void CloseWind()
    {
        dialogueBox1.SetActive(false);
    }
}
