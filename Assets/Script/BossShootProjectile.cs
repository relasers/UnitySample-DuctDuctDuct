using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootProjectile : MonoBehaviour {

    public GameObject player;
    public GameObject bullet;

    Coroutine BulletShoot_Coroutine;

    public float randomdelay_min = 0.5f;
    public float randomdelay_max = 1.5f;

    IEnumerator ShootProjectile(float delay)
    {
        while (true)
        {

            yield return new WaitForSeconds(delay);

            GameObject new_bullet = Instantiate(bullet, transform.position, Quaternion.identity);
            new_bullet.GetComponent<BossProjectile>().player = player;
            new_bullet.GetComponent<BossProjectile>().MovingDirection = Vector3.Normalize(player.transform.position - new_bullet.transform.position);

            Light light = GetComponent<Light>();

            if (light)
            {
                light.intensity += 20;
            }
        }
    }

	// Use this for initialization
	void Start () {
        BulletShoot_Coroutine = StartCoroutine(ShootProjectile(Random.Range(randomdelay_min,randomdelay_max)));
    }

    // Update is called once per frame
    void Update() {

        Light light = GetComponent<Light>();

        if (light)
        {
            light.intensity = Mathf.Max(0, light.intensity*0.5f-1 );
        }

	}
}
