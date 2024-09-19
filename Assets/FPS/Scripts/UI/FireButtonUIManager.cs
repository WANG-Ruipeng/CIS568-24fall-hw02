using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Gameplay;
using UnityEngine.EventSystems;

public class FireButtonUIManager : MonoBehaviour
{
    [SerializeField]
    public Button fireButton;
    public PlayerInputHandler m_InputHandler;

    void Start()
    {
        if (fireButton == null)
        {
            Debug.LogError("Fire button is not assigned in the inspector!");
            return;
        }

        if (m_InputHandler == null)
        {
            Debug.LogError("PlayerInputHandler is not assigned in the inspector!");
            return;
        }

        EventTrigger trigger = fireButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) => { OnFireButtonDown(); });
        trigger.triggers.Add(pointerDown);

        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((data) => { OnFireButtonUp(); });
        trigger.triggers.Add(pointerUp);
    }

    void OnFireButtonDown()
    {
        m_InputHandler.isTouchingFireButton = true;
    }

    void OnFireButtonUp()
    {
        m_InputHandler.isTouchingFireButton = false;
    }
}