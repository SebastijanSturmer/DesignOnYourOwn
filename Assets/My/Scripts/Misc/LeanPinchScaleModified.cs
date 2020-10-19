using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lean.Touch
{
    public class LeanPinchScaleModified : LeanPinchScale
    {
		[SerializeField] private float _maxScale;
		[SerializeField] private float _minScale;

		[SerializeField] private ScriptableEvent _leanPinchScaleInput;

		protected override void Update()
        {
			// Store
			var oldScale = transform.localPosition;

			// Get the fingers we want to use
			var fingers = Use.GetFingers();

			// Calculate pinch scale, and make sure it's valid
			var pinchScale = LeanGesture.GetPinchScale(fingers);

			if (pinchScale != 1.0f)
			{
				pinchScale = Mathf.Pow(pinchScale, Sensitivity);

				// Perform the translation if this is a relative scale
				if (Relative == true)
				{
					var pinchScreenCenter = LeanGesture.GetScreenCenter(fingers);

					if (transform is RectTransform)
					{
						TranslateUI(pinchScale, pinchScreenCenter);
					}
					else
					{
						Translate(pinchScale, pinchScreenCenter);
					}
				}

				_leanPinchScaleInput.RaiseEvent(new FloatMessage(pinchScale));

			}
		}
    }
}
