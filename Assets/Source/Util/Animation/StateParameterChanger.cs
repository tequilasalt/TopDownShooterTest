using System;
using UnityEngine;
using System.Collections;

public class StateParameterChanger : StateMachineBehaviour {

    public enum ParameterType {
        BOOL,
        FLOAT,
        INT
    }

    public enum ChangeTime {
        ON_START,
        ON_END
    }

    [System.Serializable]
    public class StateParameter{

        public ParameterType Type;
        public ChangeTime Timing;
        public string Parameter;
        public string Value;

    }

    public StateParameter[] Parameters;
    private Animator _animator;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        _animator = animator;
        bool onEnd = false;

        foreach (var stateParameter in Parameters) {

            if (stateParameter.Timing == ChangeTime.ON_END) {
                onEnd = true;
            } else {
                SetParameter(stateParameter);
            }
        }

        if (onEnd) {
            Coroutiner.StartCoroutine(OnStateEnd(stateInfo.length-0.2f));
        }
    }

    private IEnumerator OnStateEnd(float length) {
        yield return new WaitForSeconds(length);

        foreach (var stateParameter in Parameters) {

            if (stateParameter.Timing == ChangeTime.ON_END) {

                if (_animator != null) {
                    SetParameter(stateParameter);
                }

            }

        }
    }

    private void SetParameter(StateParameter parameter) {

        if (_animator == null) {
            return;
        }

        switch (parameter.Type) {
            case ParameterType.BOOL:
                _animator.SetBool(parameter.Parameter, Boolean.Parse(parameter.Value));
                break;
            case ParameterType.FLOAT:
                _animator.SetFloat(parameter.Parameter, float.Parse(parameter.Value));
                break;
            case ParameterType.INT:
                _animator.SetInteger(parameter.Parameter, int.Parse(parameter.Value));
                break;
        }
    }
}
