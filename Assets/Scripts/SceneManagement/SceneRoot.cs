using UnityEngine;

public class SceneRoot : MonoBehaviour
{
    public virtual void Initialize(SceneArgs args)
    {
        Debug.Log($"Scene loaded with args: {args.GetType()}");
    }
}
