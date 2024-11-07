using System;
using MorseTranslator;
using TicTacToeGame;

namespace TicTacToeGame
{
    class Program
    {
        static void Main()
        {
            //Console.WriteLine("Добро пожаловать в игру Крестики-Нолики!");
            //Console.WriteLine("Выберите режим игры:");
            //Console.WriteLine("1 - Игра с компьютером");
            //Console.WriteLine("2 - Игра с другим игроком");

            //int choice = int.Parse(Console.ReadLine());
            //bool isPlayingWithComputer = (choice == 1);

            //Game game = new Game(isPlayingWithComputer);
            //game.Start();

            Console.WriteLine("1 - Перевести текст в азбуку Морзе");
            Console.WriteLine("2 - Перевести азбуку Морзе в текст");
           int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("Введите текст для перевода в азбуку Морзе:");
                string text = Console.ReadLine();
                Console.WriteLine(MorseTranslator.MorseTranslator.ToMorse(text));
            }
            else if (choice == 2)
            {
                Console.WriteLine("Введите азбуку Морзе для перевода в текст (разделите символы пробелами):");
                string morse = Console.ReadLine();
                Console.WriteLine(MorseTranslator.MorseTranslator.ToText(morse));
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
        }
    }
}
namespace TicTacToeGame
{
    public class Game
    {
        private char[,] board = new char[3, 3];
        private Random random = new Random();
        private bool isPlayingWithComputer;
        private char currentPlayer;
        private Players.Human playerX;
        private object playerO;

        public Game(bool isPlayingWithComputer)
        {
            this.isPlayingWithComputer = isPlayingWithComputer;
            this.currentPlayer = 'X';
            this.playerX = new Players.Human('X');
            this.playerO = isPlayingWithComputer ? (object)new Players.Computer('O', random) : new Players.Human('O');
            InitializeBoard();
        }

        public void Start()
        {
            DisplayBoard();

            while (true)
            {
                if (currentPlayer == 'X')
                {
                    playerX.MakeMove(board);
                }
                else
                {
                    if (playerO is Players.Human humanPlayerO)
                    {
                        humanPlayerO.MakeMove(board);
                    }
                    else if (playerO is Players.Computer computerPlayerO)
                    {
                        computerPlayerO.MakeMove(board);
                    }
                }

                DisplayBoard();

                if (CheckWinner('X'))
                {
                    Console.WriteLine("Поздравляем, вы победили!");
                    break;
                }
                if (CheckWinner('O'))
                {
                    Console.WriteLine(isPlayingWithComputer ? "Компьютер победил." : "Игрок O победил.");
                    break;
                }

                if (IsBoardFull())
                {
                    Console.WriteLine("Ничья.");
                    break;
                }

                SwitchPlayer();
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '.';
                }
            }
        }

        private void DisplayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        private bool CheckWinner(char player)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                    (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                    return true;
            }

            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == '.')
                    return false;
            }
            return true;
        }
    }
}
namespace TicTacToeGame.Players
{
    public class Human
    {
        public char Symbol { get; }

        public Human(char symbol)
        {
            Symbol = symbol;
        }

