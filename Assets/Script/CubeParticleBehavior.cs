using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParticleBehavior : MonoBehaviour {
    public Vector3 random_direction;
    public float speed = 100;
    public float lifetime = 100;
    public bool isActive = false;
    // Use this for initialization

    void Start () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        random_direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        random_direction.Normalize();
        rigidbody.velocity = random_direction * speed;

    }

    private void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        speed--;
        rigidbody.velocity = random_direction * speed;
    }
    
    // Update is called once per frame
    void Update () {
        lifetime--;

        if (lifetime <= 0)
        {
            isActive = false;

            Destroy(gameObject);

        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "SolidBlock")
        {
            // 접촉 지점을 찾아낸다.
            ContactPoint contact;

            if (collision.contacts.Length > 0)
            {
                contact = collision.contacts[0];

                Vector3 MovingVector = Vector3.Reflect(random_direction, contact.normal);
                random_direction = MovingVector;
                rigidbody.velocity = MovingVector *speed;
            }

        }

    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "SolidBlock")
        {
            // 접촉 지점을 찾아낸다.
            ContactPoint contact;

            if (collision.contacts.Length > 0)
            {
                contact = collision.contacts[0];

                Vector3 MovingVector = Vector3.Reflect(random_direction, contact.normal);
                random_direction = MovingVector;
                rigidbody.velocity = MovingVector * speed;
            }

        }
    }
}
