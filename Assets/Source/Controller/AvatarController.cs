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

        if (JoystickController.Instance.Right.Angle != 0 && JoystickController.Instance.Left.Angle == 0) {

            transform.localEulerAngles = new Vector3(0, JoystickController.Instance.Right.Angle, 0);

            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("moving", false);

        } else if (JoystickController.Instance.Left.Angle != 0 && JoystickController.Instance.Right.Angle == 0) {

            transform.localEulerAngles = new Vector3(0, JoystickController.Instance.Left.Angle, 0);

            Vector3 velocity = new Vector3();

            velocity += transform.forward*JoystickController.Instance.Left.Axis.magnitude;
            //velocity += transform.right*JoystickController.Instance.Left.Axis.x;

            velocity *= 5;

            _rigidbody.velocity = velocity;
            
            _animator.SetBool("moving", true);
            _animator.SetFloat("angle", 0);

        }else if (JoystickController.Instance.Left.Angle != 0 && JoystickController.Instance.Right.Angle != 0) {

            transform.localEulerAngles = new Vector3(0, JoystickController.Instance.Right.Angle, 0);

            Vector3 velocity = new Vector3();

            velocity += Vector3.forward*JoystickController.Instance.Left.Axis.y;
            velocity += Vector3.right*JoystickController.Instance.Left.Axis.x;


            velocity *= 5;
            
            if (Mathf.Abs(JoystickController.Instance.Left.Angle - JoystickController.Instance.Right.Angle) > 45) {
                velocity /= 1.5f;
            }

            _animator.SetBool("moving", true);
            _animator.SetFloat("angle", JoystickController.Instance.Left.Angle-JoystickController.Instance.Right.Angle);
            _rigidbody.velocity = velocity;

            
        } else {

            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool("moving", false);

        }

    }

}
