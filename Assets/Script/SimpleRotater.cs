using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 1, 0), 50);

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color = new Color(renderer.material.color.r,
            renderer.material.color.g,
            renderer.material.color.b,
            renderer.material.color.a - 0.01f
            );

        transform.localScale = new Vector3(transform.localScale.x-0.01f,
            transform.localScale.y - 0.01f,
            transform.localScale.z - 0.01f

            );
    }

    
}
