using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//click on a pixel to turn it on and change the neurons' image matrix
public class Pixel : MonoBehaviour {
    public NeuronHidden[] neurons = new NeuronHidden[10];

    public bool lit;
    
    public Material off;
    public Material on;
    public int x;
    public int y;

    //grab the position in the matrix from the position on screen
    void Start()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

    //change color and update the neurons' matrix
	void OnMouseDown()
    {
        if (lit)
        {
            lit = false;            
            GetComponent<Renderer>().material = off;            
        } else
        {
            lit = true;
            GetComponent<Renderer>().material = on;
        }
        for (int i=0; i<10; i++)
        {
            neurons[i].image[x, y] = lit;
        }        
    }
}
