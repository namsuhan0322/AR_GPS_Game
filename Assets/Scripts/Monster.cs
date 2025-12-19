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
    private Collider _collider;
    private bool isDead = false;

    // 사운드 이름 변수
    private string hitSoundName;
    private string dieSoundName;

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

        if (gameObject.CompareTag("Slime"))
        {
            // 태그가 Slime인 경우
            hitSoundName = "Slime_Hit";
            dieSoundName = "Slime_Die";
        }
        else if (gameObject.CompareTag("Monster"))
        {
            // 태그가 Monster인 경우
            hitSoundName = "Monster_Hit";
            dieSoundName = "Monster_Die";
        }
        else
        {
            hitSoundName = "Monster_Hit";
            dieSoundName = "Monster_Die";
        }
    }

    public void OnClick(int damage)
    {
        if (isDead) return;

        currentHp -= damage;

        if (currentHp <= 0)
        {
            StopCoroutine("HitAnimation");
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

            // 설정된 사망 사운드 재생
            SoundManager.instance.PlaySound(dieSoundName);

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

            // 설정된 피격 사운드 재생
            SoundManager.instance.PlaySound(hitSoundName);

            // 이펙트 재생
            if (EffectManager.Instance != null)
                EffectManager.Instance.PlayEffect("Hit", this.transform.position, Quaternion.identity);

            monsterRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            monsterRenderer.material.color = originalColor;
        }
    }
}