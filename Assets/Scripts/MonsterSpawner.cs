using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] monsterPrefabs;
    public Transform arCameraTransform;

    [Header("Spawn Time Settings")]
    public float minSpawnTime;
    public float maxSpawnTime; 

    [Header("Debug UI")]
    public Text debugText;

    private float timer = 0f;
    private float nextSpawnTime = 0f;

    void Start()
    {
        if (arCameraTransform == null)
        {
            if (Camera.main != null) arCameraTransform = Camera.main.transform;
        }

        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (debugText != null)
        {
            float remainingTime = nextSpawnTime - timer;
            if (remainingTime < 0) remainingTime = 0;

            debugText.text = $"다음 몬스터까지: {remainingTime:F1}초\n" +
                             $"스폰 주기: {nextSpawnTime:F1}초";
        }

        if (timer >= nextSpawnTime)
        {
            SpawnRandomMonster();
            timer = 0f;
            SetNextSpawnTime(); 
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void SpawnByButton()
    {
        Debug.Log(">>> [버튼 클릭] 몬스터 강제 소환!");
        SpawnRandomMonster();
    }

    public void SpawnRandomMonster()
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return;
        if (arCameraTransform == null) return;

        // 랜덤 몬스터 선택
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonster = monsterPrefabs[randomIndex];

        // 플레이어 앞쪽 랜덤 위치 계산 (전방 1.5m ~ 2.5m)
        float randomDist = Random.Range(1.5f, 2.5f);
        Vector3 spawnPos = arCameraTransform.position + (arCameraTransform.forward * randomDist);

        spawnPos.y -= 0.5f;

        // 몬스터 생성
        GameObject mon = Instantiate(selectedMonster, spawnPos, Quaternion.identity);

        // 몬스터가 플레이어를 바라보게 회전 (Y축 기준)
        Vector3 lookPos = new Vector3(arCameraTransform.position.x, mon.transform.position.y, arCameraTransform.position.z);
        mon.transform.LookAt(lookPos);

        Debug.Log($">>> 몬스터 등장! ({selectedMonster.name})");
    }
}