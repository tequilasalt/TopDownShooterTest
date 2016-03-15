using UnityEngine;
using System.Collections;

public class AvatarController : MonoBehaviour {

    private Animator _animator;
    private Rigidbody _rigidbody;
    
    void Start() {

        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();

    }

    void Update() {
        Controls();
    }

    private void Controls() {

        Debug.Log(JoystickController.Instance.Right.Angle);

        if (JoystickController.Instance.Right.Angle != 0 && JoystickController.Instance.Left.Angle == 0) {
            transform.localEulerAngles = new Vector3(0, JoystickController.Instance.Right.Angle, 0);
        }


        /*
        if (JoystickController.Instance.Left.Angle != 0 && JoystickController.Instance.Right.Angle == 0) {
            
        }*/


        /*
        Vector2 moveAxis = Joystick.GetJoystick(Joystick.LEFT).Axis;
        Vector2 lookAxis = Joystick.GetJoystick(Joystick.RIGHT).Axis;
        */
    }

}
