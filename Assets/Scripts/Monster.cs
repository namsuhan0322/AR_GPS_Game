using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    [Header("Settings")]
    public int maxHp = 100;
    public int goldReward = 50; // 잡으면 주는 돈

    private int currentHp;
    private Renderer monsterRenderer; // 몬스터의 렌더러 컴포넌트
    private Color originalColor;      // 원래 색상을 저장할 변수

    void Start()
    {
        currentHp = maxHp;
        monsterRenderer = GetComponent<Renderer>();
        if (monsterRenderer == null)
        {
            monsterRenderer = GetComponentInChildren<Renderer>();
        }

        if (monsterRenderer != null)
        {
            originalColor = monsterRenderer.material.color; // 원래 색상 저장
        }
        else
        {
            Debug.LogWarning("Monster 스크립트: Renderer 컴포넌트를 찾을 수 없습니다. 색상 변경이 동작하지 않습니다.");
        }
    }

    // 터치 매니저에서 호출할 함수
    public void OnClick(int damage)
    {
        currentHp -= damage;

        // 타격감 연출 (색상 변경)
        StopAllCoroutines(); // 기존 코루틴 중단 (이전 색상 변경 중이라면)
        StartCoroutine(HitAnimation());

        // 체력이 다 달면 사망
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddGold(goldReward);

        // 몬스터가 죽을 때 원래 색상으로 되돌리기 (선택 사항)
        if (monsterRenderer != null)
        {
            monsterRenderer.material.color = originalColor;
        }

        // 펑 터지는 이펙트가 있다면 여기서 Instantiate(effectPrefab, ...);
        Destroy(gameObject); // 몬스터 삭제
    }

    // 색상 변경 애니메이션 코루틴
    IEnumerator HitAnimation()
    {
        if (monsterRenderer != null)
        {
            monsterRenderer.material.color = Color.red; // 빨간색으로 변경
            yield return new WaitForSeconds(0.1f);      // 0.1초 대기
            monsterRenderer.material.color = originalColor; // 원래 색상으로 복귀
        }
        else
        {
            yield return null; // 렌더러가 없으면 아무것도 안 함
        }
    }
}