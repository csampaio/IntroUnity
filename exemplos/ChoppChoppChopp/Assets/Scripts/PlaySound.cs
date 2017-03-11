using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource explosion;
    public bool playOnTrigger = false;
    public string layerName;

    void OnTriggerEnter(Collider other)
    {
        int layerNum = LayerMask.NameToLayer(layerName);
        if (other.gameObject.layer.Equals(layerNum) && playOnTrigger)
        {
            explosion.Play();
            Debug.Log("Kaboommm");
        }
    }
}