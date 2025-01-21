using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    public float moveSpeed = 10.0f; 

    // Start is called before the first frame update
    void Start()
    {
        tr =  GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Debug.Log("h=" + h);
        Debug.Log("v=" + v);

        // Transform 컴포넌트의 position 속성값을 변경
        //transform.position += new Vector3(0, 0, 1);

        // 정규화 벡터를 사용한 코드
        //tr.position += Vector3.forward * 1;

        tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);
    }
}
