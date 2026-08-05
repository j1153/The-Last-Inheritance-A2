using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems; // 1. 务必引入事件系统命名空间

public class PasswordChecker : MonoBehaviour
{
    [Header("组件引用")]
    [SerializeField] private TMP_InputField passwordInput;

    [Header("密码配置")]
    [SerializeField] private string targetPassword = "1967";

    [Header("触发事件")]
    public UnityEvent OnPasswordCorrect;

    private void OnEnable()
    {
        if (passwordInput != null)
        {
            // 绑定监听
            passwordInput.onValueChanged.AddListener(CheckPassword);

            // 建议：每次打开界面时重置上次的密码
            passwordInput.text = "";

            // 2. 延迟一帧激活输入框（最稳妥的方式）
            StartCoroutine(FocusInputFieldNextFrame());
        }
    }

    private void OnDisable()
    {
        if (passwordInput != null)
        {
            passwordInput.onValueChanged.RemoveListener(CheckPassword);
        }
    }

    /// <summary>
    /// 延迟一帧让 EventSystem 和 UI 准备完毕后再聚焦
    /// </summary>
    private IEnumerator FocusInputFieldNextFrame()
    {
        // 等待当前帧渲染结束，避免 UI 刚 SetActive 时 EventSystem 还没刷新导致的聚焦失败
        yield return null;

        if (passwordInput != null)
        {
            // 将 UI 系统焦点设为当前输入框
            EventSystem.current?.SetSelectedGameObject(passwordInput.gameObject);

            // 调出闪烁光标（在移动端还会唤起软键盘）
            passwordInput.ActivateInputField();
        }
    }

    private void CheckPassword(string input)
    {
        if (input == targetPassword)
        {
            Debug.Log("密码正确！执行 UnityEvent 回调。");
            OnPasswordCorrect?.Invoke();
        }
    }
}