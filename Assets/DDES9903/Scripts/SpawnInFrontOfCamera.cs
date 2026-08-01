using UnityEngine;

public class SpawnInFrontOfCamera : MonoBehaviour
{
    [Header("位置设置")]
    [Tooltip("距离相机的正前方水平距离（米）")]
    public float distanceInFront = 2.0f;

    [Tooltip("放置的绝对世界高度/Y轴高度（米）")]
    public float worldHeight = 1.3f;

    [Header("朝向设置")]
    [Tooltip("是否只沿Y轴旋转面向相机（保持物体垂直不倾斜）")]
    public bool lockVerticalRotation = true;

    private void OnEnable()
    {
        // 1. 获取第一人称相机（优先 Camera.main）
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogWarning("[SpawnInFrontOfCamera] 未找到 Tag 为 MainCamera 的相机！");
            return;
        }

        Transform camTransform = mainCam.transform;

        // 2. 计算相机视线的水平正前方方向（忽略相机的俯仰角，防止物体斜着偏向地面或天空）
        Vector3 forwardDir = camTransform.forward;
        forwardDir.y = 0f;
        forwardDir.Normalize();

        // 如果玩家垂直抬头/俯视导致水平方向接近零，退回使用相机原始前向
        if (forwardDir == Vector3.zero)
        {
            forwardDir = Vector3.ProjectOnPlane(camTransform.forward, Vector3.up).normalized;
        }

        // 3. 设置放置位置：相机的 X/Z 位置 + 正前方距离，Y 高度固定为指定高度（如 1.3m）
        Vector3 spawnPosition = camTransform.position + forwardDir * distanceInFront;
        spawnPosition.y = worldHeight;
        transform.position = spawnPosition;

        // 4. 设置朝向：看向相机
        Vector3 lookTarget = camTransform.position;
        if (lockVerticalRotation)
        {
            // 将目标高度设为与物体同高，确保物体只在水平方向旋转，不会上下倾斜
            lookTarget.y = transform.position.y;
        }

        transform.LookAt(lookTarget);
    }
}