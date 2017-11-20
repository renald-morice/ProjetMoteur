namespace Engine
{
    public interface IPhysicComponent
    {
        // From our representation to the physics library.
        void PreUpdate();
        // Physic library changes only.
        void FixedUpdate();
        // From the physics library to our representation.
        void PostUpdate();
    }
}