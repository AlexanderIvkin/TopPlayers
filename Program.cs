using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TopPlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int bestPlayersCount = 3;
            int[] levelsLimits = new int[] { 50, 80 };
            int[] powerLimits = new int[] { 1300, 3900 };
            PlayerFactory _playerFactory = new PlayerFactory(levelsLimits, powerLimits);
            List<string> names = new List<string>
            {
                "Бельзебуп", "Мандаринка999", "ФирстПлеер", "ТопЧарик", "ФростДамагер", "ЭниФриНикНейм", "Плохиш", "Кибальчиш", "ЧтоМолчишь", "Киписшъ"
            };
            TopPlayers topPlayers = new TopPlayers(_playerFactory.Create(names), bestPlayersCount);

            topPlayers.Execute();
        }
    }

    class TopPlayers
    {
        private List<Player> _players;
        private int _count;

        public TopPlayers(List<Player> players, int bestPlayersCount)
        {
            _players = players;
            _count = bestPlayersCount;
        }

        public void Execute()
        {
            Console.WriteLine("Обычный список:");
            ShowInfo(_players);
            Console.WriteLine($"\nТОП {_count} по уровню:");
            ShowInfo(SelectBestByLevel());
            Console.WriteLine($"\nТОП {_count} по силе:");
            ShowInfo(SelectBestByPower());
        }

        private List<Player> SelectBestByLevel()
        {
            return _players.OrderByDescending(player => player.Level).Take(_count).ToList();
        }
        
        private List<Player> SelectBestByPower()
        {
            return _players.OrderByDescending(player => player.Power).Take(_count).ToList();
        }

        private void ShowInfo(List<Player> players)
        {
            foreach(Player player in players)
            {
                player.ShowInfo();
            }
        }
    }

    class PlayerFactory
    {
        private int[] _levelsLimits;
        private int[] _powerLimits;

        public PlayerFactory(int[] levelsLimits, int[] powerLimits)
        {
            _levelsLimits = levelsLimits;
            _powerLimits = powerLimits;
        }

        public List<Player> Create(List<string> names)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < names.Count; i++)
            {
                players.Add(new Player(names[i],
                    UserUtills.GenerateNumberFromArrayLimits(_levelsLimits),
                    UserUtills.GenerateNumberFromArrayLimits(_powerLimits)));
            }

            return players;
        }
    }

    class Player
    {
        private string _name;

        public Player(string name, int level, int power)
        {
            _name = name;
            Level = level;
            Power = power;
        }

        public int Level { get; }
        public int Power { get; }

        public void ShowInfo()
        {
            Console.WriteLine($"Ник игрока: {_name}. Уровень: {Level}. Мощь: {Power}.");
        }
    }

    class UserUtills
    {
        private static Random s_random = new Random();

        public static int GenerateNumberFromArrayLimits(int[] limits)
        {
            Array.Sort(limits);

            return s_random.Next(limits[0], limits[1]);
        }
    }
}
