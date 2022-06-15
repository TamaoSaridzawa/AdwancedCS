using System;

namespace CleanCode_Task3
{
    class Weapon
    {
        private const int MinNumberBullets = 0;
        private const int NumberBulletsPerShot = 1;

        private int _bullets;

        public bool CanShoot() => _bullets > MinNumberBullets;

        public void Shoot() => _bullets -= NumberBulletsPerShot;
    }
}
