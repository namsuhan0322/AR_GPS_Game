using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] monsterPrefabs; // 여러 몬스터 배열
    public Transform arCameraTransform;

    // [핵심 설정] 몇 미터 걸을 때마다 나올지 설정 (예: 3.0 = 3미터)
    // 실내 테스트라면 1.5 ~ 2.0 정도로 줄여서 해보세요
    public float spawnDistance = 3.0f;

    // 마지막으로 스폰했던(또는 게임 시작한) 위치 저장용
    private Vector3 lastSpawnPos;

    void Start()
    {
        // 1. 카메라 자동 찾기
        if (arCameraTransform == null)
        {
            if (Camera.main != null) arCameraTransform = Camera.main.transform;
            else Debug.LogError(">>> [비상] 메인 카메라를 못 찾겠어요!");
        }

        // 2. 초기 위치 기억
        if (arCameraTransform != null)
        {
            lastSpawnPos = arCameraTransform.position;
        }
    }

    void Update()
    {
        if (arCameraTransform == null) return;

        // 3. 거리 계산 (현재 위치 vs 마지막 스폰 위치)
        float distance = Vector3.Distance(arCameraTransform.position, lastSpawnPos);

        // 4. 설정한 거리(예: 3m)보다 많이 이동했으면 스폰!
        if (distance >= spawnDistance)
        {
            // 거리 스폰
            SpawnRandomMonster(distance);

            // 기준 위치를 현재 위치로 갱신 (다시 0m부터 카운트)
            lastSpawnPos = arCameraTransform.position;
        }
    }

    // [추가됨] 버튼 클릭용 함수 (매개변수 없음)
    public void SpawnByButton()
    {
        Debug.Log(">>> [버튼 클릭] 몬스터 강제 소환!");

        // 거리는 0으로 처리해서 소환 함수 호출
        SpawnRandomMonster(0f);

        // (선택) 버튼으로 뽑았을 때도 거리 카운트를 초기화할지 결정
        // 만약 초기화하고 싶으면 아래 주석을 푸세요. (그러면 버튼 누르고 나서 다시 3m 걸어야 자동 소환됨)
        // if (arCameraTransform != null) lastSpawnPos = arCameraTransform.position;
    }

    // 실제 생성 로직
    public void SpawnRandomMonster(float movedDist)
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return;

        // 랜덤 몬스터 선택
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonster = monsterPrefabs[randomIndex];

        // 몬스터 위치: 내 위치에서 전방 랜덤 거리 (1.5 ~ 2.5m)
        float randomDist = Random.Range(1.5f, 2.5f);
        Vector3 spawnPos = arCameraTransform.position + (arCameraTransform.forward * randomDist);
        spawnPos.y -= 0.5f; // 높이 조절

        // 몬스터 생성
        GameObject mon = Instantiate(selectedMonster, spawnPos, Quaternion.identity);

        // 나를 보게 함
        mon.transform.LookAt(new Vector3(arCameraTransform.position.x, mon.transform.position.y, arCameraTransform.position.z));

        // 로그 출력 (버튼일 경우 0.0m로 뜸)
        Debug.Log($">>> {movedDist:F1}m 이동(또는 버튼)함! {selectedMonster.name} 등장!");
    }
}