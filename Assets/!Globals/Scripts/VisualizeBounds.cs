using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeBounds : MonoBehaviour {

    private Renderer rend;

    void OnDrawGizmos()
    {
        rend = GetComponent<Renderer>();
        Gizmos.DrawWireCube(rend.bounds.center, rend.bounds.size);
    }
}
