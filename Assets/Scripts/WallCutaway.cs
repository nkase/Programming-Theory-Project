using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCutaway : MonoBehaviour
{
    //private bool isPlayerOccupied;
    private int playerEnterCount;
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
            playerEnterCount++;
            //isPlayerOccupied = true;
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
            playerEnterCount--;
            if (playerEnterCount <= 0)
            {
                //isPlayerOccupied = false;
                foreach (MeshRenderer meshRenderer in wallsToCut)
                {
                    var materialCopy = meshRenderer.material;
                    materialCopy = stoneWall;
                    meshRenderer.material = materialCopy;
                }
            }
        }
    }
}
