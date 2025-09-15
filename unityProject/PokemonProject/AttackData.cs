using UnityEngine;
using System.Collections.Generic;
public class AttackData
{
    private float basePower;
    private int attackNumber;
    private string attackName;

    public AttackData(string name, int number, float power)
    {
        attackName = name;
        attackNumber = number;
        basePower = power;
    }

    public float CalculateDamage(int attackerLvl, int attackStat, int defenseStat)
    {
        float damage = (((2f * attackerLvl / 5f + 2f) * basePower * attackStat / defenseStat) / 50f + 2f)+10;
        return damage;
    }

    public string GetAttackName() => attackName;
}


public static class AttackDatabase
{
    public static Dictionary<string, List<AttackData>> attacksByPokemon =
        new Dictionary<string, List<AttackData>>()
        {
            {
                "Charmander", new List<AttackData>()
                {
                    new AttackData("Scratch", 1, 40),
                    new AttackData("Ember", 2, 40),
                    new AttackData("Smokescreen", 3, 0), // attaque de statut
                    new AttackData("Dragon Rage", 4, 40), // dégâts fixes en vrai, simplifié ici
                    new AttackData("Flamethrower", 5, 90)
                }
            },
            {
                "Bulbasaur", new List<AttackData>()
                {
                    new AttackData("Tackle", 1, 40),
                    new AttackData("Growl", 2, 0), // statut
                    new AttackData("Vine Whip", 3, 45),
                    new AttackData("Leech Seed", 4, 0), // statut
                    new AttackData("Razor Leaf", 5, 55)
                }
            },
            {
                "Squirtle", new List<AttackData>()
                {
                    new AttackData("Tackle", 1, 40),
                    new AttackData("Tail Whip", 2, 0), // statut
                    new AttackData("Water Gun", 3, 40),
                    new AttackData("Withdraw", 4, 0), // statut
                    new AttackData("Bubble", 5, 40)
                }
            }
        };
}