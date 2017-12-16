namespace Engine
{
    public interface IPhysicComponent
    {
        // NOTE: Both of these should be internal to our Engine.
        // From our representation to the physics library.
        void CopyState();
        // Physics library to our representation.
        void ApplyChanges();
        
        
        // Callback before starting any physics update.
        void PreUpdate();
        // Callback after each physics update.
        void FixedUpdate();
        // Callback after every physics update is done.
        void PostUpdate();
    }
}