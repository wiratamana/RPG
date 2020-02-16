using UnityEngine;
using System.Linq;
using System.Collections;

namespace Tamana
{
    public class Inventory_EquipmentManager : SingletonMonobehaviour<Inventory_EquipmentManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(Inventory_EquipmentManager));
            DontDestroyOnLoad(go);
            go.AddComponent<Inventory_EquipmentManager>();
        }        

        public string ObjectPartPath { get; set; }

        public void EquipModularPart()
        {
            // ===============================================================================================
            // Get the Transform renference from player and the portrait on inventory menu.
            // ===============================================================================================
            var PortraitTransform = Inventory_Menu_PlayerPortrait.Instance.transform;
            var playerTransform = GameManager.Player;

            // ===============================================================================================
            // Split the path to the Transform part
            // ===============================================================================================
            var split = ObjectPartPath.Split('/').ToList();
            split.RemoveAt(0);

            // ===============================================================================================
            // Get the equip part by searching it through child
            // ===============================================================================================
            var PortraitPart = PortraitTransform;
            var playerPart = playerTransform;
            while (split.Count > 0)
            {
                PortraitPart = GetChildTransformWithName(PortraitPart, split[0]);
                playerPart = GetChildTransformWithName(playerPart, split[0]);
                split.RemoveAt(0);
            }

            // ===============================================================================================
            // Deactivate all parts with same type as the equip item
            // ===============================================================================================
            var portaitPartParent = PortraitPart.parent;
            var playerPartParent = playerPart.parent;
            for(int i = 0; i < portaitPartParent.childCount; i++)
            {
                if(portaitPartParent.GetChild(i) == PortraitPart)
                {
                    continue;
                }

                portaitPartParent.GetChild(i).gameObject.SetActive(false);
                playerPartParent.GetChild(i).gameObject.SetActive(false);
            }

            // ===============================================================================================
            // Finally, activate the equip
            // ===============================================================================================
            playerPart.gameObject.SetActive(true);
            PortraitPart.gameObject.SetActive(true);
        }

        public void EquipWeapon()
        {

        }

        public void UnequipModularPart()
        {
            
        }

        public void UnequipWeapon()
        {

        }

        private Transform GetChildTransformWithName(Transform parent, string name)
        {
            for(int i = 0; i < parent.childCount; i++)
            {
                if(parent.GetChild(i).name == name)
                {
                    return parent.GetChild(i);
                }
            }

            return null;
        }
    }
}
