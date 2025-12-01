using UnityEngine;

public class MarkerFollow : MonoBehaviour
{
    public Transform target; // 쫓아다닐 대상 (AR Camera)
    public float fixedY = 50f; // 빨간 점 높이 (건물보다 높게!)

    void LateUpdate()
    {
        if (target != null)
        {
            // 1. 타겟(카메라)의 위치값만 가져옴
            Vector3 targetPos = target.position;

            // 2. 높이는 우리가 정한 높이로 고정 (건물 위)
            targetPos.y = fixedY;

            // 3. 내 위치에 적용
            transform.position = targetPos;

            // 4. 회전은 무시! (항상 0,0,0으로 고정)
            transform.rotation = Quaternion.identity;
        }
    }
}