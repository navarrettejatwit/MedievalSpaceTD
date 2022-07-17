using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    public GameObject SlotGraphic = null;

    public GameObject Parent = null;

    private GameObject[,] SlotMatrix;
    
    private Cell[,] VirtualSlotMatrix;

    [SerializeField] private int row;
    
    [SerializeField] private int column;

    private SlotGrid sg;

    // Start is called before the first frame update
    void Start()
    {
        SlotMatrix = new GameObject[row, column];
		VirtualSlotMatrix = new Cell[row, column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {                                       
                Vector3 Position = new Vector3(i, j, -1);
                SlotMatrix[i, j] = Instantiate(SlotGraphic, Position, Quaternion.identity, Parent.transform);
                VirtualSlotMatrix[i, j] = new Cell(i, j, false);
            }
        }
    }

	public bool setSlot(int i, int j){
		if(VirtualSlotMatrix[i, j].getIsFilled() == false){
			VirtualSlotMatrix[i, j].setIsFilled(true);
			return true;
		}
		return false;
	}
    

}
