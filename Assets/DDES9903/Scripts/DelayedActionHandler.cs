using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedActionHandler : MonoBehaviour
{
    [Header("设置")]
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private bool autoExecuteOnStart = true;

    [Header("回调事件")]
    public UnityEvent onActionExecuted;

    private Coroutine activeTimer;

    void Start()
    {
        if (autoExecuteOnStart)
        {
            BeginTimer();
        }
    }

    public void BeginTimer()
    {
        if (activeTimer != null)
        {
            StopCoroutine(activeTimer);
        }

        activeTimer = StartCoroutine(ExecuteAfterDelay());
    }

    private IEnumerator ExecuteAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        onActionExecuted?.Invoke();

        activeTimer = null;
    }

    public void CancelTimer()
    {
        if (activeTimer != null)
        {
            StopCoroutine(activeTimer);
            activeTimer = null;
        }
    }
}