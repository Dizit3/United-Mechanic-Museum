using UnityEngine;

public class PIDGyroSimulation : MonoBehaviour
{
    public GameObject targetVertex;  // Целевая вершина
    public float kp = 10.0f;  // Пропорциональный коэффициент
    public float ki = 0.1f;   // Интегральный коэффициент
    public float kd = 5.0f;   // Дифференциальный коэффициент

    private Rigidbody rb;
    private float integral;
    private float lastError;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlaceVertexObject(transform, targetVertex);
    }

    void FixedUpdate()
    {
        Vector3 targetDirection = (targetVertex.transform.position - transform.position).normalized;
        Vector3 currentUp = transform.up;
        float error = Vector3.Angle(currentUp, targetDirection);

        // Расчёт PID
        integral += error * Time.fixedDeltaTime;
        float derivative = (error - lastError) / Time.fixedDeltaTime;
        float output = kp * error + ki * integral + kd * derivative;
        lastError = error;

        Vector3 correctionTorque = Vector3.Cross(currentUp, targetDirection) * output;
        rb.AddTorque(correctionTorque);
    }

    void PlaceVertexObject(Transform cubeTransform, GameObject vertexObject)
    {
        float size = cubeTransform.localScale.x;
        float halfSize = size / 2;
        vertexObject.transform.position = cubeTransform.position + new Vector3(halfSize, halfSize, halfSize);
        vertexObject.transform.parent = cubeTransform;
    }
}
