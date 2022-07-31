using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject grassTile = null;

    [SerializeField] private GameObject grassTile1 = null;

    [SerializeField] private GameObject TreeTile2 = null;
    
    //Castle Tiles
    
    //Mining Tiles

    [SerializeField] private GameObject map = null;

    [SerializeField] private Camera camera;

    [SerializeField] private int row;

    [SerializeField] private int column;

    private GameObject[,] MapMatrix;

    private float aspectRatio;

    private float previousAspectRatio;

    void Start()
    {
        bool makeGreen = false;
        MapMatrix = new GameObject[row, column];
        for (int i = 0; i < row; i++)
        {
            if (makeGreen) makeGreen = false;
            else makeGreen = true;
            
            for (int j = 0; j < column; j++)
            {
                if (j == 0) MapMatrix[i, j] = Instantiate(TreeTile2, new Vector3(i, j, 0), Quaternion.identity, map.transform);
                if (j == column - 1) MapMatrix[i, j] = Instantiate(TreeTile2, new Vector3(i, j, 0), Quaternion.identity, map.transform);
                if (makeGreen && j > 0 && j < row-1)
                {
                    MapMatrix[i, j] = Instantiate(grassTile,  new Vector3(i, j, 0), Quaternion.identity, map.transform);
                    makeGreen = false;

                }
                else if (!makeGreen && j > 0 && j < row-1)
                {
                    MapMatrix[i, j] = Instantiate(grassTile1,  new Vector3(i, j, 0), Quaternion.identity, map.transform);
                    makeGreen = true;
                    
                }
                MapMatrix[i, j].transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));
            }
        }
                                                                          
        Vector3 position = new Vector3((column - 1) / 2f, (row - 1) / 2f, -10);
        camera.transform.position = position;
        aspectRatio = camera.aspect;
        previousAspectRatio = aspectRatio;
        if (camera.orthographic){
            setCameraSize();
        }
    }

    private void setCameraSize()
    {
        float rowBased = row / 2f;
        float columnBased = column / (2f * camera.aspect);
        camera.orthographicSize = Mathf.Max(rowBased, columnBased);
    }
    
}
