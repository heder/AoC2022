class Program
{
    static FsEntry Root = new FsEntry(null, "/") { IsRoot = true, IsDir = true };
    static FsEntry CurrentDir = Root;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int i = 0;
        while (i < lines.Length)
        {
            var tokens = lines[i].Split(' ');

            if (tokens[0] == "$")
            {
                switch (tokens[1])
                {
                    case "cd":
                        switch (tokens[2])
                        {
                            case "/":
                                CurrentDir = Root;
                                break;

                            case "..":
                                CurrentDir = CurrentDir.Parent;
                                break;

                            default:
                                CurrentDir = CurrentDir.FsObjects.Single(f => f.Name == tokens[2]);
                                break;
                        }
                        break;

                    case "ls":

                        i++;

                        while (i < lines.Length && lines[i].StartsWith("$") == false)
                        {
                            var fstokens = lines[i].Split(' ');

                            switch (fstokens[0])
                            {
                                case "dir":
                                    CurrentDir.FsObjects.Add(new FsEntry(CurrentDir, fstokens[1]) { IsDir = true });
                                    break;

                                default:
                                    CurrentDir.FsObjects.Add(new FsEntry(CurrentDir, fstokens[1]) { IsDir = false, Size = Convert.ToInt32(fstokens[0]) });
                                    break;
                            }


                            i++;
                        }
                        continue;

                    default:
                        throw new Exception("Unknown command");
                }
            }

            i++;
        }

        int totalsize = TraverseFilesystem(Root, 1);

        Console.WriteLine(d);
        Console.ReadKey();
    }

    static int d = 0;

    private static int TraverseFilesystem(FsEntry e, int level)
    {
        Console.WriteLine($"{new string('-', level)} {e.Name} (dir)");

        int size = 0;
        foreach (var item in e.FsObjects)
        {
            if (item.IsDir)
            {
                int dirsize = TraverseFilesystem(item, level + 1);

                if (dirsize <= 100000)
                {
                    d += dirsize;
                }

                size += dirsize;
            }
            else
            {
                Console.WriteLine($"{new string('-', level + 1)} {item.Name} (file, {item.Size})");
                size += item.Size;
            }
        }

        return size;
    }

    class FsEntry
    {
        public FsEntry(FsEntry parent, string name)
        {
            Name = name;
            FsObjects = new List<FsEntry>();
            Parent = parent;
        }

        public FsEntry Parent { get; set; }

        public string Name { get; set; }

        public bool IsRoot { get; set; }
        public bool IsDir { get; set; }
        public int Size { get; set; }

        public List<FsEntry> FsObjects { get; set; }
    }
}