        public void MakeMove(char[,] board)
        {
            int row, col;
            while (true)
            {
                Console.WriteLine("Ваш ход. Введите строку и столбец (0-2): ");
                row = int.Parse(Console.ReadLine());
                col = int.Parse(Console.ReadLine());

                if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == '.')
                {
                    board[row, col] = Symbol;
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ход, попробуйте снова.");
                }
            }
        }
    }

    public class Computer
    {
        public char Symbol { get; }
        private Random random;

        public Computer(char symbol, Random random)
        {
            Symbol = symbol;
            this.random = random;
        }

        public void MakeMove(char[,] board)
        {
            Console.WriteLine("Ход компьютера...");
            int row, col;
            while (true)
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);

                if (board[row, col] == '.')
                {
                    board[row, col] = Symbol;
                    break;
                }
            }
        }
    }
}
namespace MorseTranslator
{
    public static class MorseTranslator
    {
        public static string ToMorse(string text)
        {
            text = text.ToUpper();
            string morseCode = string.Empty;

            foreach (char c in text)
            {
                if (c == 'A') morseCode += ".- ";
                else if (c == 'B') morseCode += "-... ";
                else if (c == 'C') morseCode += "-.-. ";
                else if (c == 'D') morseCode += "-.. ";
                else if (c == 'E') morseCode += ". ";
                else if (c == 'F') morseCode += "..-. ";
                else if (c == 'G') morseCode += "--. ";
                else if (c == 'H') morseCode += ".... ";
                else if (c == 'I') morseCode += ".. ";
                else if (c == 'J') morseCode += ".--- ";
                else if (c == 'K') morseCode += "-.- ";
                else if (c == 'L') morseCode += ".-.. ";
                else if (c == 'M') morseCode += "-- ";
                else if (c == 'N') morseCode += "-. ";
                else if (c == 'O') morseCode += "--- ";
                else if (c == 'P') morseCode += ".--. ";
                else if (c == 'Q') morseCode += "--.- ";
                else if (c == 'R') morseCode += ".-. ";
                else if (c == 'S') morseCode += "... ";
                else if (c == 'T') morseCode += "- ";
                else if (c == 'U') morseCode += "..- ";
                else if (c == 'V') morseCode += "...- ";
                else if (c == 'W') morseCode += ".-- ";
                else if (c == 'X') morseCode += "-..- ";
                else if (c == 'Y') morseCode += "-.-- ";
                else if (c == 'Z') morseCode += "--.. ";
                else if (c == '1') morseCode += ".---- ";
                else if (c == '2') morseCode += "..--- ";
                else if (c == '3') morseCode += "...-- ";
                else if (c == '4') morseCode += "....- ";
                else if (c == '5') morseCode += "..... ";
                else if (c == '6') morseCode += "-.... ";
                else if (c == '7') morseCode += "--... ";
                else if (c == '8') morseCode += "---.. ";
                else if (c == '9') morseCode += "----. ";
                else if (c == '0') morseCode += "----- ";
                else if (c == '.') morseCode += ".-.-.- ";
                else if (c == ',') morseCode += "--..-- ";
                else if (c == '?') morseCode += "..--.. ";
                else if (c == ' ') morseCode += "  ";

            }

            return morseCode.Trim();
        }

        public static string ToText(string morse)
        {
            string[] morseWords = morse.Split(new string[] { "   " }, StringSplitOptions.None);
            string text = string.Empty;

            foreach (string morseWord in morseWords)
            {
                string[] morseLetters = morseWord.Split(' ');
                foreach (string morseLetter in morseLetters)
                {
                    if (morseLetter == ".-") text += "A";
                    else if (morseLetter == "-...") text += "B";
                    else if (morseLetter == "-.-.") text += "C";
                    else if (morseLetter == "-..") text += "D";
                    else if (morseLetter == ".") text += "E";
                    else if (morseLetter == "..-.") text += "F";
                    else if (morseLetter == "--.") text += "G";
                    else if (morseLetter == "....") text += "H";
                    else if (morseLetter == "..") text += "I";
                    else if (morseLetter == ".---") text += "J";
                    else if (morseLetter == "-.-") text += "K";
                    else if (morseLetter == ".-..") text += "L";
                    else if (morseLetter == "--") text += "M";
                    else if (morseLetter == "-.") text += "N";
                    else if (morseLetter == "---") text += "O";
                    else if (morseLetter == ".--.") text += "P";
                    else if (morseLetter == "--.-") text += "Q";
                    else if (morseLetter == ".-.") text += "R";
                    else if (morseLetter == "...") text += "S";
                    else if (morseLetter == "-") text += "T";
                    else if (morseLetter == "..-") text += "U";
                    else if (morseLetter == "...-") text += "V";
                    else if (morseLetter == ".--") text += "W";
                    else if (morseLetter == "-..-") text += "X";
                    else if (morseLetter == "-.--") text += "Y";
                    else if (morseLetter == "--..") text += "Z";
                    else if (morseLetter == ".----") text += "1";
                    else if (morseLetter == "..---") text += "2";
                    else if (morseLetter == "...--") text += "3";
                    else if (morseLetter == "....-") text += "4";
                    else if (morseLetter == ".....") text += "5";
                    else if (morseLetter == "-....") text += "6";
                    else if (morseLetter == "--...") text += "7";
                    else if (morseLetter == "---..") text += "8";
                    else if (morseLetter == "----.") text += "9";
                    else if (morseLetter == "-----") text += "0";
                    else if (morseLetter == ".-.-.-") text += ".";
                    else if (morseLetter == "--..--") text += ",";
                    else if (morseLetter == "..--..") text += "?";
                }
                text += " ";
            }

            return text.Trim();
        }
    }
}
