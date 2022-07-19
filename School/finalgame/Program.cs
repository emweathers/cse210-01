using System;

namespace finalgame {
    abstract class Entity {
        public bool guarded = false;
        protected string name;
        protected int health;
        protected int maxHealth;
        protected int damage;
        protected Entity() {
            this.name = "";
            this.health = 0;
            this.damage = 0;
        }
        protected Entity(string name, int health, int damage) {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
            this.damage = damage;
        }
        public string GetName() {
            return name;
        }
        public int Health {
            get { return health; }
            set { health = value;}
        }
        public int GetDamage() {
            return damage;
        }
        public void AddCountToDamage(int count) {
            damage += count;
        }
        public int GetMaxHealth() {
            return maxHealth;
        }
        public void ResetHealth() {
            health = maxHealth;
        }
    }
    class Player : Entity {
        public bool hasSlept = false;
        public bool hasSharpStick = false;
        public int enemiesDefeated = 0;
        public int actionPoint;
        public Player(string name, int health, int damage, int actionPoint) {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
            this.damage = damage;
            this.actionPoint = actionPoint;
        }
    }
    class Enemy : Entity {
        int enemyID;
        public Enemy(string name, int health, int damage, int enemyID) {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
            this.damage = damage;
            this.enemyID = enemyID;
        }
    }

    abstract class Action {
        protected int actionCost;
        protected int damage;
        protected string name;
        protected Action() {
            this.damage = 0;
            this.name = "";
        }
        protected Action(int damage, string name) {
            this.damage = damage;
            this.name = name;
        }
        public string GetName() {
            return name;
        }
        public int GetDamage() {
            return damage;
        }
        public virtual void Attack(Player user, Enemy target, int damage) {
            if(user.actionPoint >= actionCost) {
                if(!target.guarded) {
                    target.Health -= (damage + (2 * user.GetDamage()));
                }
                user.actionPoint -= actionCost;
            }
            else {
                Console.WriteLine("Insufficient AP. Turn wasted.");
            }
        }
        public virtual void Attack(Enemy user, Player target, int damage) {
            if(!target.guarded) {
                target.Health -= (damage + (2 * user.GetDamage()));
            }
        }
        public void Guard(Player user) {
            if(user.actionPoint >= actionCost) {
                user.guarded = true;
                user.actionPoint -= actionCost;
            }
            else {
                Console.WriteLine("Insufficient AP. Turn wasted.");
            }
        }
        public void Guard(Enemy user) {
            user.guarded = true;
        }
        public void Heal(Player user) {
            if(user.actionPoint >= actionCost) {
                user.Health += (2 + user.GetDamage());
                user.actionPoint -= actionCost;
                if(user.Health > user.GetMaxHealth()) {
                    user.Health = user.GetMaxHealth();
                }
            }
            else {
                Console.WriteLine("Insufficient AP. Turn wasted.");
            }
        }
        public void Heal(Enemy user) {
            user.Health += (2 + user.GetDamage());
            if(user.Health > user.GetMaxHealth()) {
                user.Health = user.GetMaxHealth();
            }
        }
    }
    class PlayerAction : Action {
        public int actionID;
        public PlayerAction(string name, int damage, int actionCost, int actionID) {
            this.damage = damage;
            this.name = name;
            this.actionCost = actionCost;
            this.actionID = actionID;
        }
        public override void Attack(Player user, Enemy target, int damage) {
            base.Attack(user, target, damage);
        }
    }
    class EnemyAction : Action {
        public int actionID;
        public EnemyAction(string name, int damage, int actionID) {
            this.name = name;
            this.damage = damage;
            this.actionID = actionID;
        }
        public override void Attack(Enemy user, Player target, int damage) {
            base.Attack(user, target, damage);
        }
    }

