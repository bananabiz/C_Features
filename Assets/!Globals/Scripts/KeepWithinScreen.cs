using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Renderer))] //Force Renderer component to be attached
public class KeepWithinScreen : MonoBehaviour {

    private Renderer rend;
    private Camera cam;
    private Bounds camBounds;
    private float camWidth, camHeight;

	// Use this for initialization
	void Start ()
    {
        // Set tje camera
        cam = Camera.main;
        // Get the renderer component (SpriteRenderer, MeshRenderer or SkinnedMeshRenderer)
        rend = GetComponent<Renderer>();
	}

    void UpdateCamBounds()
    {

        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect; // (16:9 -> 16*9)
        camBounds = new Bounds(cam.transform.position, new Vector3(camWidth, camHeight));

    }

    // Adjusts position to fit screen and returns it
    Vector3 CheckBounds()
    {
        //Store current position of the gameObject
        Vector3 pos = transform.position;
        Vector3 size = rend.bounds.size;
        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;


        //Check left
        if (pos.x - halfWidth < camBounds.min.x)
        {
            pos.x = camBounds.min.x + halfWidth;
        }
        //Check right
        else if (pos.x + halfWidth > camBounds.max.x)
        {
            pos.x = camBounds.max.x - halfWidth;
        }
        //Check down
        if (pos.y - halfHeight < camBounds.min.y)
        {
            //pos.y = camBounds.min.y + halfHeight;
            SceneManager.LoadScene(0);
        }
        //Check up
        else if (pos.y + halfHeight > camBounds.max.y)
        {
            pos.y = camBounds.max.y - halfHeight;
        }
        // Returns adjusted position
        return pos;
    }

	// Update is called once per frame
	void Update () {
        // Update the camera bounds
        UpdateCamBounds();
        // Set the position after checking the bounds
        transform.position = CheckBounds();
	}
}