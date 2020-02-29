using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tamana
{
    public class TPC_AnimController : SingletonMonobehaviour<TPC_AnimController>
    {
        private Animator _characterAnimator;
        public Animator CharacterAnimator
        {
            get
            {
                if (_characterAnimator == null)
                {
                    _characterAnimator = GetComponent<Animator>();

                    if (_characterAnimator == null)
                    {
                        Debug.Log("Couldn't find 'Animator' component.");
                        return null;
                    }
                }

                return _characterAnimator;
            }
        }

        public Dictionary<string, TPC_Anim_AnimInfo<bool>> AnimStatusDic { get; private set;} = new Dictionary<string, TPC_Anim_AnimInfo<bool>>();
        public Dictionary<string, bool> AnimStateDic { get; private set; } = new Dictionary<string, bool>();

        public Dictionary<string, int> AnimatorLayerInfoDic { private set; get; } = new Dictionary<string, int>();

        public TPC_AnimParams AnimParams { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            AnimParams = new GameObject(nameof(TPC_AnimParams)).AddComponent<TPC_AnimParams>();
            GetAllAttribute();
            GetAnimatorLayerInformation();

            AnimStatusDic = ClassManager.GetAllConstValueFromStaticClass<TPC_Anim_AttributeBase>(typeof(TPC_Anim_RunAnimsetBasic));
            AnimStatusDic = AnimStatusDic.Concat(ClassManager.GetAllConstValueFromStaticClass<TPC_Anim_AttributeBase>(typeof(TPC_Anim_SwordAnimsetPro)))
                .ToLookup(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, g => g.First()); ;
        }

        private void Update()
        {
            AssignAnimatorValue();
        }

        public bool IsPlaying(string key)
        {
            if (AnimStatusDic.ContainsKey(key) == false)
            {
                Debug.Log($"{nameof(AnimStatusDic)} Dictionary doesn't have '{key}' key", Debug.LogType.Warning);
                return false;
            }

            return AnimStatusDic[key].TValue;
        }

        public void SetAnimStatusValue(string key, bool value)
        {
            if (AnimStatusDic.ContainsKey(key) == false)
            {
                Debug.Log($"{nameof(AnimStatusDic)} Dictionary doesn't have '{key}' key", Debug.LogType.Warning);
                return;
            }

            var newValue = AnimStatusDic[key];
            newValue.TValue = value;

            AnimStatusDic[key] = newValue;
        }

        private void AssignAnimatorValue()
        {
            var keys = new List<string>(AnimStateDic.Keys);
            foreach (var key in keys)
            {
                AnimStateDic[key] = false;
            }

            var layerCount = CharacterAnimator.layerCount;

            foreach (var anim in AnimStatusDic)
            {
                if (anim.Value.TValue == true)
                {
                    var thisLayerIndex = AnimatorLayerInfoDic[anim.Value.Layer];
                    if (thisLayerIndex < layerCount - 1 &&
                        CharacterAnimator.GetLayerWeight(thisLayerIndex + 1) > 0.0f)
                    {
                        continue;
                    }

                    foreach (var t in ClassManager.GetAttributesFromClass<TPC_Anim_AttributeBase>())
                    {                        
                        if (anim.Value.StateDic[t.Name] == true)
                        {
                            AnimStateDic[t.Name] = true;
                        }
                    }
                }
            }
        }

        public void SetLayerWeight(string layerName, float weight)
        {
            if(weight < 0.0f || weight > 1.0f)
            {
                Debug.Log($"Invalid value ({weight}). Weight must a value between 0 and 1", Debug.LogType.Warning);
                return;
            }

            var layerIndex = CharacterAnimator.GetLayerIndex(layerName);
            CharacterAnimator.SetLayerWeight(layerIndex, weight);
        }

        public float GetLayerWeight(string layerName)
        {
            var layerIndex = CharacterAnimator.GetLayerIndex(layerName);
            return CharacterAnimator.GetLayerWeight(layerIndex);
        }

        public void PlayAnim(string animName)
        {
            CharacterAnimator.Play(animName);
        }

        public void PlayStartMoveAnimation()
        {
            TPC_PlayerMovement.OnPlayerMoveStart.Invoke();
            AnimParams.IsMoving = true;
        }

        public void PlayStopMoveAnimation()
        {
            TPC_PlayerMovement.OnPlayerMoveStop.Invoke();
            AnimParams.IsMoving = false;
        }

        private void GetAllAttribute()
        {
            foreach (var t in ClassManager.GetAttributesFromClass<TPC_Anim_AttributeBase>())
            {
                AnimStateDic.Add(t.Name, false);
            }
        }

        private void GetAnimatorLayerInformation()
        {
            for(int i = 0; i < CharacterAnimator.layerCount; i++)
            {
                AnimatorLayerInfoDic.Add(CharacterAnimator.GetLayerName(i), i);
            }
        }
    }
}
