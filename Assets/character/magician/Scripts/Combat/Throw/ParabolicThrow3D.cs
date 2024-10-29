using UnityEngine;

public class ParabolicThrow3D : MonoBehaviour
{
    public Vector3 throwPosition;  // Vị trí ném ban đầu
    public Vector3 throwVelocity;  // Vận tốc ném ban đầu
    public float gravity = 9.8f;   // Gia tốc trọng trường
    public int trajectoryResolution = 30;  // Số điểm của quỹ đạo để vẽ
    private Vector3[] trajectoryPoints;  // Các điểm quỹ đạo

    void Start()
    {
        CalculateTrajectory();
    }

    void CalculateTrajectory()
    {
        float totalTime = CalculateTotalFlightTime();
        trajectoryPoints = new Vector3[trajectoryResolution];

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float t = (totalTime / trajectoryResolution) * i;
            Vector3 point = CalculatePositionAtTime(t);
            trajectoryPoints[i] = point;
        }
    }

    float CalculateTotalFlightTime()
    {
        // Giải phương trình bậc hai cho trục Y để tìm thời gian rơi
        float v_y0 = throwVelocity.y;
        float discriminant = v_y0 * v_y0 + 2 * gravity * throwPosition.y;
        return (v_y0 + Mathf.Sqrt(discriminant)) / gravity;
    }

    Vector3 CalculatePositionAtTime(float t)
    {
        float x = throwPosition.x + throwVelocity.x * t;
        float y = throwPosition.y + throwVelocity.y * t - 0.5f * gravity * t * t;
        float z = throwPosition.z + throwVelocity.z * t;
        return new Vector3(x, y, z);
    }

    void OnDrawGizmos()
    {
        // Vẽ các điểm quỹ đạo ra Scene
        if (trajectoryPoints == null) return;

        Gizmos.color = Color.red;

        for (int i = 0; i < trajectoryPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(trajectoryPoints[i], trajectoryPoints[i + 1]);
        }
    }
}
