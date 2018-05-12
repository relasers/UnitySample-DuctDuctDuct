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

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // 벽에 부딫힌 상태면 색상을 변경하자.
        if (collision.gameObject.tag == "SolidBlock")
        {
            Material material = GetComponent<MeshRenderer>().material;

            GetComponent<MeshRenderer>().material.color
                = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }


    private void OnCollisionStay(Collision collision)
    {

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "SolidBlock")
        {
            Material material = GetComponent<MeshRenderer>().material;

            GetComponent<MeshRenderer>().material.color
               = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "SolidBlock")
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;

            GetComponent<MeshRenderer>().material.color
                   = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
