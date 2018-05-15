using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour {

    public float speed = 150.0f;
    public bool isAlive = true;

    public int ammo = 20;
    public int missile_catridge = 0;

    public Camera camera;

    public Quaternion Initalized_rotation;
    public GameObject bullet;
    public GameObject missile;

    bool canShoot = true;
    bool canLaunch = true;

    IEnumerator ShootBullet(float delay)
    {
        ShootBullet();

        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    IEnumerator LaunchMissile(float delay)
    {
        LaunchMissile();

        canLaunch = false;
        yield return new WaitForSeconds(delay);
        canLaunch = true;
    }

        // Use this for initialization
        void Start () {
        Initalized_rotation = transform.rotation;
        camera = GetComponentInChildren<Camera>();
	}

    private void FixedUpdate()
    {

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // 좌우 움직임
        float translation_x = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1) * speed * Time.fixedDeltaTime ;
        // 위 아래 움직임
        float translation_y = Mathf.Clamp(Input.GetAxis("HoverAxis"), -1, 1) * speed * Time.fixedDeltaTime;
        // 정면 후면 움직임
        float translation_z = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1) * speed * Time.fixedDeltaTime;

        
        rigidbody.velocity = (new Vector3(translation_x, translation_y, translation_z) * rigidbody.mass);

        //transform.Translate(translation_x, translation_y, translation_z);



        //  transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateQuaternion, 2  );

        // 전 후진에 따라 카메라 fov 값을 조절해 시야를 변화시킨다.
        camera.fov = 60 - Mathf.Clamp(Input.GetAxis("Vertical") * 0.5f, -1, 1) * 20.0f;



    }

    // Update is called once per frame
    void Update () {
        

        //////////////////////////////////////////////////////
        Ray ray;
        RaycastHit hit;

        ray = camera.ScreenPointToRay(Input.mousePosition);
        
        // 플레이어를 제외한 모든 오브젝트에 대한 레이 캐스팅
        if (Physics.Raycast(ray, out hit, 20000,  -1 ))
        {

           // transform.LookAt(hit.point);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (canShoot)
            {
                StartCoroutine(ShootBullet(0.5f));
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            if (canShoot)
            {
                StartCoroutine(LaunchMissile(1));
            }
        }



    }

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * 5, Quaternion.identity);
        newBullet.transform.rotation = transform.rotation;
        Rigidbody rigidbody = newBullet.GetComponent<Rigidbody>();
        rigidbody.velocity = bullet.transform.forward*100;
    }

    void LaunchMissile()
    {
        GameObject newMissile = Instantiate(missile, new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * 5, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }


    private void OnCollisionStay(Collision collision)
    {


    }

    private void OnCollisionExit(Collision collision)
    {
       
    }

}
