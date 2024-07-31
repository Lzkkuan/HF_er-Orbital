using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public MultipleTalkable Multipletalkable; // Reference to the Talkable script
    public GameObject Enemy1;
    public GameObject Enemy2;

    private void Update()
    {
        MissionJudge();
    }

    public void MissionJudge()
    {
        // Logic to complete the mission
        if (!Enemy1)
        {
            if (!Enemy2) { FinishTwo(); }
            else { FinishOne();  }
        }
        
        
    }

    public void FinishOne()
    {
        Multipletalkable.isMissionOneCompleted = true;
        Multipletalkable.initialLineSpoken = true;

    }

    public void FinishTwo()
    {
        Multipletalkable.isMissionOneCompleted = true;
        Multipletalkable.isMissionTwoCompleted = true;
        Multipletalkable.initialLineSpoken = true;
    }
}
