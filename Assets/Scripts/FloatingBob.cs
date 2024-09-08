using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABSTRACTION
// This behaviour can be applied to any object to make it bob up and down.
public class FloatingBob : MonoBehaviour
{
    [SerializeField] private float oscillationCycleTime;
    [SerializeField] private float oscillationAmplitude;
    private float oscillationTimer = 0;
    private bool isMovingUp = true;

    // Update is called once per frame
    void Update()
    {
        float tempY = Mathf.Lerp(transform.position.y, transform.position.y + oscillationAmplitude, oscillationCycleTime);
        if (isMovingUp)
        {
            transform.Translate(0, tempY * Time.deltaTime, 0);
            oscillationTimer += Time.deltaTime;
            if (oscillationTimer >= oscillationCycleTime)
            {
                isMovingUp = false;
            }
        }
        else
        {
            transform.Translate(0, -tempY * Time.deltaTime, 0);
            oscillationTimer -= Time.deltaTime;
            if (oscillationTimer <= 0)
            {
                isMovingUp = true;
            }
        }
    }
}
