using System;
using UnityEngine;
using UnityEngine.Events;

public class CountDoor : MonoBehaviour
{
    // 记录当前的交互次数
    private int currentInteractCount = 0;

    [Header("设置")]
    public UnityEvent OnFirstInteract;
    public UnityEvent OnSecondInteract;
    public UnityEvent OnThirdInteract;

    /// <summary>
    /// 通用交互入口（挂载在 InteractableGeneral 上的点击回调）
    /// </summary>
    public void OnInteractGeneral()
    {
        currentInteractCount++;

        // 把当前次数传给事件分发函数
        TriggerDoorEvent(currentInteractCount);
    }

    /// <summary>
    /// 根据传入的次数，触发对应的事件
    /// </summary>
    /// <param name="count">当前的交互次数</param>
    public void TriggerDoorEvent(int count)
    {
        switch (count)
        {
            case 1:
                Debug.Log("触发：第一次开门事件");
                OnFirstInteract?.Invoke();
                break;

            case 2:
                Debug.Log("触发：第二次开门事件");
                OnSecondInteract?.Invoke();
                break;

            case 3:
                Debug.Log("触发：第三次开门事件");
                OnThirdInteract?.Invoke();
                break;

            default:
                // 超出 3 次后的处理
                    Debug.Log($"触发：超过3次，重复执行第三次开门事件 (第 {count} 次)");
                    OnThirdInteract?.Invoke();
                break;
        }
    }

    // 重置交互次数（如果需要复位门的状态可以调用）
    public void ResetInteractCount()
    {
        currentInteractCount = 0;
    }
}