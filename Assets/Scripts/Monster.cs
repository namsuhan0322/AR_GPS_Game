using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    [Header("Settings")]
    public int maxHp = 100;
    public int goldReward = 50;

    private int currentHp;
    private Renderer monsterRenderer;
    private Color originalColor;
    private Animator _anim;
    private Collider _collider; // 중복 클릭 방지용
    private bool isDead = false; // 사망 상태 체크

    void Start()
    {
        currentHp = maxHp;
        _collider = GetComponent<Collider>();
        _anim = GetComponent<Animator>();
        monsterRenderer = GetComponentInChildren<Renderer>();

        if (monsterRenderer != null)
        {
            originalColor = monsterRenderer.material.color;
        }

        if (_anim == null)
        {
            Debug.LogError("Monster 스크립트: Animator를 찾을 수 없습니다!");
        }
    }

    public void OnClick(int damage)
    {
        if (isDead) return; // 이미 죽었다면 무시

        currentHp -= damage;

        // 체력이 0 이하인지 먼저 확인하여 로직을 분리합니다.
        if (currentHp <= 0)
        {
            StopCoroutine("HitAnimation");

            // 바로 사망 처리
            StartCoroutine(DieAnimation());
        }
        else
        {
            StopCoroutine("HitAnimation");
            StartCoroutine(HitAnimation());
        }
    }

    IEnumerator DieAnimation()
    {
        if (monsterRenderer != null && _anim != null)
        {
            isDead = true;
            _collider.enabled = false;

            GameManager.Instance.AddGold(goldReward);

            if (monsterRenderer != null)
            {
                monsterRenderer.material.color = originalColor;
            }

            if (isDead)
                _anim.SetTrigger("Die");

            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }

    IEnumerator HitAnimation()
    {
        if (monsterRenderer != null && _anim != null)
        {
            _anim.SetTrigger("Hit");
            EffectManager.Instance.PlayEffect("Hit", this.transform.position, Quaternion.identity);

            monsterRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            monsterRenderer.material.color = originalColor;
        }
    }
}