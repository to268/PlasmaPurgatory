using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PlasmaPurgatory
{
    static class Collisions
    {
        public static void CheckCollision(Player player, Rectangle playerAttackRect, List<Level.EnemyData> enemies)
        {
            // Check player attacks collisions
            if (player.IsAttacking)
            {
                foreach (Level.EnemyData enemyData in enemies)
                    for (int i = 0; i < enemyData.patterns.Count; i++)
                        for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                            if (playerAttackRect.Intersects(enemyData.patterns[i].Bullets[j].Rectangle) &&
                                enemyData.patterns[i].Bullets[j].Type == Bullet.BulletType.BREAKABLE)
                                enemyData.patterns[i].Bullets.Remove(enemyData.patterns[i].Bullets[j]);
                
                foreach (Level.EnemyData enemyData in enemies)
                    if (playerAttackRect.Intersects(enemyData.enemy.Rectangle))
                        enemyData.enemy.TakeDamage();
            }

            // Check player colliding a bullet
            foreach (Level.EnemyData enemyData in enemies)
            {
                for (int i = 0; i < enemyData.patterns.Count; i++)
                {
                    for (int j = 0; j < enemyData.patterns[i].Bullets.Count; j++)
                    {
                        if (player.Rectangle.Intersects(enemyData.patterns[i].Bullets[j].Rectangle))
                        {
                            player.TakeDamage();
                            enemyData.patterns[i].Bullets.Remove(enemyData.patterns[i].Bullets[j]);
                        }
                    }
                }
            }
            
            // Check player colliding a enemy
            foreach (Level.EnemyData enemyData in enemies)
                if (player.Rectangle.Intersects(enemyData.enemy.Rectangle))
                    player.TakeDamage();
        }
    }
}