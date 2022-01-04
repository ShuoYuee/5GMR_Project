using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epibyte.ConceptVR
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        public UnityEvent onHoveredEvent;
        public UnityEvent onClickedEvent;
        public UnityEvent onReleasedEvent;
        public UnityEvent onLeaveEvent;

        public void OnHovered()
        {
            if (onHoveredEvent == null) { return; }
            onHoveredEvent.Invoke();
        }

        public void OnClicked()
        {
            if (onClickedEvent == null) { return; }
            onClickedEvent.Invoke();
        }

        public void OnReleased()
        {
            if (onReleasedEvent == null) { return; }
            onReleasedEvent.Invoke();
        }

        public void OnLeave()
        {
            if (onLeaveEvent == null) { return; }
            onLeaveEvent.Invoke();
        }
    }
}