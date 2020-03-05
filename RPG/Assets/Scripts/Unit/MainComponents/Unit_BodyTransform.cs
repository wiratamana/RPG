using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Unit_BodyTransform : MonoBehaviour
    {
        private const string ROOT   = "Root";
        private const string HIPS   = "Hips";
        private const string SPINE  = "Spine_01";
        private const string HAND_R = "Hand_R";
        private const string HEAD   = "Head";

        private Transform root;
        public Transform Root => this.GetChildWithNameAndAssign(ROOT, ref root);

        private Transform hips;
        public Transform Hips => this.GetChildWithNameFromParentAndAssign(HIPS, Root, ref hips);

        private Transform spine;
        public Transform Spine => this.GetChildWithNameFromParentAndAssign(SPINE, Hips, ref spine);

        private Transform handr;
        public Transform HandR => this.GetChildWithNameFromParentAndAssign(HAND_R, Spine, ref handr);

        private Transform head;
        public Transform Head => this.GetChildWithNameFromParentAndAssign(HEAD, Spine, ref head);
    }
}
