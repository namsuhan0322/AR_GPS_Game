using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class ARTouchManager : MonoBehaviour
{
    public Camera arCamera;
    public int clickDamage = 10;

    void Update()
    {
        // 터치 또는 마우스 클릭 감지
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI()) return;

            Vector2 touchPos;

#if UNITY_EDITOR
            touchPos = Input.mousePosition;
#else
            if (Input.touchCount > 0) touchPos = Input.GetTouch(0).position;
            else return;
#endif
            ShootRay(touchPos);
        }
    }

    bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return true;
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return true;

        return false;
    }

    void ShootRay(Vector2 screenPos)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Monster monster = hit.collider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.OnClick(clickDamage);
            }
        }
    }
}