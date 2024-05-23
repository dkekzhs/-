using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public GameObject[] backgrounds, grounds; // 백그라운드 이미지들을 배열로 설정
    private float backgroundWidth,groundWidth;

    void Start()
    {
        if (backgrounds.Length < 2 || grounds.Length < 2)
        {
            Debug.LogError("At least two backgrounds are required for infinite scrolling.");
            return;
        }

        // 백그라운드 이미지의 폭 계산
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        groundWidth = grounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // 백그라운드 스크롤
        ScrollObjects(backgrounds, backgroundWidth);
        // 바닥 스크롤
        ScrollObjects(grounds, groundWidth);
    }

    void ScrollObjects(GameObject[] objects, float objectWidth)
    {
        foreach (GameObject obj in objects)
        {
            // 오브젝트를 왼쪽으로 스크롤
            obj.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

            // 오브젝트가 화면 왼쪽 밖으로 나가면 오른쪽 끝으로 이동
            if (obj.transform.position.x <= -objectWidth)
            {
                RepositionObject(obj, objects, objectWidth);
            }
        }
    }

    void RepositionObject(GameObject obj, GameObject[] objects, float objectWidth)
    {
        // 가장 오른쪽에 있는 오브젝트 찾기
        float rightmostX = -objectWidth;
        foreach (GameObject objInArray in objects)
        {
            if (objInArray.transform.position.x > rightmostX)
            {
                rightmostX = objInArray.transform.position.x;
            }
        }

        // 오른쪽 끝에 있는 오브젝트의 오른쪽으로 이동
        obj.transform.position = new Vector3(rightmostX + objectWidth, obj.transform.position.y, obj.transform.position.z);
    }
}
