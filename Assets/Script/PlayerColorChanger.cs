using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        // 벽에 부딫힌 상태면 색상을 변경하자.
        if (other.gameObject.tag == "SolidBlock")
        {
            MeshRenderer mesh_renderer = GetComponentInParent<MeshRenderer>();

            mesh_renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "SolidBlock")
        {
            MeshRenderer mesh_renderer = GetComponentInParent<MeshRenderer>();

            mesh_renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            Debug.Log("Act");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SolidBlock")
        {


            MeshRenderer mesh_renderer = GetComponentInParent<MeshRenderer>();
            mesh_renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
