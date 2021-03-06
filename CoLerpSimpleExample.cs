using UnityEngine;

public class CoLerpSimpleExample : MonoBehaviour {
    
    [Tooltip("The time it takes for the cube to move to the target position")]
    public float time = 0.5f;
    [Tooltip("The interpolation curve used to change the way the cube moves between positions")]
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Creates a native AnimationCurve with ease in and out
    [Tooltip("Object that is being moved by the script")]
    public Transform objectMoving;

    private CoLerp coLerp;
    const float moveDistance = 3f;
    const float center = 0f;

    private void Start () {
        coLerp = new CoLerp(center); // Creates an instance of the CoLerp class with center as the initial value
    }

    private void Update () {

        // Input
        if (Input.GetKey(KeyCode.A) == true) {
            coLerp.To(-moveDistance, time, curve); // Start interpolating towards -moveDistance
        } else if (Input.GetKey(KeyCode.D) == true) {
            coLerp.To(moveDistance, time, curve); // Start interpolating towards moveDistance
        } else {
            coLerp.To(center, time, curve); // Start interpolating towards center
        }

        // Changes the X position of the object using coLerp.Get(), which calculates the current interpolation value each time it's called
        objectMoving.position = new Vector3(coLerp.Get(), objectMoving.position.y, objectMoving.position.z);
	}
}
