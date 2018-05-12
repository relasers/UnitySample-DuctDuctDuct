using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour {

    public float speed = 150.0f;
    public bool isAlive = true;

    public Camera camera;

    public Quaternion Initalized_rotation;
    public GameObject bullet;

    bool canShoot = true;

    IEnumerator ShootBullet(float delay)
    {
        ShootBullet();

        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
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
            Debug.Log(hit.GetType());
           // transform.LookAt(hit.point);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (canShoot)
            {
                StartCoroutine(ShootBullet(1));
            }
        }

        

    }

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * 20, Quaternion.identity);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
      
        // 벽에 부딫힌 상태면 색상을 변경하자.
        if (collision.gameObject.tag == "SolidBlock")
        {
            rigidbody.isKinematic = true;
            Material material = GetComponent<MeshRenderer>().material;
            
            GetComponent<MeshRenderer>().material.color
                = new Color(1.0f,0.0f,0.0f,1.0f);
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


            // 접촉 지점을 찾아낸다.
            ContactPoint contact;

            if (collision.contacts.Length > 0)
            {
                contact = collision.contacts[0];

                transform.position = contact.point + contact.normal * Time.deltaTime;

                //rigidbody.AddForce(contact.normal  * 2);
                
            }


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
