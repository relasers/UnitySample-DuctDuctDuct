using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 Start_Position;
    public float EffectiveRange = 100.0f; 

    // Use this for initialization
    void Start () {
        Start_Position = transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        // 사거리 체크 후 제거
        if (Vector3.Distance(Start_Position, transform.position) > EffectiveRange)
        {
            Destroy(gameObject);
        }

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject)
            {
                EnemyStat stat = collision.transform.GetComponent<EnemyStat>();
                if (stat)
                {
                    stat.AutoTargeted = false;
                    stat.hp--;
                }
            }
            
        }

        Destroy(gameObject);
    }
}
