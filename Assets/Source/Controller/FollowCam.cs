using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public Vector3 PositionOffset;
    public Vector3 LookOffset;

    public GameObject Target;
    
	void Update () {

	    transform.position = Target.transform.position + PositionOffset;
	    transform.rotation = Quaternion.LookRotation((Target.transform.position + LookOffset)-transform.position);

    }

}
