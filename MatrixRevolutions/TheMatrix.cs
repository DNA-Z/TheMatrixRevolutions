using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixRevolutions
{
    public class TheMatrix
    {
        static readonly object locker = new object();
        Random random;
        IEnumerable<char> symbols = Enumerable.Range(48, 58).Union(Enumerable.Range(65, 91)).Select(x => (char)(x));

        public int Column { get; set; }
        public bool IsNeedSecondChain { get; set; } = true;

        public TheMatrix() { }

        public TheMatrix(int column, bool isNeedSecondChain)
        {
            this.random = new Random((int)DateTime.Now.Ticks);
            Column = column;
            IsNeedSecondChain = isNeedSecondChain;
        }

        private char GetSymbols()
        {
            string symbolsStr = string.Join("", symbols);
            return symbolsStr.ToCharArray()[random.Next(0, 35)];
        }

        public void FallingSymbols()
        {
            int currentChainLength;
            int maxChainLength;

            while (true)
            {
                currentChainLength = 0;
                maxChainLength = random.Next(3, 10);
                Thread.Sleep(random.Next(20, 5000));

                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    lock (locker)
                    {
                        Console.CursorTop = 0;
                        Console.ForegroundColor = ConsoleColor.Black;

                        PaintOverSymbol(i);

                        if (currentChainLength < maxChainLength)
                            currentChainLength++;
                        else
                            maxChainLength = 0;

                        GetSecondChain(currentChainLength, i);

                        if (Console.WindowHeight < currentChainLength)
                            currentChainLength--;

                        Console.CursorTop = i - currentChainLength + 1;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        PaintSymbol(currentChainLength);
                        Thread.Sleep(20);
                    }
                }
            }
        }

        private void PaintOverSymbol(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.CursorLeft = Column;
                Console.WriteLine("█");
            }
        }

        private void PaintSymbol(int lenght)
        {
            for (int j = 0; j < lenght - 2; j++)
            {
                Console.CursorLeft = Column;
                Console.WriteLine(GetSymbols());
            }
            if (lenght >= 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.CursorLeft = Column;
                Console.WriteLine(GetSymbols());
            }
            if (lenght >= 1)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorLeft = Column;
                Console.WriteLine(GetSymbols());
            }
            //switch (lenght)
            //{
            //    case 1:
            //        Console.ForegroundColor = ConsoleColor.White;
            //        Console.CursorLeft = Column;
            //        Console.WriteLine(GetSymbols());
            //        break;
            //    case 2:
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.CursorLeft = Column;
            //        Console.WriteLine(GetSymbols());
            //        break;
            }

        private void GetSecondChain(int lenght, int count)
        {
            if (IsNeedSecondChain && count < Console.WindowHeight / 2 && count > lenght + 2 && (random.Next(1, 5) == 3))
            {
                new Thread((new TheMatrix(Column, false)).FallingSymbols).Start();
                IsNeedSecondChain = false;
            }
        }
    }
}
    

        
    
