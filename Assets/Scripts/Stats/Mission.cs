using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public GameObject dialogueBox1;
    public GameObject dialogueBox2;
    public GameObject dialogueBox3;
    public GameObject missionCompleteAnimation;

    private int missionStage = 0;  // Track the mission progress

    void Start()
    {
        dialogueBox1.SetActive(false);
        dialogueBox2.SetActive(false);
        dialogueBox3.SetActive(false);
        missionCompleteAnimation.SetActive(false);
    }

    public void TalkToNPC1()
    {
        if (missionStage == 0)
        {
            dialogueBox1.SetActive(true);
            missionStage = 1;  // Move to next stage
        }
        else if (missionStage == 2)
        {
            dialogueBox3.SetActive(true);
            missionStage = 3;  // Move to final stage
        }
    }

    public void TalkToNPC2()
    {
        if (missionStage == 1)
        {
            dialogueBox2.SetActive(true);
            missionStage = 2;  // Move to next stage
        }
    }

    public void EndConversation(GameObject dialogueBox)
    {
        dialogueBox.SetActive(false);

        if (missionStage == 3)
        {
            CompleteMission();
        }
    }

    void CompleteMission()
    {
        missionCompleteAnimation.SetActive(true);
        // Additional logic for mission completion
    }
}
