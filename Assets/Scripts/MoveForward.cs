using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward*Time.deltaTime);
    }
}
