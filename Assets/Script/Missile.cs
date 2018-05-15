using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed = 150.0f;
    public float LifeTime = 1.5f;
    IEnumerator SelfDestroy(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }

    Coroutine deathTImer;
	// Use this for initialization
	void Start () {
        deathTImer = StartCoroutine(SelfDestroy(LifeTime));

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = transform.forward * speed;

    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(transform.position + transform.forward);

	}

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "SolidBlock")
        {
            Destroy(gameObject);
        }
    }
}
