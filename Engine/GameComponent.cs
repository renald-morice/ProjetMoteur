namespace Engine
{
    // NOTE(francois)/TODO:
    //  This is currently used only to have access to a gameObject property
    //  (this avoids having to cast entity into a GameObject (to get the transform, most of the time)).
    //  This type may be used in other places to have compile-time checks. Maybe.
    public class GameComponent : Component
    {
        public GameObject gameObject { get => entity as GameObject; }
    }
}