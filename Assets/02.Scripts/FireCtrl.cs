using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 반드시 필요한 컴포넌트를 삭제 안되도록 하는 어트리뷰트
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // 총알 프리펩
    public GameObject bullet;
    // 총알 발사 좌표
    public Transform firePos;
    // 총소리 음원
    public AudioClip fireSfx;

    // 오디오 컴포넌트
    private new AudioSource audio;
    private MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        // 처음 시작할 때 비활성화
        muzzleFlash.enabled = false;
    }
    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭 했을 때 Fire 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        } 
    }
    void Fire()
    {   
        Instantiate(bullet, firePos.position, firePos.rotation);
        // 총소리 발생
        audio.PlayOneShot(fireSfx, 1.0f);
        // 총구 화염 효과 코루틴 함수 호출
        StartCoroutine(ShowMuzzleFlash());
        //StartCoroutine("ShowMuzzleFlash");
    }

    IEnumerator ShowMuzzleFlash()
    {   
        // MuzzleFlash 활성화
        muzzleFlash.enabled = true;
        // 0.2초동안 대기(제어권 양보)
        yield return new WaitForSeconds(0.2f);

        // MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }
}
