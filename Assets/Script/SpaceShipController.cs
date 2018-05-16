using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour {

    public float speed = 150.0f;
    public bool isAlive = true;

    public int ammo = 20;
    public int max_ammo = 20;
    public int missile_catridge = 0;

    public Camera camera;
    public GameObject spaceship;

    public Quaternion Initalized_rotation;
    public GameObject bullet;
    public GameObject missile;

    // 총구
    public GameObject barrel;

    // 마우스 움직임 민감도
    public float MouseSensitivity = 1.0f;
    // 화면을 y축으로 홰까닥 돌려버리면 카메라 뷰가 하자를 일으킴
    public float yAxisLimit_Max = 60.0f;
    public float yAxisLimit_Min = -60.0f;

    public float AmmoRechargeDelay = 1.0f;

    bool canShoot = true;
    bool canLaunch = true;

    // 마우스 고정 켜고 풀기
    // 기본값은 true로, 
    bool toggle_mouse_fixer = true;
    // 회전할 떄 z축을 풀 것인가?
    // Roll 모드
    bool RollMode = false;


    Coroutine BulletRecharge;

    IEnumerator BulletReCharger(float delay)
    {
        while (true)
        {
            if (max_ammo > ammo) ammo++;
            yield return new WaitForSeconds(delay);
        }
        
    }

    IEnumerator ShootBullet(float delay)
    {
        ShootBullet();
        ammo--;
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    IEnumerator LaunchMissile(float delay)
    {
        LaunchMissile();
        missile_catridge--;
        canLaunch = false;
        yield return new WaitForSeconds(delay);
        canLaunch = true;
    }

        // Use this for initialization
    void Start () {
        Initalized_rotation = transform.rotation;
        camera = GetComponentInChildren<Camera>();

        BulletRecharge = StartCoroutine(BulletReCharger(AmmoRechargeDelay));

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

        
        rigidbody.velocity = ( 
            (transform.forward*translation_z +
            transform.right * translation_x +
            transform.up * translation_y

            ) 
            * rigidbody.mass);

        // 전 후진에 따라 카메라 fov 값을 조절해 시야를 변화시킨다.
        camera.fov = 60 - Mathf.Clamp(Input.GetAxis("Vertical") * 0.5f, -1, 1) * 20.0f;



    }

    // Update is called once per frame
    void Update () {

        if (toggle_mouse_fixer)
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Roll 모드 켜져있으면 롤을 한다.
            if (RollMode)
            {
                float zRot = Input.GetAxis("Mouse Y") * MouseSensitivity;
                transform.rotation *= Quaternion.Euler(0, 0, zRot);
            }
            else
            {
                float yRot = Input.GetAxis("Mouse X") * MouseSensitivity;
                float xRot = Input.GetAxis("Mouse Y") * MouseSensitivity;

                // x축은 음수를 붙여줘야 제대로 회전
                // 곱셈연산을 해야 회전이 누적된다.
                transform.rotation *= Quaternion.Euler(-xRot, yRot, 0);
                // Roll 고정이므로 쿼터니언의 z 값을 0으로 고정시킨다.
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            }
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (toggle_mouse_fixer) toggle_mouse_fixer = false;
            else toggle_mouse_fixer = true;
        }

        if (Input.GetMouseButton(2))
        {
            RollMode = true;
        }
        else
        {
            RollMode = false;
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (canShoot && ammo > 0 )
            {
                StartCoroutine(ShootBullet(0.25f));
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            if (canLaunch && missile_catridge > 0)
            {
                StartCoroutine(LaunchMissile(1));
            }
        }



    }

    void ShootBullet()
    {

        GameObject newBullet = Instantiate(bullet, new Vector3(barrel.transform.position.x, barrel.transform.position.y, barrel.transform.position.z), Quaternion.identity);
        newBullet.transform.LookAt(barrel.transform.position+ barrel.transform.forward);
        Rigidbody rigidbody = newBullet.GetComponent<Rigidbody>();
        rigidbody.velocity = newBullet.transform.forward*100;
    }

    void LaunchMissile()
    {
        GameObject newMissile = Instantiate(missile, new Vector3(barrel.transform.position.x, barrel.transform.position.y, barrel.transform.position.z) , Quaternion.identity);
        newMissile.transform.LookAt(barrel.transform.position + barrel.transform.forward);
        Rigidbody rigidbody = newMissile.GetComponent<Rigidbody>();
        rigidbody.velocity = newMissile.transform.forward * 100;
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
