using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour {

    public enum JoystickMode {
        INTERACT,
        CONTROLL
    }

    public Image Base;
    public Image Controller;
    
    public int Radius;
    public int DeadRadius;

    private RectTransform _rectTransform;
    private Vector2 _axis;
    private float _angle;

    void Start() {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public RectTransform RectTransform {
        get { return _rectTransform; }
    }

    public void Dispose() {
        Controller.rectTransform.localPosition = Vector3.zero;
        _axis = Vector2.zero;
        _angle = 0;
    }

    public void UpdateController(Vector3 position) {

        if (Vector3.Distance(position, _rectTransform.position) < DeadRadius) {
            Dispose();
            return;
        }
        
        Vector3 normalized = position - _rectTransform.position;

        _angle = Mathf.Atan2(normalized.x, normalized.y);

        if (normalized.magnitude <= Radius) {
            Controller.rectTransform.position = position;
        } else {
            Controller.rectTransform.position = new Vector3((Mathf.Sin(_angle) * Radius) + _rectTransform.position.x, (Mathf.Cos(_angle) * Radius) + _rectTransform.position.y, 0);
        }

        _axis = normalized.normalized;

    }

    public float Angle {
        get { return _angle; }
    }

    public Vector2 Axis {
        get { return _axis; }
    }

}
