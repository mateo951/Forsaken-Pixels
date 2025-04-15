using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
    public class KnightUIAnimator : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        [SerializeField] private RectTransform knight;
        public RectTransform[] buttonPositions;
        public Button[] buttons;
        public float jumpHeight = 2.0f;
        public float jumpDuration = 0.5f;

        private int _currentIndex;
    
        [SerializeField] private Animator knightAnimator;
        private Coroutine _moveCoroutine;

        void Start()
        {
            // Start the knight at the first button position
            // World Position - knight.position = buttonPositions[_currentIndex].position;
            // Anchored position
            knight.anchoredPosition = buttonPositions[0].anchoredPosition;
        }

        public void MoveToButton(BaseEventData data)
        {
            // Cast BaseEventData to PointerEventData
            PointerEventData pointerData = data as PointerEventData;
            if (pointerData == null) return;
            // Get the GameObject currently under the pointer (even if it's a child)
            GameObject hoveredObject = pointerData.pointerCurrentRaycast.gameObject;

            // Safely find the Button component on the parent
            Button hoveredButton = hoveredObject?.GetComponentInParent<Button>();
            if (hoveredButton == null) return;

            // Now safely get the index in your button array
            int index = System.Array.IndexOf(buttons, hoveredButton);

            if (index != -1 && index != _currentIndex)
            {
                MoveKnightTo(index);
                _currentIndex = index;
            }
        }

        private void MoveKnightTo(int buttonIndex)
        {
            if (buttonPositions == null || buttonIndex < 0 || buttonIndex >= buttonPositions.Length) return;
            if (buttonPositions[buttonIndex] == null || knight == null) return;

            Vector2 targetAnchoredPos = buttonPositions[buttonIndex].anchoredPosition;

            // Controls direction of sprite
            knight.localScale = targetAnchoredPos.x < knight.anchoredPosition.x 
                ? new Vector3(-1, 1, 1) 
                : new Vector3(1, 1, 1);

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(SmoothMoveKnight(targetAnchoredPos));
            _currentIndex = buttonIndex;
        }


        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SmoothMoveKnight(Vector2 targetAnchoredPos)
        {
            float duration = 1.3f;
            float elapsed = 0f;
            Vector2 startPos = knight.anchoredPosition;

            knightAnimator?.SetBool(IsRunning, true);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                t = t * t * (3f - 2f * t); 

                float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;
                Vector2 temp = Vector2.Lerp(startPos, targetAnchoredPos, t) + new Vector2(0, height);

                ((RectTransform)knight).anchoredPosition = temp;

                yield return null;
            }

            knightAnimator?.SetBool(IsRunning, false);
            knight.anchoredPosition = targetAnchoredPos;
        }

    }
}