    class Program {
        static void PrintTextSlowish(string input, int timebetween) {
            List<char> charList = input.ToList<char>();
            foreach(char character in charList) {
                Console.Write(character);
                System.Threading.Thread.Sleep(timebetween);
            }
        }
        static void Fight(Player player, Random rand, List<Enemy> enemies, int atkRangeMin, int atkRangeMax, List<PlayerAction> playerActions, List<EnemyAction> enemyActions, bool playerWin, int selectedEnemy, string menuAction) {
            Console.WriteLine($"Now engaging in battle against {enemies[selectedEnemy].GetName()}!");
            Console.WriteLine("\nPress any key to begin the fight...");
            Console.ReadKey(true);
            while(!playerWin) {
                menuAction = "0";
                while(menuAction != "1" && menuAction != "2" && menuAction != "3" && menuAction != "4") {
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine($"Battle: {player.GetName()} vs {enemies[selectedEnemy].GetName()}!\n");
                    Console.WriteLine($"     You:    {enemies[selectedEnemy].GetName()}:");
                    if(player.Health >= 10 || enemies[selectedEnemy].Health >= 10) {
                        Console.WriteLine($"HP:  {player.Health}/{player.GetMaxHealth()}   {enemies[selectedEnemy].Health}/{enemies[selectedEnemy].GetMaxHealth()}");
                    }
                    else if(player.Health >= 10 || enemies[selectedEnemy].Health < 10) {
                        Console.WriteLine($"HP:  {player.Health}/{player.GetMaxHealth()}    {enemies[selectedEnemy].Health}/{enemies[selectedEnemy].GetMaxHealth()}");
                    }
                    else if(player.Health < 10 || enemies[selectedEnemy].Health >= 10) {
                        Console.WriteLine($"HP:  {player.Health}/{player.GetMaxHealth()}    {enemies[selectedEnemy].Health}/{enemies[selectedEnemy].GetMaxHealth()}");
                    }
                    else if(player.Health < 10 || enemies[selectedEnemy].Health < 10) {
                        Console.WriteLine($"HP:  {player.Health}/{player.GetMaxHealth()}     {enemies[selectedEnemy].Health}/{enemies[selectedEnemy].GetMaxHealth()}");
                    }
                    Console.WriteLine($"ATK: {player.GetDamage()}       {enemies[selectedEnemy].GetDamage()}");
                    Console.WriteLine($"AP:  {player.actionPoint}\n");
                    Console.WriteLine("Available Actions:");
                    foreach(PlayerAction action in playerActions) {
                        if(!player.hasSharpStick && action.actionID == 2) {
                            Console.WriteLine("2 - [Do Nothing]");
                        }
                        else {
                            Console.WriteLine($"{action.actionID} - {action.GetName()}");
                        }
                    }
                    Console.Write(">> ");
                    menuAction = Console.ReadLine()!;
                    if(menuAction == "1" && !enemies[selectedEnemy].guarded) {
                        playerActions[Convert.ToInt16(menuAction)-1].Attack(player, enemies[selectedEnemy], playerActions[Convert.ToInt16(menuAction)-1].GetDamage());
                        Console.WriteLine($"{player.GetName()} uses {playerActions[Convert.ToInt16(menuAction)-1].GetName()}, dealing {playerActions[Convert.ToInt16(menuAction)-1].GetDamage() + (2 * player.GetDamage())} damage to {enemies[selectedEnemy].GetName()}!");
                    }
                    else if(menuAction == "2" && player.hasSharpStick && !enemies[selectedEnemy].guarded) {
                        playerActions[Convert.ToInt16(menuAction)-1].Attack(player, enemies[selectedEnemy], playerActions[Convert.ToInt16(menuAction)-1].GetDamage());
                        Console.WriteLine($"{player.GetName()} uses {playerActions[Convert.ToInt16(menuAction)-1].GetName()}, dealing {playerActions[Convert.ToInt16(menuAction)-1].GetDamage() + (2 * player.GetDamage())} damage to {enemies[selectedEnemy].GetName()}!");
                    }
                    else if(menuAction == "2" && !player.hasSharpStick) {
                        Console.WriteLine($"{player.GetName()} does nothing this round.");
                    }
                    else if(menuAction == "3") {
                        playerActions[Convert.ToInt16(menuAction)-1].Guard(player);
                        Console.WriteLine($"{player.GetName()} is guarding!");
                    }
                    else if(menuAction == "4") {
                        playerActions[Convert.ToInt16(menuAction)-1].Heal(player);
                        Console.WriteLine($"{player.GetName()} heals back up to {player.Health} HP!");
                    }
                    else if(enemies[selectedEnemy].guarded) {
                        Console.WriteLine($"{enemies[selectedEnemy].GetName()} was guarded. The attack failed!");
                    }
                    else {
                        Console.WriteLine("Invalid Move.");
                    }
                    if(enemies[selectedEnemy].Health <= 0) {
                        Console.WriteLine($"\n{player.GetName()} wins the battle!");
                        enemies[selectedEnemy].ResetHealth();
                        player.ResetHealth();
                        player.actionPoint = 9;
                        playerWin = true;
                        break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                enemies[selectedEnemy].guarded = false;
                if(!playerWin) {
                    int randomNumber = rand.Next(atkRangeMin, atkRangeMax+1);
                    if(enemyActions[randomNumber].actionID != 3 && enemyActions[randomNumber].actionID != 4 && !player.guarded) {
                        enemyActions[randomNumber].Attack(enemies[selectedEnemy], player, enemyActions[randomNumber].GetDamage());
                        Console.WriteLine($"{enemies[selectedEnemy].GetName()} uses {enemyActions[randomNumber].GetName()}, dealing {enemyActions[randomNumber].GetDamage() + (2 * enemies[selectedEnemy].GetDamage())} damage to {player.GetName()}");
                    }
                    else if(enemyActions[randomNumber].actionID == 3) {
                        enemyActions[randomNumber].Guard(enemies[selectedEnemy]);
                        Console.WriteLine($"{enemies[selectedEnemy].GetName()} is guarding!");
                    }
                    else if(enemyActions[randomNumber].actionID == 4) {
                        enemyActions[randomNumber].Heal(enemies[selectedEnemy]);
                        Console.WriteLine($"{enemies[selectedEnemy].GetName()} heals back up to {enemies[selectedEnemy].Health} HP!");
                    }
                    else {
                        Console.WriteLine($"{player.GetName()} was guarded. The attack failed!");
                    }
                    if(player.Health <= 0) {
                        Console.Write($"\n{player.GetName()} gets heavily injured by the {enemies[selectedEnemy].GetName()} ");
                        Console.WriteLine($"and goes home to avoid the situation.");
                        if(selectedEnemy == 2) {
                            PrintTextSlowish(@"Sadly, you could not defeat the Tuna Casserole. It was an impossible task. And then you woke up
and realized that it was all a dream. Everything was as it should be.", 45);
                            Console.WriteLine("\n\n\n");
                            PrintTextSlowish("The End.", 250);
                        }
                        while(true) {}
                    }
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                player.guarded = false;
                player.actionPoint++;
            }
            player.enemiesDefeated++;
        }
        static void Main() {
            bool playerWin = false;
            int textspeed = 45;
            string menuAction;
            int selectedEnemy;
            int atkRangeMin;
            int atkRangeMax;
            int maxNormEnemies;
            Random rand = new Random();
            try {
                Console.Write("Insert Max Enemies to Fight: ");
                maxNormEnemies = Convert.ToInt16(Console.ReadLine());
            }
            catch(System.FormatException) {
                Console.WriteLine("Invalid Number Provided. Defaulting to 5");
                maxNormEnemies = 5;
            }

            //Instantiate the Player:
            string playername = "";
            Console.Write("Insert Player Name: ");
            playername = Console.ReadLine()!;
            Player player = new Player(playername, 20, 1, 10);

            //Instantiate the Enemies:
            Enemy potato = new Enemy("Potato", 5, 1, 1);
            Enemy potatoSoldier = new Enemy("Potato Soldier", 8, 3, 2);
            Enemy tunaCasserole = new Enemy("Tuna Casserole", 50, 3, 3);
            Enemy potatoKing = new Enemy("Potato King", 15, 3, 4);
            List<Enemy> enemies = new List<Enemy> {potato, potatoSoldier, tunaCasserole, potatoKing};

            //Instantiate the Actions:
            PlayerAction PlayerKick = new PlayerAction("[Kick]", 0, 0, 1);
            PlayerAction PlayerStab = new PlayerAction("[Stab]", 3, 3, 2);
            PlayerAction PlayerGuard = new PlayerAction("[Guard]", 0, 2, 3);
            PlayerAction PlayerHeal = new PlayerAction("[Heal]", 0, 4, 4);
            List<PlayerAction> playerActions = new List<PlayerAction> {PlayerKick, PlayerStab, PlayerGuard, PlayerHeal};

            EnemyAction EnemySlash = new EnemyAction("[Slash]", 2, 1);
            EnemyAction EnemyScratch = new EnemyAction("[Scratch]", 1, 2);
            EnemyAction EnemyGuard = new EnemyAction("[Guard]", 0, 3);
            EnemyAction EnemyHeal = new EnemyAction("[Heal]", 0, 4);
            EnemyAction EnemyCasserole = new EnemyAction("[Tuna Casserole]", 4, 5);
            List<EnemyAction> enemyActions = new List<EnemyAction> {EnemySlash, EnemyScratch, EnemyGuard, EnemyHeal, EnemyCasserole};
            List<EnemyAction> casseroleActions = new List<EnemyAction> {EnemyGuard, EnemyCasserole};

            //Start the Game:
            Console.WriteLine("");
            PrintTextSlowish(@"You are an ordinary person, living in an ordinary region of Idaho, when suddenly, all of the 
potatoes come to life. At first, all was fine, but eventually, the potatoes were tired of
being eaten and have decided to rebel against the humans. They have formed a hierarchy, and
they have the grand and evil (yet localized) Potato King! It is up to you to stop this menace,
before your neighbourhood is taken over by the tuber tyrant.", textspeed);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine("\n\n");

            bool option1try = false;
            bool option2try = false;
            bool option3try = false;
            while(true) {
                PrintTextSlowish("You are currently in your house.\n", textspeed);
                Console.WriteLine("Actions:\n[1] Open Pantry\n[2] Sleep\n[3] Look Under Bed\n[4] Go Outside");
                Console.Write(">> ");
                menuAction = Console.ReadLine()!;
                if(menuAction != "1" && menuAction != "2" && menuAction != "3" && menuAction != "4") {
                    Console.WriteLine("There is no such action.");
                }
                else {
                    if(menuAction == "1") {
                        if(!option1try) {
                            Console.WriteLine("It's Empty.\n");
                            option1try = true;
                        }
                        else {
                            Console.WriteLine("It is still Empty.\n");
                        }
                    }
                    else if(menuAction == "2" && !player.hasSharpStick) {
                        Console.WriteLine("You are not tired.\n");
                        option2try = true;
                    }
                    else if(menuAction == "2" && player.hasSharpStick) {
                        Console.WriteLine("After getting the sharp stick, you are able to fall asleep somehow.\n");
                        option2try = true;
                        player.hasSlept = true;
                    }
                    else if(menuAction == "3") {
                        if(!option3try) {
                            if(option1try) {
                                Console.WriteLine("You didn't find anything.\n");
                            }
                            else if(option2try && !option1try) {
                                Console.WriteLine("You found a sharp stick. Your damage output increased by 3.\n");
                                player.AddCountToDamage(3);
                                player.hasSharpStick = true;
                            }
                            else {
                                Console.WriteLine("You didn't find anything.\n");
                            }
                            option3try = true;
                        }
                        else if(player.hasSharpStick) {
                            Console.WriteLine("You already took everything from here.\n");
                        }
                    }
                    else if(menuAction == "4") {
                        Console.WriteLine("You leave your house, and begin your quest to slay the Potato King.\nPress any key to continue...");
                        Console.ReadKey(true);
                        Console.WriteLine("\n");
                        break;
                    }
                }
            }

            PrintTextSlowish(@"As you are walking along the sidewalk, you encounter your first wild potato.
It doesn't seem to take kindly of your presence.", textspeed);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine("\n");
            selectedEnemy = 0;
            atkRangeMin = 0;
            atkRangeMax = 2;
            Fight(player, rand, enemies, atkRangeMin, atkRangeMax, playerActions, enemyActions, playerWin, selectedEnemy, menuAction);
            PrintTextSlowish($"That was a tough first battle. Fortunately, it resulted in a victory. Now you just need to defeat {maxNormEnemies - 1} more in order to get to the Potato King.", textspeed);
            Console.WriteLine("\n\n");
            while(player.enemiesDefeated < maxNormEnemies) {
                selectedEnemy = rand.Next(0,2);
                atkRangeMin = 0;
                atkRangeMax = rand.Next(0,4);
                Fight(player, rand, enemies, atkRangeMin, atkRangeMax, playerActions, enemyActions, playerWin, selectedEnemy, menuAction);
                Console.WriteLine($"\n{maxNormEnemies - player.enemiesDefeated} more enemies to defeat...\n");
            }
            PrintTextSlowish("You successfully made your way to the \"castle\" of the evil Potato King. You step inside and immediately begin fighting.", textspeed);
            Console.WriteLine("\n\n");
            Fight(player, rand, enemies, 0, 3, playerActions, enemyActions, playerWin, 3, menuAction);
            Console.WriteLine("\n\n");
            if(player.hasSlept) {
                PrintTextSlowish(@"After the rather challenging fight, you realize that this is not the end. Eating up the remains of the Potato King is
the strongest being known to mankind: Tuna Casserole! You don't think that you can win, but you cannot escape.
You must fight it.", textspeed);
                Console.WriteLine("\n\n");
                Fight(player, rand, enemies, 0, 1, playerActions, casseroleActions, playerWin, 2, menuAction);
                Console.WriteLine("You defeated the Tuna Casserole, and the world was saved. You wake up and realize that it was all a dream.");
                PrintTextSlowish("The End.", 250);
            }
            else {
                PrintTextSlowish("After the rather challenging fight, you go back home, knowing that the world has been saved. You take a nice rest.", textspeed);
                PrintTextSlowish("The End.", 250);
            }
            while(true) {}
        }
    }
}