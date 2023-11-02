using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    /*
     * open - the set of nodes to be evaluated
     * closed - the set of nodes already evaluate
     * add start node to open
     * loop
     * current = node in open with lowest f cost
     * remove current from open
     * add current to closed
     * if current is target node return
     * for each neighbor of current node
     * if neighbor is not traversable or closed skip to next neighbor
     * if new path to neighbor is shorter or neighbor not in open
     * set f cost of neighbor
     * set parent of neighbor to current
     * if neighbor is not in open 
     * add neighbor to open
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
