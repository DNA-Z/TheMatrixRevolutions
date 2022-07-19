using MatrixRevolutions;


TheMatrix matrix;

for (int i = 0; i < Console.WindowWidth / 2; i++)
{
    matrix = new TheMatrix(i * 2, true);
    new Thread(matrix.FallingSymbols).Start();
}


Console.ReadKey();