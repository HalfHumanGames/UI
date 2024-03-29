﻿using UnityEngine;
using UnityEngine.UI;

namespace HHG.UI
{ 
    [RequireComponent(typeof(CanvasGroup))]
    public class UIView : UIElement
    {
        public Selectable SelectOnFocus;

        protected override void Awake()
        {
            base.Awake();
            RectTransform.anchoredPosition = Vector2.zero;
        }

        protected override void Start()
        {
            base.Start();
            InitializeState();
        }

        protected override void OnFocus()
        {
            base.OnFocus();
            CanvasGroup.interactable = true;
            if (SelectOnFocus != null) SelectOnFocus.Select();
        }

        protected override void OnUnfocus()
        {
            base.OnUnfocus();
            CanvasGroup.interactable = false;
        }

        internal override void InitializeState()
        {
            children.ForEach(child => child.InitializeState());
            switch (state)
            {
                case OpenState.Closed:
                    Close(true);
                    break;
                case OpenState.Opening:
                    state = OpenState.Open;
                    Close(true);
                    UI.Push(GetType(), Id);
                    break;
                case OpenState.Open:
                    Close(true);
                    UI.Push(GetType(), Id, true);
                    break;
                case OpenState.Closing:
                    state = OpenState.Closed;
                    Open(true);
                    Close(false);
                    break;
            }
        }

        public void GoTo(bool instant = false)
        {
            UI.GoTo(GetType(), null, instant);
        }

        public void Push(bool instant = false)
        {
            UI.Push(GetType(), null, instant);
        }

        public void Clear(bool instant = false)
        {
            UI.Clear(instant);
        }

        public void Swap(bool instant = false)
        {
            UI.Swap(GetType(), null, instant);
        }
    }
}