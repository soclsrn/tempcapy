public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }

    public enum Scene
    {
        Unknown,
        Dev,
        Game,
        Title,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }

    public enum CardID{
        BasicTower,
        MeleeTower
    }

    public enum TargetType{
        Nearest,
        Random
    }

    public enum BulletType{
        Targeting,
        NonTargeting
    }

}