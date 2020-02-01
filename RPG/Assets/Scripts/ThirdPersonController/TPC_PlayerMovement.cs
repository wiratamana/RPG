using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_PlayerMovement : SingletonMonobehaviour<TPC_PlayerMovement>
    {
        private Dictionary<string, TPC_Anim_AnimInfo<bool>> RunAnimsetBasicDic = new Dictionary<string, TPC_Anim_AnimInfo<bool>>();
        private Dictionary<string, bool> AnimStateDic = new Dictionary<string, bool>();

        protected override void Awake()
        {
            base.Awake();

            GetAllAttribute();
            GetAllConstValueFromRunAnimsetBasic();
        }

        private void Update()
        {
            AssignAnimatorValue();

            if (KeyboardController.IsForwardPressed == true && AnimStateDic[nameof(TPC_Anim_AttributeMoving)] == false)
            {
                TPC_AnimController.Instance.PlayStartMoveAnimation();
            }
            else if(KeyboardController.IsForwardPressed == false && AnimStateDic[nameof(TPC_Anim_AttributeMoving)] == true 
                && AnimStateDic[nameof(TPC_Anim_AttributeMoveStopping)] == false)
            {
                TPC_AnimController.Instance.PlayAnim(TPC_Anim_RunAnimsetBasic.RunFwdStop_LU);
            }

            if(AnimStateDic[nameof(TPC_Anim_AttributeMoving)] == true && 
                AnimStateDic[nameof(TPC_Anim_AttributeMoveStopping)] == false &&
                AnimStateDic[nameof(TPC_Anim_AttributeMoveStarting)] == false)
            {
                var cameraForward = GameManager.MainCamera.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                var lookRotation = Quaternion.LookRotation(cameraForward);
                GameManager.Player.transform.rotation = Quaternion.Slerp(GameManager.Player.transform.rotation, lookRotation, 5 * Time.deltaTime);
            }
        }

        public bool IsPlaying(string key)
        {
            if(RunAnimsetBasicDic.ContainsKey(key) == false)
            {
                Debug.Log($"RunAnimsetBasicDic Dictionary doesn't have '{key}' key", Debug.LogType.Warning);
                return false;
            }

            return RunAnimsetBasicDic[key].value;
        }

        public void SetValue(string key, bool value)
        {
            if(RunAnimsetBasicDic.ContainsKey(key) == false)
            {
                Debug.Log($"RunAnimsetBasicDic Dictionary doesn't have '{key}' key", Debug.LogType.Warning);
                return;
            }

            var newValue = RunAnimsetBasicDic[key];
            newValue.value = value;

            RunAnimsetBasicDic[key] = newValue;
        }

        private void AssignAnimatorValue()
        {
            var keys = new List<string>(AnimStateDic.Keys);
            foreach(var key in keys)
            {
                AnimStateDic[key] = false;
            }

            foreach(var anim in RunAnimsetBasicDic)
            {
                if (anim.Value.value == true)
                {
                    foreach(var t in ClassManager.AnimAttributes)
                    {
                        if(anim.Value.StateDic[t.Name] == true)
                        {
                            AnimStateDic[t.Name] = true;
                        }
                    }
                }
            }
        }

        private void GetAllConstValueFromRunAnimsetBasic()
        {
            var constVars = typeof(TPC_Anim_RunAnimsetBasic).GetFields(BindingFlags.Public
                | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var constVar in constVars)
            {
                var value = new TPC_Anim_AnimInfo<bool>(false);
                foreach(var t in ClassManager.AnimAttributes)
                {
                    value.StateDic[t.Name] = constVar.IsDefined(t);
                }

                var fieldName = constVar.GetValue(null).ToString();
                RunAnimsetBasicDic.Add(fieldName, value);
            }
        }

        private void GetAllAttribute()
        {
            foreach (var t in ClassManager.AnimAttributes)
            {
                Debug.Log($"Add : '{t.Name}' to {nameof(AnimStateDic)}");
                AnimStateDic.Add(t.Name, false);
            }
        }
    }
}
