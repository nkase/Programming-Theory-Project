using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private bool pauseState;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pauseState)
            {
                Time.timeScale = 0;
                pauseState = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseState = false;
            }

        }
    }
}
