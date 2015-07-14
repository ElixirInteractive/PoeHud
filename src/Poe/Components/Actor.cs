using System.Collections.Generic;

namespace PoeHUD.Poe.Components
{
    public class Actor : Component
    {
        /// <summary>
        ///     Standing still = 2048 =bit 11 set
        ///     running = 2178 = bit 11 & 7
        ///     Maybe Bit-field : Bit 7 set = running
        /// </summary>
        public int ActionId
        {
            get
            {
                if (Address != 0)
                {
                    return M.ReadInt(Address + 0x7C);
                }
                return 1;
            }
        }

        public bool isMoving
        {
            get { return (ActionId & 128) > 0; }
        }

        public List<int> Minions
        {
            get
            {
                var list = new List<int>();
                if (Address == 0)
                {
                    return list;
                }
                int num = M.ReadInt(Address + 0x278);
                int num2 = M.ReadInt(Address + 0x27C);
                for (int i = num; i < num2; i += 8)
                {
                    int item = M.ReadInt(i);
                    list.Add(item);
                }
                return list;
            }
        }

        public bool HasMinion(Entity entity)
        {
            if (Address == 0)
            {
                return false;
            }
            int num = M.ReadInt(Address + 0x278);
            int num2 = M.ReadInt(Address + 0x27C);
            for (int i = num; i < num2; i += 8)
            {
                int num3 = M.ReadInt(i);
                if (num3 == entity.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}