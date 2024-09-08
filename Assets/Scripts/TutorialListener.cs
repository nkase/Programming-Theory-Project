using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialListener : MonoBehaviour
{
    [SerializeField] private GameObject tutorialDisplay;
    private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject tutorialDisplaySmall;
    private TextMeshProUGUI tutorialTextSmall;

    private string[] tutorialMessages =
    {
        "Use W A S D or arrow keys to move",
        "Hold Shift to sprint",
        "Press Spacebar to attack",
        "Press F to interact with highlighted objects",
        "Congratulations! Press Esc to return to main menu"
    };

    // Start is called before the first frame update
    void Start()
    {
        TutorialTrigger.playTutorial += DisplayTutorial;
        TutorialTrigger.dismissTutorial += DismissTutorial;
        tutorialText = tutorialDisplay.GetComponentInChildren<TextMeshProUGUI>();
        tutorialTextSmall = tutorialDisplaySmall.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayTutorial(int tutorialSegment, bool tutorialSegmentHasPlayed)
    {
        if (tutorialSegmentHasPlayed)
        {
            DisplayTutorialSmall(tutorialSegment);
        }
        else
        {
            tutorialText.text = tutorialMessages[tutorialSegment];
            tutorialDisplay.gameObject.SetActive(true);
            StartCoroutine(DisplaySmallTutorialAfterTimer(tutorialSegment));
        }
    }

    private void DisplayTutorialSmall(int tutorialSegment)
    {
        tutorialTextSmall.text = tutorialMessages[tutorialSegment];
        tutorialDisplaySmall.gameObject.SetActive(true);
    }

    private void DismissTutorial()
    {
        tutorialDisplay.gameObject.SetActive(false);
        tutorialDisplaySmall.gameObject.SetActive(false);
    }

    private IEnumerator DisplaySmallTutorialAfterTimer(int tutorialSegment)
    {
        yield return new WaitForSeconds(2);
        DismissTutorial();
        DisplayTutorialSmall(tutorialSegment);
    }
}
