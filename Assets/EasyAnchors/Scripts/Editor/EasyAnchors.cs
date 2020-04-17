using UnityEngine;
using UnityEditor;

namespace Tools.EasyAnchors
{
    public class EasyAnchors : MonoBehaviour
    {
        [MenuItem("Tools/EasyAnchors/Anchors to Corners %[")]
        static void AnchorsToCorners()
        {
            foreach (RectTransform _rectTransform in Selection.transforms)
            {
                // Get the parent
                RectTransform _parent = _rectTransform.parent as RectTransform;

                // Security
                if (_rectTransform == null || _parent == null) return;

                // Define the new anchors
                Vector2 newAnchorsMin = new Vector2(
                    _rectTransform.anchorMin.x + _rectTransform.offsetMin.x / _parent.rect.width,
                    _rectTransform.anchorMin.y + _rectTransform.offsetMin.y / _parent.rect.height);

                Vector2 newAnchorsMax = new Vector2(
                    _rectTransform.anchorMax.x + _rectTransform.offsetMax.x / _parent.rect.width,
                    _rectTransform.anchorMax.y + _rectTransform.offsetMax.y / _parent.rect.height);


                Undo.RecordObject(_rectTransform, "Anchor to Corner");
                // Set it
                _rectTransform.anchorMin = newAnchorsMin;
                _rectTransform.anchorMax = newAnchorsMax;

                // Set the offsets
                _rectTransform.offsetMin = _rectTransform.offsetMax = new Vector2(0, 0);
            }


        }

        [MenuItem("Tools/EasyAnchors/Corners to Anchors %]")]
        static void CornersToAnchors()
        {
            foreach (RectTransform _rectTransform in Selection.transforms)
            {
                // Security
                if (_rectTransform == null) return;

                Undo.RecordObject(_rectTransform, "Corner to Anchor");

                // Set the offsets without changing the anchorMin and Max
                _rectTransform.offsetMin = _rectTransform.offsetMax = new Vector2(0, 0);
            }
        }

        #region Mirror Horizontal

        [MenuItem("Tools/EasyAnchors/Mirror Horizontally Around Anchors %;")]
        static void MirrorHorizontallyAnchors()
        {
            MirrorHorizontally(false);
        }

        [MenuItem("Tools/EasyAnchors/Mirror Horizontally Around Parent Center %:")]
        static void MirrorHorizontallyParent()
        {
            MirrorHorizontally(true);
        }

        static void MirrorHorizontally(bool aroundCenter)
        {
            foreach (RectTransform _rectTransform in Selection.transforms)
            {
                if (_rectTransform == null) return;


                Undo.RecordObject(_rectTransform, "Mirror Horizontal");

                if (aroundCenter)
                {
                    Vector2 oldAnchorMin = _rectTransform.anchorMin;



                    _rectTransform.anchorMin = new Vector2(1 - _rectTransform.anchorMax.x, _rectTransform.anchorMin.y);
                    _rectTransform.anchorMax = new Vector2(1 - oldAnchorMin.x, _rectTransform.anchorMax.y);
                }

                Vector2 oldOffsetMin = _rectTransform.offsetMin;

                _rectTransform.offsetMin = new Vector2(-_rectTransform.offsetMax.x, _rectTransform.offsetMin.y);
                _rectTransform.offsetMax = new Vector2(-oldOffsetMin.x, _rectTransform.offsetMax.y);

                _rectTransform.localScale = new Vector3(-_rectTransform.localScale.x, _rectTransform.localScale.y,
                    _rectTransform.localScale.z);
            }
        }

        #endregion

        #region Mirror Vertical

        [MenuItem("Tools/EasyAnchors/Mirror Vertically Around Anchors %'")]
        static void MirrorVerticallyAnchors()
        {
            MirrorVertically(false);
        }

        [MenuItem("Tools/EasyAnchors/Mirror Vertically Around Parent Center %\"")]
        static void MirrorVerticallyParent()
        {
            MirrorVertically(true);
        }

        static void MirrorVertically(bool aroundCenter)
        {
            foreach (RectTransform _rectTransform in Selection.transforms)
            {
                if (_rectTransform == null) return;

                Undo.RecordObject(_rectTransform, "Mirror Vertical");

                if (aroundCenter)
                {
                    Vector2 oldAnchorMin = _rectTransform.anchorMin;

                    _rectTransform.anchorMin = new Vector2(_rectTransform.anchorMin.x, 1 - _rectTransform.anchorMax.y);
                    _rectTransform.anchorMax = new Vector2(_rectTransform.anchorMax.x, 1 - oldAnchorMin.y);
                }

                Vector2 oldOffsetMin = _rectTransform.offsetMin;

                _rectTransform.offsetMin = new Vector2(_rectTransform.offsetMin.x, -_rectTransform.offsetMax.y);
                _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, -oldOffsetMin.y);

                _rectTransform.localScale = new Vector3(_rectTransform.localScale.x, -_rectTransform.localScale.y,
                    _rectTransform.localScale.z);
            }
        }

        #endregion

    }
}