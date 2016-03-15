using UnityEngine;
using System.Collections;

public class JoystickController : MonoBehaviour {

    private static JoystickController _instance;

    public Joystick Left;
    public Joystick Right;

    public int InteractRange;
    
    private int _moveFingerId;
    private int _lookFingerId;

    void Awake() {
        _instance = this;
    }

    void Update() {
        
        int touchCount = Input.touchCount;
        
        if (touchCount == 0) {
            
            if (_moveFingerId > -1) {
                EndMoveTouch();
            }

            if (_lookFingerId > -1) {
                EndLookTouch();
            }
            return;
        }

        Touch t;

        for (int i = 0; i < touchCount; i++) {
            t = Input.GetTouch(i);

            switch (t.phase) {
                case TouchPhase.Began:

                    if (InMoveArea(t.position)) {
                        _moveFingerId = t.fingerId;
                        Left.UpdateController(t.position);
                    }

                    if (InLookArea(t.position)) {
                        _lookFingerId = t.fingerId;
                        Right.UpdateController(t.position);
                    }

                    break;
                case TouchPhase.Moved:

                    if (t.fingerId == _moveFingerId) {
                        Left.UpdateController(t.position);
                    }

                    if (t.fingerId == _lookFingerId) {
                        Right.UpdateController(t.position);
                    }
                    
                    break;
                case TouchPhase.Stationary:
                    break;
                default:

                    if (t.fingerId == _moveFingerId) {
                        EndMoveTouch();
                    } 
                    
                    if(t.fingerId == _lookFingerId){
                        EndLookTouch();
                    }

                    break;
            }

        }
    }

    private void EndMoveTouch() {
        _moveFingerId = -1;
        Left.Dispose();
    }

    private void EndLookTouch() {
        _lookFingerId = -1;
        Right.Dispose();
    }

    private bool InMoveArea(Vector3 position) {
        return Vector3.Distance(Left.transform.position, position) < InteractRange;
    }

    private bool InLookArea(Vector3 position) {
        return Vector3.Distance(Right.transform.position, position) < InteractRange;
    }

    private Vector3 Normalize(Vector3 position) {
        return position*(Screen.width/1920f);
    }

    public static JoystickController Instance {
        get { return _instance; }
    }
}
