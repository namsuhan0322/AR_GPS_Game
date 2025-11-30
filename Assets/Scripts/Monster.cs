using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    [Header("Settings")]
    public int maxHp = 100;
    public int goldReward = 50; // 잡으면 주는 돈

    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    // 터치 매니저에서 호출할 함수
    public void OnClick(int damage)
    {
        currentHp -= damage;

        // 타격감 연출 (잠깐 커졌다 돌아오기)
        StopAllCoroutines();
        StartCoroutine(HitAnimation());

        // 체력이 다 달면 사망
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // GameManager에게 골드 추가 요청 (싱글톤)
        GameManager.Instance.AddGold(goldReward);

        // 펑 터지는 이펙트가 있다면 여기서 Instantiate(effectPrefab, ...);

        Destroy(gameObject); // 몬스터 삭제
    }

    // 간단한 타격 연출 (Scale 튕김)
    IEnumerator HitAnimation()
    {
        transform.localScale = Vector3.one * 1.2f; // 1.2배 커짐
        yield return new WaitForSeconds(0.1f);
        transform.localScale = Vector3.one;        // 원상복구
    }
}