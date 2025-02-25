using UnityEngine;

public class Task2Tutorial : MonoBehaviour
{
    private bool isFirstTrigger = true;
    private TutorialText tutorialText;

    void Start()
    {
        tutorialText = GameObject.Find("TutorialTextManager").GetComponent<TutorialText>();
    }

    void OnTriggerEnter(Collider other)
    {            

        if (other.CompareTag("dog front") && isFirstTrigger && (tutorialText.getCurrTutorialStage() == TutorialText.tutMoveToVase))
        {
            isFirstTrigger = false;
            tutorialText.advanceTutorialStage();
        }
    }
}
