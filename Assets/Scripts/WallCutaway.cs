using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCutaway : MonoBehaviour
{
    [SerializeField] private bool isPlayerOccupied;
    private Transform southEastWallContainer;
    private MeshRenderer[] wallsToCut;
    [SerializeField] private Material stoneWall;
    [SerializeField] private Material stoneWallTransparent;

    private void Start()
    {
        southEastWallContainer = transform.Find("SEWalls");
        wallsToCut = southEastWallContainer.GetComponentsInChildren<MeshRenderer>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            isPlayerOccupied = true;
            foreach(MeshRenderer meshRenderer in wallsToCut)
            {
                var materialCopy = meshRenderer.material;
                materialCopy = stoneWallTransparent;
                meshRenderer.material = materialCopy;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            isPlayerOccupied = false;
            foreach (MeshRenderer meshRenderer in wallsToCut)
            {
                var materialCopy = meshRenderer.material;
                materialCopy = stoneWall;
                meshRenderer.material = materialCopy;
            }
        }
    }
}
