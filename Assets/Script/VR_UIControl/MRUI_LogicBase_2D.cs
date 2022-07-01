using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;

namespace ccUI_U3DSpace
{
    public class MRUI_LogicBase_2D : MRUI_LogicBase
    {
        private Canvas _Canvas;

        protected override void On_Init()
        {
            base.On_Init();
            _Canvas = GetComponentInParent<Canvas>();
        }

        protected Canvas f_GetCanvas()
        {
            return _Canvas;
        }
    }
}
