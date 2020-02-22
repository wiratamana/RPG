using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_BodyTransform : SingletonMonobehaviour<TPC_BodyTransform>
    {
        private const string ROOT   = "Root";
        private const string HIPS   = "Hips";
        private const string SPINE  = "Spine_01";
        private const string HAND_R = "Hand_R";

        private Transform root;
        public Transform Root
        {
            get
            {
                if(root == null)
                {
                    root = GetChild(ROOT, transform);
                }

                return root;
            }
        }

        private Transform hips;
        public Transform Hips
        {
            get
            {
                if(hips == null)
                {
                    hips = GetChild(HIPS, Root);
                }

                return hips;
            }
        }

        private Transform spine;
        public Transform Spine
        {
            get
            {
                if(spine == null)
                {
                    spine = GetChild(SPINE, Hips);
                }

                return spine;
            }
        }

        private Transform handr;
        public Transform HandR
        {
            get
            {
                if (handr == null)
                {
                    handr = TransformUtils.GetChildRecursive(HAND_R, Spine);

                    if(handr == null)
                    {
                        Debug.Log($"Failed to get '{nameof(Transform)}' with name {HAND_R}", Debug.LogType.ForceQuit);
                    }
                }

                return handr;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private Transform GetChild(string childName, Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child.name != childName)
                {
                    continue;
                }

                return child;
            }

            if (root == null)
            {
                Debug.Log($"Failed to get '{nameof(Transform)}' with name {childName}", Debug.LogType.ForceQuit);
            }

            return null;
        }
    }
}
