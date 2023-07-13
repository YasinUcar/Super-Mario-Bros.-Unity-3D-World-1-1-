using UnityEngine;

public class ChangeSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _firstSFX;
    [SerializeField] private AudioClip _secondSFX;
    

    public void PlaySFX(string firstTagName, string currentTagName)
    {
        if (firstTagName == currentTagName)
        {
            AudioManager.Instance.PlaySound(_firstSFX);
        }
        else
        {
            AudioManager.Instance.PlaySound(_secondSFX);
        }
    }
}
