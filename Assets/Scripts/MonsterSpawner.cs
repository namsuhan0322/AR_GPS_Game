using UnityEngine;
using UnityEngine.UI; // [필수] UI 텍스트를 쓰기 위해 추가

public class MonsterSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] monsterPrefabs;
    public Transform arCameraTransform;

    public float spawnDistance = 3.0f;

    [Header("Debug UI")]
    public Text distanceText; // [추가됨] 화면에 거리를 보여줄 텍스트 (인스펙터 연결)

    private Vector3 lastSpawnPos;

    void Start()
    {
        if (arCameraTransform == null)
        {
            if (Camera.main != null) arCameraTransform = Camera.main.transform;
            else Debug.LogError(">>> [비상] 메인 카메라를 못 찾겠어요!");
        }

        if (arCameraTransform != null)
        {
            lastSpawnPos = arCameraTransform.position;
        }
    }

    void Update()
    {
        if (arCameraTransform == null) return;

        // 1. 거리 계산
        float distance = Vector3.Distance(arCameraTransform.position, lastSpawnPos);

        // [추가됨] 2. 화면에 실시간 거리 표시 (예: "이동: 1.25m / 3.0m")
        if (distanceText != null)
        {
            distanceText.text = $"이동: {distance:F2}m / {spawnDistance:F1}m";
        }

        // (선택사항) 콘솔창에도 띄우고 싶다면 주석 해제 (너무 빨라서 비추천)
        // Debug.Log($"현재 거리: {distance}m");

        // 3. 스폰 체크
        if (distance >= spawnDistance)
        {
            SpawnRandomMonster(distance);
            lastSpawnPos = arCameraTransform.position;
        }
    }

    public void SpawnByButton()
    {
        Debug.Log(">>> [버튼 클릭] 몬스터 강제 소환!");
        SpawnRandomMonster(0f);
        // 버튼 눌렀을 때도 거리 초기화하고 싶으면 아래 주석 해제
        // if (arCameraTransform != null) lastSpawnPos = arCameraTransform.position;
    }

    public void SpawnRandomMonster(float movedDist)
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonster = monsterPrefabs[randomIndex];

        float randomDist = Random.Range(1.5f, 2.5f);
        Vector3 spawnPos = arCameraTransform.position + (arCameraTransform.forward * randomDist);
        spawnPos.y -= 0.5f;

        GameObject mon = Instantiate(selectedMonster, spawnPos, Quaternion.identity);

        mon.transform.LookAt(new Vector3(arCameraTransform.position.x, mon.transform.position.y, arCameraTransform.position.z));

        // 몬스터 크기 랜덤 (0.8 ~ 1.2배)
        mon.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);

        Debug.Log($">>> {movedDist:F1}m 이동함! {selectedMonster.name} 등장!");
    }
}