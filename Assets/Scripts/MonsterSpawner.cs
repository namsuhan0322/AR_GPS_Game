using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹
    public Transform arCameraTransform; // XR Origin 안의 Main Camera 넣기

    // GPS 이벤트 등에서 호출할 함수
    public void SpawnMonsterInFront()
    {
        // 이미 몬스터가 있다면 중복 생성 방지 로직 필요

        // 카메라의 위치에서 앞쪽(Forward)으로 2m 떨어진 위치 계산
        Vector3 spawnPos = arCameraTransform.position + (arCameraTransform.forward * 2.0f);

        // 몬스터 높이를 카메라 높이와 맞출지, 바닥(y=0)으로 내릴지 결정
        // 여기서는 간단하게 카메라 높이보다 약간 아래(-0.5m)에 생성
        spawnPos.y -= 0.5f;

        // 몬스터 생성
        GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

        // 몬스터가 플레이어(카메라)를 바라보게 회전
        monster.transform.LookAt(new Vector3(arCameraTransform.position.x, monster.transform.position.y, arCameraTransform.position.z));
    }
}