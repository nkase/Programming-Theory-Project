using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ABSTRACTION at its finest
public class MoveForward : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward*Time.deltaTime);
    }
}
