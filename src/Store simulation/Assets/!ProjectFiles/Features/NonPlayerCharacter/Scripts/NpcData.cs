using System.Collections.Generic;

namespace NonPlayerCharacter
{
    public class NpcData
    {
        public int Balance = 1000;

        public Dictionary<int, int> DesiredProducts = new()
        {
            { 0, 10 },
        };
    }
}