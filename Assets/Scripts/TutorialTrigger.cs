using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private bool tutorialSegmentHasPlayed;
    [SerializeField] private int tutorialSegment;


    // ENCAPSULATION
    public delegate void PlayTutorial(int tutorialSegment, bool tutorialSegmentHasPlayed);
    public static event PlayTutorial playTutorial;
    
    public delegate void DismissTutorial();
    public static event DismissTutorial dismissTutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            playTutorial?.Invoke(tutorialSegment, tutorialSegmentHasPlayed);
            if (tutorialSegmentHasPlayed == false)
            {
                tutorialSegmentHasPlayed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent <Player>() != null)
        {
            tutorialSegmentHasPlayed = true;
            dismissTutorial?.Invoke();
            tutorialSegmentHasPlayed = true;
        }
    }
}
