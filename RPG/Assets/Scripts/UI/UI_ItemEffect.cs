using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_ItemEffect : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public Image IconBackground { private set; get; }
        public Image Icon { private set; get; }
        public RectTransform TextBackground { private set; get; }
        public TextMeshProUGUI Text { private set; get; }

        public static UI_ItemEffect CreateInstance(RectTransform parent,
            Item_Equipment_Effect effect, float xOffset, out float outWidth)
        {
            // ===============================================================================================
            // Variables decalaration
            // ===============================================================================================
            var iconSizeBG = new Vector2(parent.sizeDelta.y, parent.sizeDelta.y);
            var iconSize = iconSizeBG - new Vector2(4, 4);
            var textSizeBG = new Vector2(0, iconSizeBG.y);
            var textSize = new Vector2(0, iconSizeBG.y - 4);
            var textHorizontalSpacing = 5.0f * 2;
            var position = new Vector3((parent.sizeDelta.x * -0.5f) + xOffset, 0);

            // ===============================================================================================
            // Instantiate parent
            // ===============================================================================================
            var go = new GameObject(effect.ToString());
            var rt = go.AddComponent<RectTransform>();
            rt.SetParent(parent);
            var retval = go.AddComponent<UI_ItemEffect>();

            // ===============================================================================================
            // Instantiate IconBackground and Icon
            // ===============================================================================================
            retval.IconBackground = UI_Pool.Instance.GetImage(rt, (int)iconSizeBG.y, (int)iconSizeBG.x, nameof(IconBackground));
            retval.Icon = UI_Pool.Instance.GetImage(retval.IconBackground.rectTransform,
                (int)iconSize.x, (int)iconSize.y, nameof(Icon));
            retval.Icon.rectTransform.localPosition = Vector3.zero;
            retval.Icon.sprite = GlobalAssetsReference.GetMainStatusSprites(effect.type);

            // ===============================================================================================
            // Instantiate text, and get prefered width from the text. 
            // After that, instantiate text background, and calculate size of text, and textbackground.
            // ===============================================================================================
            retval.Text = UI_Pool.Instance.GetText(null, 0, 0, effect.value.ToString(), nameof(Text));
            textSizeBG.x = retval.Text.preferredWidth + textHorizontalSpacing;
            textSize.x = retval.Text.preferredWidth;
            retval.TextBackground = new GameObject(nameof(TextBackground)).AddComponent<RectTransform>();
            retval.TextBackground.SetParent(rt);
            retval.TextBackground.sizeDelta = textSizeBG;
            retval.Text.rectTransform.SetParent(retval.TextBackground);
            retval.Text.rectTransform.sizeDelta = textSize;
            retval.Text.rectTransform.localPosition = Vector3.zero;

            // ===============================================================================================
            // Get size of the parent, and reposition icon to left, and text to right
            // ===============================================================================================
            rt.sizeDelta = new Vector2(iconSizeBG.x + textSizeBG.x, iconSizeBG.y);
            retval.IconBackground.rectTransform.localPosition = new Vector3((rt.sizeDelta.x * -0.5f) + (iconSizeBG.x * 0.5f), 0);
            retval.TextBackground.localPosition = new Vector3((rt.sizeDelta.x * 0.5f) - (textSizeBG.x * 0.5f), 0);

            // ===============================================================================================
            // Assign output for next instantiation. And set my position.
            // ===============================================================================================
            outWidth = rt.sizeDelta.x;
            position.x += (outWidth * 0.5f);

            rt.localPosition = position;

            return retval;
        }

        public void ReturnToPool()
        {
            UI_Pool.Instance.RemoveImage(IconBackground);
            UI_Pool.Instance.RemoveImage(Icon);
            UI_Pool.Instance.RemoveText(Text);

            IconBackground = null;
            Icon = null;
            TextBackground = null;
            Text = null;
        }
    }
}
