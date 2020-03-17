using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class GlobalAssetsReference : MonoBehaviour
    {
        private static GlobalAssetsReference instance;
        private static GlobalAssetsReference Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<GlobalAssetsReference>();
                }

                return instance;
            }
        }
        [SerializeField] private Sprite _inventoryItemOnPointerOver_Sprite;

        public static Sprite InventoryItemOnPointerOver_Sprite => Instance._inventoryItemOnPointerOver_Sprite;
    }
}
