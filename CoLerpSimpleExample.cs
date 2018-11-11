using UnityEngine;

public class CoLerpSimpleExample : MonoBehaviour {
    
    public float time = 0.5f; // The time it takes for the cube to move to the target position
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // Creates a native AnimationCurve with ease in and out
    public Transform objectMoving; // Object that is being moved by the script

    private CoLerp coLerp;
    const float moveDistance = 3f;
    const float center = 0f;

    private void Start () {
        coLerp = new CoLerp(center); // Creates an instance of the CoLerp class with center as the initial value
    }

    void Update () {

        // Left input
        bool leftDown = Input.GetKeyDown(KeyCode.A);
        bool left = Input.GetKey(KeyCode.A);
        bool leftUp = Input.GetKeyUp(KeyCode.A);

        // Right input
        bool rightDown = Input.GetKeyDown(KeyCode.D);
        bool right = Input.GetKey(KeyCode.D);
        bool rightUp = Input.GetKeyUp(KeyCode.D);

        // Left: When left is first pressed
        if (leftDown == true) {
            coLerp.To(-moveDistance, time, curve); // Start interpolating towards -moveDistance
        }
    
        // Right: When right is first pressed
        if (rightDown == true) {
            coLerp.To(moveDistance, time, curve); // Start interpolating towards moveDistance
        }

        // Center: When left or right is released while both left and right isn't pressed
        if ((left == false && right == false) && (leftUp == true || rightUp == true)) {
            coLerp.To(center, time, curve); // Start interpolating towards center
        }

        // Changes the X position of the object using coLerp.Get(), which calculates the current interpolation value each time it's called
        objectMoving.position = new Vector3(coLerp.Get(), objectMoving.position.y, objectMoving.position.z);
	}
}
