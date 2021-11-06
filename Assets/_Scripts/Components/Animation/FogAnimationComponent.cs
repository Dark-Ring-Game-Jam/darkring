using Spine.Unity;

public class FogAnimationComponent : AnimationComponent
{
    private const string IdleAnimationName = "static";
    private const string OffAnimationName = "off";
    private const string ExplanationAnimationName = "explanation";

    public void InitFogAnimation(SkeletonAnimation skeletonAnimation)
    {
        Init(IdleAnimationName, skeletonAnimation);
    }

    public void Off()
    {
        SetAnimationState(OffAnimationName, false);
    }

    public void Idle()
    {
        SetAnimationState(IdleAnimationName);
    }

    public void On()
    {
        SetAnimationState(ExplanationAnimationName, false);
    }
}