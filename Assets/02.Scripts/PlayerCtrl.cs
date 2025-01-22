using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    // 이동 속도 변수
    public float moveSpeed = 10.0f; 
    
    public float turnSpeed = 80.0f;

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
        float r = Input.GetAxis("Mouse X");

        // Debug.Log("h=" + h);
        // Debug.Log("v=" + v);

        // Transform 컴포넌트의 position 속성값을 변경
        //transform.position += new Vector3(0, 0, 1);

        // 정규화 벡터를 사용한 코드
        //tr.position += Vector3.forward * 1;


        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir.normalized * Time.deltaTime  * moveSpeed);
        
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
    }
}
