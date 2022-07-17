using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int x;

    private int y;

    private bool isfilled;
    
    public Cell(int x, int y, bool isfilled)
    {
        this.x = x;
        this.y = y;
        this.isfilled = isfilled;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void setIsFilled(bool isfilled)
    {
        this.isfilled = isfilled;
    }

    public bool getIsFilled()
    {
        return isfilled;
    }


	
}
