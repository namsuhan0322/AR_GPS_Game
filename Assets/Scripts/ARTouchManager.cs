using UnityEngine;
using UnityEngine.XR.ARFoundation; // AR 관련 네임스페이스

public class ARTouchManager : MonoBehaviour
{
    public Camera arCamera; // Inspector에서 AR Camera 연결
    public int clickDamage = 10;

    void Update()
    {
        // 터치 또는 마우스 클릭 감지
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos;

            // 모바일 터치 좌표 vs 에디터 마우스 좌표 분기 처리
#if UNITY_EDITOR
            touchPos = Input.mousePosition;
#else
            if (Input.touchCount > 0) touchPos = Input.GetTouch(0).position;
            else return;
#endif
            ShootRay(touchPos);
        }
    }

    void ShootRay(Vector2 screenPos)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        // 레이저 발사!
        if (Physics.Raycast(ray, out hit))
        {
            // 맞은 녀석이 몬스터라면?
            Monster monster = hit.collider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.OnClick(clickDamage);

                // (선택) 여기에 터치 이펙트 생성 코드 추가 가능
                // Instantiate(touchEffect, hit.point, Quaternion.identity);
            }
        }
    }
}