using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour {
    public float MovingSpeed;
    public Vector3 MovingDirection;
    public GameObject player;
	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody)
        {
            rigidbody.velocity = MovingSpeed * MovingDirection;

        }

	}

    private void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject);

    }

}
