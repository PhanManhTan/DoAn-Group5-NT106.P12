using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienLenDoAn
{
    public class Player1
    {
        // Token: 0x0600000C RID: 12 RVA: 0x000028C4 File Offset: 0x00000AC4
        public void SortCList(ref List<Card> CardList)
        {
            int num = CardList.Count<Card>();
            if (num > 1)
            {
                for (int i = 0; i < num - 1; i++)
                {
                    for (int j = i + 1; j < num; j++)
                    {
                        if (CardList[i].NameOfCard.GetHashCode() == CardList[j].NameOfCard.GetHashCode())
                        {
                            if (CardList[i].TypeOfCard.GetHashCode() > CardList[j].TypeOfCard.GetHashCode())
                            {
                                Card value = CardList[i];
                                CardList[i] = CardList[j];
                                CardList[j] = value;
                            }
                        }
                        else if (CardList[i].NameOfCard.GetHashCode() > CardList[j].NameOfCard.GetHashCode())
                        {
                            Card value = CardList[i];
                            CardList[i] = CardList[j];
                            CardList[j] = value;
                        }
                    }
                }
            }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002A20 File Offset: 0x00000C20
        public bool DoCheck(List<Card> PreMoveCards)
        {
            bool result;
            if (this.ChoseCard.Count<Card>() != 0)
            {
                this.SortCList(ref this.ChoseCard);
                this.SortCList(ref PreMoveCards);
                if (PreMoveCards.Count == 0)
                {
                    result = this.CheckValidFirstRound();
                }
                else
                {
                    result = this.CheckValidMidRound(PreMoveCards);
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002A80 File Offset: 0x00000C80
        public bool CheckSingle(Card a, Card b)
        {
            switch (this.CheckName(a, b))
            {
                case -1:
                    return false;
                case 0:
                    switch (this.CheckType(a, b))
                    {
                        case -1:
                            return false;
                        case 1:
                            return true;
                    }
                    break;
                case 1:
                    return true;
            }
            return true;
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002AE4 File Offset: 0x00000CE4
        private bool CheckCouple(Card a, Card b)
        {
            return a.NameOfCard.GetHashCode() == b.NameOfCard.GetHashCode();
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002B24 File Offset: 0x00000D24
        private bool CheckTriple(Card a, Card b, Card c)
        {
            return this.CheckCouple(a, b) && this.CheckCouple(b, c);
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002B54 File Offset: 0x00000D54
        private bool CheckSameFour(Card a, Card b, Card c, Card d)
        {
            return this.CheckCouple(a, b) && this.CheckCouple(b, c) && this.CheckCouple(c, d);
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002B90 File Offset: 0x00000D90
        private bool CheckContinuous(Card a, Card b)
        {
            return b.NameOfCard.GetHashCode() == a.NameOfCard.GetHashCode() + 1;
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002BD4 File Offset: 0x00000DD4
        private bool CheckListContinuous(List<Card> list)
        {
            bool result;
            if (list[list.Count - 1].NameOfCard.GetHashCode() == 15)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (!this.CheckContinuous(list[i], list[i + 1]))
                    {
                        return false;
                    }
                }
                result = true;
            }
            return result;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002C48 File Offset: 0x00000E48
        private bool CheckX_Con_Coup(List<Card> list)
        {
            int i = 0;
            bool result;
            if (list[list.Count - 1].NameOfCard.GetHashCode() == 15)
            {
                result = false;
            }
            else
            {
                while (i < list.Count<Card>() - 3)
                {
                    if (!this.CheckCouple(list[i], list[i + 1]) || !this.CheckContinuous(list[i], list[i + 2]))
                    {
                        return false;
                    }
                    i += 2;
                }
                result = this.CheckCouple(list[list.Count<Card>() - 2], list[list.Count<Card>() - 1]);
            }
            return result;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002D04 File Offset: 0x00000F04
        public int CheckName(Card a, Card b)
        {
            int result;
            if (a.NameOfCard.GetHashCode() > b.NameOfCard.GetHashCode())
            {
                result = 1;
            }
            else if (a.NameOfCard.GetHashCode() == b.NameOfCard.GetHashCode())
            {
                result = 0;
            }
            else if (a.NameOfCard.GetHashCode() < b.NameOfCard.GetHashCode())
            {
                result = -1;
            }
            else
            {
                result = -2;
            }
            return result;
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002DA0 File Offset: 0x00000FA0
        public int CheckType(Card a, Card b)
        {
            int result;
            if (a.TypeOfCard.GetHashCode() > b.TypeOfCard.GetHashCode())
            {
                result = 1;
            }
            else if (a.TypeOfCard.GetHashCode() < b.TypeOfCard.GetHashCode())
            {
                result = -1;
            }
            else
            {
                result = -2;
            }
            return result;
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002E10 File Offset: 0x00001010
        public bool CheckValidFirstRound()
        {
            int num = this.ChoseCard.Count<Card>();
            bool result;
            if (num == 1)
            {
                result = true;
            }
            else if (num == 3 && this.CheckTriple(this.ChoseCard[0], this.ChoseCard[1], this.ChoseCard[2]))
            {
                result = true;
            }
            else
            {
                if (num % 2 == 0)
                {
                    if (num == 2)
                    {
                        return this.CheckCouple(this.ChoseCard[0], this.ChoseCard[1]);
                    }
                    if (num == 4 && this.CheckSameFour(this.ChoseCard[0], this.ChoseCard[1], this.ChoseCard[2], this.ChoseCard[3]))
                    {
                        return true;
                    }
                    if (num >= 6 && this.CheckX_Con_Coup(this.ChoseCard))
                    {
                        return true;
                    }
                }
                result = this.CheckListContinuous(this.ChoseCard);
            }
            return result;
        }

        // Token: 0x06000018 RID: 24 RVA: 0x00002F48 File Offset: 0x00001148
        public bool CheckValidMidRound(List<Card> PreMoveCards)
        {
            if (PreMoveCards[PreMoveCards.Count - 1].NameOfCard.GetHashCode() == 15)
            {
                if (PreMoveCards.Count == 1)
                {
                    if (this.ChoseCard.Count == 4)
                    {
                        return this.CheckSameFour(this.ChoseCard[0], this.ChoseCard[1], this.ChoseCard[2], this.ChoseCard[3]);
                    }
                    if (this.ChoseCard.Count >= 6)
                    {
                        return this.CheckX_Con_Coup(this.ChoseCard);
                    }
                }
                else if (PreMoveCards.Count == 2 && this.ChoseCard.Count >= 8)
                {
                    return this.CheckX_Con_Coup(this.ChoseCard);
                }
            }
            if (this.ChoseCard.Count == PreMoveCards.Count)
            {
                int num = PreMoveCards.Count<Card>();
                if (num == 1)
                {
                    return this.CheckSingle(this.ChoseCard[0], PreMoveCards[0]);
                }
                if (num == 3 && this.CheckTriple(PreMoveCards[0], PreMoveCards[1], PreMoveCards[2]) && this.CheckTriple(this.ChoseCard[0], this.ChoseCard[1], this.ChoseCard[2]) && this.CheckSingle(this.ChoseCard[this.ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                {
                    return true;
                }
                if (num % 2 == 0)
                {
                    if (num == 2)
                    {
                        if (this.CheckCouple(PreMoveCards[0], PreMoveCards[1]) && this.CheckCouple(this.ChoseCard[0], this.ChoseCard[1]) && this.CheckSingle(this.ChoseCard[this.ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                        {
                            return true;
                        }
                    }
                    if (num == 4 && this.CheckSameFour(PreMoveCards[0], PreMoveCards[1], PreMoveCards[2], PreMoveCards[3]) && this.CheckSameFour(this.ChoseCard[0], this.ChoseCard[1], this.ChoseCard[2], this.ChoseCard[3]) && this.CheckSingle(this.ChoseCard[this.ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                    {
                        return true;
                    }
                    if (num >= 6 && this.CheckX_Con_Coup(PreMoveCards) && this.CheckX_Con_Coup(this.ChoseCard) && this.CheckSingle(this.ChoseCard[this.ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                    {
                        return true;
                    }
                }
                if (this.CheckListContinuous(PreMoveCards) && PreMoveCards.Count > 2 && this.CheckListContinuous(this.ChoseCard))
                {
                    return this.CheckSingle(this.ChoseCard[this.ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]);
                }
            }
            return false;
        }

        // Token: 0x06000019 RID: 25 RVA: 0x00003338 File Offset: 0x00001538
        public void FindValidCardtoPlay(List<Card> PreMoveCards)
        {
            if (PreMoveCards.Count<Card>() == 1)
            {
                for (int i = 0; i < this.PlayerCard.Count<Card>(); i++)
                {
                    this.ChoseCard.Add(this.PlayerCard[i]);
                    if (this.DoCheck(PreMoveCards))
                    {
                        break;
                    }
                    this.ChoseCard.Clear();
                }
            }
            else
            {
                for (int i = 0; i < this.PlayerCard.Count<Card>() - PreMoveCards.Count<Card>(); i++)
                {
                    int num = i;
                    for (int j = 1; j <= PreMoveCards.Count<Card>(); j++)
                    {
                        this.ChoseCard.Add(this.PlayerCard[num]);
                        num++;
                    }
                    if (this.DoCheck(PreMoveCards))
                    {
                        break;
                    }
                    this.ChoseCard.Clear();
                }
            }
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00003424 File Offset: 0x00001624
        public void FindFirstValidCardstoPlay()
        {
            for (int i = 0; i < this.PlayerCard.Count - 2; i++)
            {
                this.ChoseCard.Add(this.PlayerCard[i]);
                for (int j = i + 1; j < this.PlayerCard.Count - 1; j++)
                {
                    if (this.CheckContinuous(this.ChoseCard[this.ChoseCard.Count - 1], this.PlayerCard[j]))
                    {
                        this.ChoseCard.Add(this.PlayerCard[j]);
                        i = j;
                        break;
                    }
                }
                this.ChoseCard.Clear();
            }
        }

        // Token: 0x0400000C RID: 12
        public List<Card> PlayerCard = new List<Card>();

        // Token: 0x0400000D RID: 13
        public List<Card> ChoseCard = new List<Card>();
    }
}

