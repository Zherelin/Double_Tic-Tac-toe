namespace LibraryOfPathPositions
{
    class PathPositions
    {
        public int Beginning { get; set; }
        public int Path { get; set; }

        public PathPositions(int beginning, int path)
        {
            Beginning = beginning;
            Path = path;
        }
    }
}
