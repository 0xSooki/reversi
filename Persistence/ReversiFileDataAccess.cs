namespace Persistence
{
    public class ReversiFileDataAccess : IReversiDataAccess
    {
        private String? _directory = String.Empty;

        public ReversiFileDataAccess(String? saveDirectory = null)
        {
            _directory = saveDirectory;
        }
        public async Task<(ReversiTable, Int32, Int32, Int32)> LoadAsync(String path)
        {
            if (!String.IsNullOrEmpty(_directory))
                path = Path.Combine(_directory, path);

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = await reader.ReadLineAsync() ?? String.Empty;
                    Int32[] nums = line.Split(' ').Select(Int32.Parse).ToArray();
                    Int32 tableSize = (nums[0]);
                    ReversiTable table = new ReversiTable(tableSize);

                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        String[] numbers = line.TrimEnd().Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            table.SetValue(i, j, Int32.Parse(numbers[j]));
                        }
                    }

                    return (table, nums[1], nums[2], nums[3]);
                }
            }
            catch
            {
                throw new ReversiDataException();
            }
        }

        public async Task SaveAsync(String path, ReversiTable table, int currentPlayer, int p1Time, int gameTime)
        {
            if (!String.IsNullOrEmpty(_directory))
                path = Path.Combine(_directory, path);

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(table.Size + " ");
                    writer.Write(gameTime + " ");
                    writer.Write(p1Time + " ");
                    writer.WriteLine(currentPlayer);
                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new ReversiDataException();
            }
        }
    }
}
