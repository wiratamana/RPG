using UnityEngine;
using System.Collections;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon", order = 1)]
    public class Item_Weapon : Item_Base
    {
        [SerializeField] private Vector3 holsterPosition;
        [SerializeField] private Vector3 holsterRotation;
        [SerializeField] private Vector3 equipPosition;
        [SerializeField] private Vector3 equipRotation;
        [SerializeField] private GameObject prefab;

        public Vector3 HolsterPosition { get { return holsterPosition; } }
        public Quaternion HolsterRotation { get { return Quaternion.Euler(holsterRotation); } }
        public Vector3 EquipPostion { get { return equipPosition; } }
        public Quaternion EquipRotation { get { return Quaternion.Euler(equipRotation); } }
        public GameObject Prefab { get { return prefab; } }
    }
}
