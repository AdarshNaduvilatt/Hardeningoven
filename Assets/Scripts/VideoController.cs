using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject rawImageGameObject; // Reference to the GameObject with the Raw Image component

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Stop(); // Ensure the video does not play on start
    }

    public void PlayVideo()
    {
        if (videoPlayer.isPlaying)
        {
            Debug.Log("Pausing video");
            videoPlayer.Pause();
        }
        else
        {
            Debug.Log("Playing video");
            videoPlayer.Play();
            rawImageGameObject.SetActive(true); // Ensure the Raw Image is visible when playing
        }
    }

    public void CloseVideo()
    {
        Debug.Log("Closing video");
        videoPlayer.Stop(); // Stop the video
        rawImageGameObject.SetActive(false); // Hide the Raw Image
    }
}
