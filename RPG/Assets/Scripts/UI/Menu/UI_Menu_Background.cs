using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Background : SingletonMonobehaviour<UI_Menu_Background>
    {
        private Image _backgroundImage;
        public Image BackgroundImage
        {
            get
            {
                if(_backgroundImage == null)
                {
                    _backgroundImage = GetComponent<Image>();

                    if(_backgroundImage == null)
                    {
                        Debug.Log($"Component {nameof(Image)} not exist.", Debug.LogType.ForceQuit);
                        return null;
                    }
                }

                return _backgroundImage;
            }
        }
    }
}
