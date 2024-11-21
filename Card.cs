using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Card
    {
        public enum TypeofCard
        {
            // Token: 0x0400007A RID: 122
            Heart = 4,
            // Token: 0x0400007B RID: 123
            Diamond = 3,
            // Token: 0x0400007C RID: 124
            Club = 2,
            // Token: 0x0400007D RID: 125
            Spade = 1
        }
        // Token: 0x02000026 RID: 38
        public enum NameofCard
        {
            // Token: 0x0400007F RID: 127
            Three = 3,
            // Token: 0x04000080 RID: 128
            Four  = 4,
            // Token: 0x04000081 RID: 129
            Five = 5,
            // Token: 0x04000082 RID: 130
            Six = 6,
            // Token: 0x04000083 RID: 131
            Seven = 7,
            // Token: 0x04000084 RID: 132
            Eight = 8,
            // Token: 0x04000085 RID: 133
            Nine = 9,
            // Token: 0x04000086 RID: 134
            Ten = 10,
            // Token: 0x04000087 RID: 135
            Jack = 11,
            // Token: 0x04000088 RID: 136
            Queen = 12,
            // Token: 0x04000089 RID: 137
            King = 13,
            // Token: 0x0400008A RID: 138
            Ace = 14,
            // Token: 0x0400008B RID: 139
            Two = 15,
        }
     
    }
}
