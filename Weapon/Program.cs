using System;

class Weapon
{
    private Bullet _bullet;
    private int _counteBullets;

    public Weapon(Bullet bullet, int counteBullets)
    {
        if (bullet != null && counteBullets >= 0)
        {
            _bullet = bullet;
            _counteBullets = counteBullets;
        }
    }

    public void Fire(Player player)
    {
        if (_counteBullets > 0)
        {
            player.TakeDamage(_bullet.Damage);
            _counteBullets--;
        }
    }
}

class Player
{
    public int Health { get; private set; }

    public Player(int health)
    {
        if (health > 0)
        {
            Health = health;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

class Bullet
{
    public int Damage { get; private set; }

    public Bullet(int damage)
    {
        if (Damage >= 0)
        {
            Damage = damage;
        }
    }
}

class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon)
    {
        if (weapon != null)
        {
            _weapon = weapon;
        }
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}
