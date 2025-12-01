using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform playerTarget; // 플레이어(또는 AR Camera)
    public float height = 100f; // 카메라 높이

    void LateUpdate()
    {
        if (playerTarget != null)
        {
            // 플레이어의 X, Z 위치만 따라가고, 높이(Y)는 고정
            Vector3 newPos = playerTarget.position;
            newPos.y = height;
            transform.position = newPos;

            // (선택사항) 지도가 회전하게 하려면 아래 주석 해제
            // transform.rotation = Quaternion.Euler(90f, playerTarget.eulerAngles.y, 0f);
        }
    }
}