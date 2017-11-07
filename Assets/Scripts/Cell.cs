using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public bool alive = false;
    public int aliveNeighbors = 0;
    private MeshRenderer renderer;
    private Material mat;

    public bool tempAlive;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void SetAlive(bool val)
    {
        alive = val;
        mat.color = alive ? new Color(0, 1, 0, 1) : new Color(1, 1, 1, 1);
    }

}
