using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotater : MonoBehaviour {

    public CubeColorType ColorType;

    Vector3 MovingVector;
    GameObject Player;
    public float MovingSpeed = 50.0f;
    public float RotateSpeed = 50.0f;
    
    // 이 시간 이후 플레이어를 추적하기 시작
    public float ChaseStartingTime = 5.0f;
    
    // 이게 true값이 되는 순간 플레이어를 향해 날아간다.
    bool ChaseSwitch = false;

    Coroutine coroutine_startChaserSwitch;

    IEnumerator PlayerChaserSwitchOn(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChaseSwitch = true;
    }


    // 인스턴스 생성 시점에서 바로 실행되어야하는 스크립트의 경우
    // AWAKE를 사용해야한다.
    void Awake () {

        //////////////////////////////////////////////////////////////////////////////////


        ColorType = (CubeColorType)Random.Range((int)CubeColorType.BLUE, (int)CubeColorType.COUNT);

        // 색상 결정
        SetCubeColor();

        //////////////////////////////////////////////////////////////////////////////////

        Vector3 RandomVector = new Vector3(
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f)
            );

        // 큐브의 초기 이동 방향은 위에서 랜덤으로 
        MovingVector = Vector3.Normalize(RandomVector);



    }

    private void Start()
    {
        // 2초뒤에 쫓아갈 수 있도록
        coroutine_startChaserSwitch = StartCoroutine(PlayerChaserSwitchOn(ChaseStartingTime));
        Player = GameObject.FindWithTag("Player");

    }

    // 물리 관련 처리는 FIxed Update에서!!
    private void FixedUpdate()
    {

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (ChaseSwitch && Player && Player.GetComponent<SpaceShipController>().isAlive)
        {

            Vector3 TargetVector = Vector3.Normalize(Player.transform.position - transform.position);

            rigidbody.velocity = (TargetVector * MovingSpeed * Time.fixedDeltaTime * rigidbody.mass);

            //transform.position = transform.position + MovingVector * MovingSpeed * Time.deltaTime;

        }
        else
        {
            rigidbody.velocity = (MovingVector * MovingSpeed * Time.fixedDeltaTime * rigidbody.mass);
        }

        transform.Rotate(new Vector3(0, 1, 0), RotateSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update () {
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (collision.gameObject.tag  == "SolidBlock" )
        {
            

            // 접촉 지점을 찾아낸다.
            ContactPoint contact;

            if (collision.contacts.Length > 0)
            {
                contact = collision.contacts[0];

                MovingVector = Vector3.Reflect(MovingVector, contact.normal);
                rigidbody.velocity = (MovingVector * Time.deltaTime * rigidbody.mass );
            }

        }

    }

    private void OnCollisionStay(Collision collision)
    {

      //  Rigidbody rigidbody = GetComponent<Rigidbody>();
      //
      //  if (collision.gameObject.tag == "SolidBlock")
      //  {
      //      rigidbody.velocity = MovingVector * Time.deltaTime * rigidbody.mass;
      //
      //  }
    }

    // 큐브의 색상을 결정한다.
    public void SetCubeColor()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        switch (ColorType)
        {
            case CubeColorType.RED:
                {
                    renderer.material.color =
                        new Color(1f, 0.0f, 0.0f, 1);
                    break;
                }
            case CubeColorType.BLUE:
                {
                    renderer.material.color =
                        new Color(0.0f, 0.0f, 1f, 1);
                    break;
                }
            case CubeColorType.GREEN:
                {
                    renderer.material.color =
                       new Color(0.0f, 1, 0.0f, 1);
                    break;
                }
            case CubeColorType.PINK:
                {
                    renderer.material.color =
                       new Color(1, 0.75f, 1, 0.79f);
                    break;
                }
            default:
                break;
        }

    }

}
