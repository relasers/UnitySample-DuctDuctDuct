using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed = 150.0f;
    public float LifeTime = 0.1f;
    public float Range = 300.0f;
    // 폭발한다.
    void Explosion()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in objects)
        {
            // 폭발 사거리 밖이면 스킵
            if (Vector3.Distance(obj.transform.position, transform.position) > Range) continue;

            EnemyStat stat = obj.GetComponent<EnemyStat>();

            if (stat)
            {
                stat.hp -= 10;
            }
        }

        Destroy(gameObject);

    }
    

    IEnumerator SelfDestroy(float delay)
    {

        yield return new WaitForSeconds(delay);
        Explosion();


    }

    Coroutine deathTImer;
	// Use this for initialization
	void Start () {
        //deathTImer = StartCoroutine(SelfDestroy(LifeTime));
       
       // Rigidbody rigidbody = GetComponent<Rigidbody>();

        //rigidbody.velocity = transform.forward * speed;

    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(transform.position + transform.forward);
        Explosion();
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
