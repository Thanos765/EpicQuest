using UnityEngine;

public class SignManager : MonoBehaviour
{
    private static SignManager instance;

    public SignText currentSign;

    public static SignManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SignManager>();
            }
            return instance;
        }
    }

    public void SetCurrentSign(SignText sign)
    {
        currentSign = sign;
    }

    public void Interact()
    {
        if (currentSign != null)
        {
            currentSign.Interact();
        }
    }
}
