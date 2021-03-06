﻿using System;
using Newtonsoft.Json;

namespace Engine
{
    // NOTE/FIXME: This is probably not useful (this is just a game object with a camera component,
    //  and can just be implemented as such in the scene.
    //  (Instead of having a specific class (or the component itself could be renamed to Camera).
    public class Camera : GameObject
    {
        public Camera() : base("Main Camera")
        {
        }

        public override void Init()
        {
            AddComponent<CameraComponent>();
        }
    }
}