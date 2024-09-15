namespace Mapbox.Examples
{
	using UnityEngine;
    using UnityEngine.EventSystems;

    namespace Scripts.Utilities
	{
		public class DragRotate : MonoBehaviour
		{
			[SerializeField]
			Transform _objectToRotate;

			[SerializeField]
			float _multiplier;

			Vector3 _startTouchPosition;

			void Update()
			{
				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					_startTouchPosition = Input.mousePosition;
				}

				if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					var dragDelta = Input.mousePosition - _startTouchPosition;
					var axis = new Vector3(0f, -dragDelta.x * _multiplier, 0f);
					_objectToRotate.RotateAround(_objectToRotate.position, axis, _multiplier);
				}
			}
		}
	}
}
