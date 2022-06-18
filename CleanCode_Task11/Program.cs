using System;

namespace CleanCode_Task11
{
    class AnimationsManager
    {
        public void EnableEffects()
        {
            _effects.StartEnableAnimation();
        }

        public void DisableEffects()
        {
            _pool.Free(this);
        }
    }
}
