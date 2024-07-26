using UnityEngine;

public class PIDGyroSimulation : MonoBehaviour
{
    public GameObject targetVertex;  // Указатель на трансформ угла, который должен оказаться вверху

    // PID параметры для первой оси
    public float kp1 = 10.0f;
    public float ki1 = 0.1f;
    public float kd1 = 5.0f;

    // PID параметры для второй оси
    public float kp2 = 10.0f;
    public float ki2 = 0.1f;
    public float kd2 = 5.0f;

    // Флаги для дублирования значений PID
    public bool duplicateKp = true;
    public bool duplicateKi = true;
    public bool duplicateKd = true;

    public float normalDamping = 0.1f;  // Нормальное демпфирование
    public float highDamping = 0.5f;  // Высокое демпфирование при быстром вращении
    public float speedThreshold = 1.0f;  // Порог скорости для переключения режимов демпфирования

    private Rigidbody rb;
    private float integral1, integral2;
    private float lastError1, lastError2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularDamping = normalDamping;
        PlaceVertexObject(transform, targetVertex);
    }

    void FixedUpdate()
    {
        // Дублирование значений PID
        if (duplicateKp) kp2 = kp1;
        if (duplicateKi) ki2 = ki1;
        if (duplicateKd) kd2 = kd1;

        Vector3 targetDirection = (targetVertex.transform.position - transform.position).normalized;
        Vector3 currentUp = transform.up;
        float error1 = Vector3.Angle(currentUp, targetDirection);

        // Расчет PID для первой оси
        integral1 += error1 * Time.fixedDeltaTime;
        float derivative1 = (error1 - lastError1) / Time.fixedDeltaTime;
        float output1 = kp1 * error1 + ki1 * integral1 + kd1 * derivative1;
        lastError1 = error1;

        Vector3 correctionTorque1 = Vector3.Cross(currentUp, targetDirection) * output1;
        rb.AddTorque(correctionTorque1);

        // Дополнительный крутящий момент от внутреннего гироскопа по второй оси
        Vector3 targetRight = Vector3.Cross(targetDirection, Vector3.up).normalized;
        Vector3 currentForward = transform.forward;
        float error2 = Vector3.Angle(currentForward, targetRight);

        // Расчет PID для второй оси
        integral2 += error2 * Time.fixedDeltaTime;
        float derivative2 = (error2 - lastError2) / Time.fixedDeltaTime;
        float output2 = kp2 * error2 + ki2 * integral2 + kd2 * derivative2;
        lastError2 = error2;

        Vector3 correctionTorque2 = Vector3.Cross(currentForward, targetRight) * output2;
        rb.AddTorque(correctionTorque2);

        // Адаптивное демпфирование
        rb.angularDamping = CalculateDamping(rb.angularVelocity.magnitude);
    }

    float CalculateDamping(float currentAngularSpeed)
    {
        return currentAngularSpeed > speedThreshold ? highDamping : normalDamping;
    }

    void PlaceVertexObject(Transform cubeTransform, GameObject vertexObject)
    {
        float size = cubeTransform.localScale.x;
        float halfSize = size / 2;
        vertexObject.transform.position = cubeTransform.position + new Vector3(halfSize, halfSize, halfSize);
        vertexObject.transform.parent = cubeTransform;
    }
}
