using UnityEngine;
using UnityEngine.UI;

public class AdjustVideoSize : MonoBehaviour
{
    public RectTransform rawImageRectTransform;
    public RenderTexture renderTexture;

    void Start()
    {
        if (rawImageRectTransform != null && renderTexture != null)
        {
            AdjustSize();
        }
        else
        {
            Debug.LogError("RectTransform or RenderTexture not assigned.");
        }
    }

    void AdjustSize()
    {
        float aspectRatio = (float)renderTexture.width / renderTexture.height;
        rawImageRectTransform.sizeDelta = new Vector2(rawImageRectTransform.sizeDelta.y * aspectRatio, rawImageRectTransform.sizeDelta.y);
    }
}
