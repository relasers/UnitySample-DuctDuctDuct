using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorriderWallBuilder : MonoBehaviour {

    Coroutine wallMaker;
    public GameObject wall;
    public GameObject corridor_light;

    public int CorriderStartPos = -500;
    public int CorriderSize = 1000;
    public int WallWidth = 50;
    public int WallHeight = 50;
    public float CreateDelay = 0.5f;

	// Use this for initialization
	void Start () {

        // 딜레이 시간만큼 지연시키며 하나하나 솟아올린다.
        wallMaker = StartCoroutine(BuildWall( CreateDelay ));

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator BuildWall(float waitTime)
    {

        GameObject NewWall;
        GameObject NewLight;

        float LeftSidePosition = -WallHeight;
        float RightSidePosition = WallHeight;

        float Light_LeftSidePosition = -WallHeight * 0.75f;
        float Light_RightSidePosition = WallHeight * 0.75f;

        float WallCenterPosition = WallHeight * 0.5f;

        for (int i = 0; i <= CorriderSize / WallWidth; ++i)
        {

            float CreatePosition_z = CorriderStartPos + i * WallWidth;

            NewWall = Instantiate(wall,
                new Vector3(LeftSidePosition, -WallHeight, CreatePosition_z),
                Quaternion.identity);
            NewWall.transform.Rotate(Vector3.up, 90);
            NewWall.name = "Wall";

            NewLight = Instantiate(corridor_light,
               new Vector3(Light_LeftSidePosition, WallCenterPosition, CreatePosition_z),
                Quaternion.identity);


            NewWall = Instantiate(wall,
                 new Vector3(RightSidePosition, -WallHeight, CreatePosition_z),
                Quaternion.identity);
            NewWall.transform.Rotate(Vector3.up, 90);
            NewWall.name = "Wall";


            NewLight = Instantiate(corridor_light,
               new Vector3(Light_RightSidePosition, WallCenterPosition, CreatePosition_z),
                Quaternion.identity);


            yield return new WaitForSeconds(CreateDelay);
        }

        NewWall = Instantiate(wall,
               new Vector3( 0, -WallHeight, 500),
               Quaternion.identity);

        // y축 90도 돌린다.
       // 
        NewWall.transform.localScale = new Vector3(100,50,10);
    }

}
