using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TienLenDoAn
{
    public class Player
    {
        #region [   -   Properties   -   ]

        public List<Card> PlayerCard = new List<Card>();
        public List<Card> ChoseCard = new List<Card>();
        
        #endregion

        #region [   -   Methods   -   ]

        public Player()
        { }

        public void SortCList(ref List<Card> CardList)
        {
            int i = CardList.Count();
            if (i > 1)
            {
                for (int m = 0; m < i - 1; m++)
                    for (int n = m + 1; n < i; n++)
                    {
                        if (CardList[m].NameOfCard.GetHashCode() == CardList[n].NameOfCard.GetHashCode())
                        {
                            if (CardList[m].TypeOfCard.GetHashCode() > CardList[n].TypeOfCard.GetHashCode())
                            {
                                Card temp = CardList[m];
                                CardList[m] = CardList[n];
                                CardList[n] = temp;
                            }
                        }
                        else
                            if (CardList[m].NameOfCard.GetHashCode() > CardList[n].NameOfCard.GetHashCode())
                            {
                                Card temp = CardList[m];
                                CardList[m] = CardList[n];
                                CardList[n] = temp;
                            }
                    }
            }
        }

        #endregion

        #region [   -   Check Card   -   ]

        public bool DoCheck(List<Card> PreMoveCards)
        {
            if (ChoseCard.Count() != 0)
            {
                SortCList(ref this.ChoseCard);
                SortCList(ref PreMoveCards);
                //begin round
                if (PreMoveCards.Count == 0)
                    return CheckValidFirstRound();

                //in mid round
                else
                    return CheckValidMidRound(PreMoveCards);
            }
            //non card chose
            else
                return false;
        }

        public bool CheckSingle(Card a, Card b)
        {
            switch (CheckName(a, b))
            {
                case 1:
                    return true;
                case -1:
                    return false;
                case 0:
                    {
                        switch (CheckType(a, b))
                        {
                            case 1:
                                return true;
                            case -1:
                                return false;
                        }
                        break;
                    }
            }
            return true;//just 4fun
        }

        private bool CheckCouple(Card a, Card b)
        {
            if (a.NameOfCard.GetHashCode() == b.NameOfCard.GetHashCode())
                return true;
            return false;
        }

        private bool CheckTriple(Card a, Card b, Card c)
        {
            if (!CheckCouple(a, b) || !CheckCouple(b, c))
                return false;
            return true;
        }

        private bool CheckSameFour(Card a, Card b, Card c, Card d)
        {
            if (!CheckCouple(a, b) || !CheckCouple(b, c) || !CheckCouple(c, d))
                return false;
            return true;
        }

        private bool CheckContinuous(Card a, Card b)
        {
            if (b.NameOfCard.GetHashCode() == a.NameOfCard.GetHashCode() + 1)
                return true;
            return false;
        }

        private bool CheckListContinuous(List<Card> list)
        {
            if (list[list.Count - 1].NameOfCard.GetHashCode() == 15)
                return false;//mean list end with Boss card, this is illegal
            //else
            for (int i = 0; i < list.Count -1; i++)
                if(!CheckContinuous(list [i], list[i +1]))
                    return false;
            return true;
        }

        private bool CheckX_Con_Coup(List<Card> list)
        {
            int i = 0;

            if (list[list.Count - 1].NameOfCard.GetHashCode() == 15)
                return false;//mean list end with Boss card, this is illegal
            //else
            while (i < list.Count() - 3)
            {
                if (!CheckCouple(list[i], list[i + 1]) || !CheckContinuous(list[i], list[i + 2]))
                    return false;
                i = i + 2;
            }
            //Check last two card
            if (!CheckCouple(list[list.Count() - 2], list[list.Count() - 1]))
                return false;
            return true;
        }

        public int CheckName(Card a, Card b)
        {
            if (a.NameOfCard.GetHashCode() > b.NameOfCard.GetHashCode())
                return 1;
            if (a.NameOfCard.GetHashCode() == b.NameOfCard.GetHashCode())
                return 0;
            if (a.NameOfCard.GetHashCode() < b.NameOfCard.GetHashCode())
                return -1;
            return -2;//just 4 fun
        }

        public int CheckType(Card a, Card b)
        {
            if (a.TypeOfCard.GetHashCode() > b.TypeOfCard.GetHashCode())
                return 1;
            if (a.TypeOfCard.GetHashCode() < b.TypeOfCard.GetHashCode())
                return -1;
            return -2;//just 4 fun
        }

        public bool CheckValidFirstRound()
        {
            int i = ChoseCard.Count();

            //chose only one card
            if (i == 1)
            {
                return true;
            }

            //chose three cards with the same NameOCard
            if (i == 3)
                if (CheckTriple(ChoseCard[0], ChoseCard[1], ChoseCard[2]))
                    return true;
            //chose even cards
            if (i % 2 == 0)
            {
                //chose two cards
                if (i == 2)
                {
                    //Couple
                    if (CheckCouple(ChoseCard[0], ChoseCard[1]))
                        return true;
                    return false;
                }

                //chose four cards with the same NameOCard
                if (i == 4)
                    //if same four card
                    if (CheckSameFour(ChoseCard[0], ChoseCard[1], ChoseCard[2], ChoseCard[3]))
                        return true;

                //chose triple-continuous-couple
                if (i >= 6)
                    if (CheckX_Con_Coup(ChoseCard))
                        return true;

            }//end of even card

            //chose continuous cards
            return CheckListContinuous(ChoseCard);
        }

        public bool CheckValidMidRound(List<Card> PreMoveCards)
        {
            #region Opponent play Boss card

            //Play Boss Card
            if (PreMoveCards[PreMoveCards.Count - 1].NameOfCard.GetHashCode() == 15)
            {
                //Play one Boss card
                if (PreMoveCards.Count == 1)
                {
                    //Same four card
                    if (ChoseCard.Count == 4)
                        return CheckSameFour(ChoseCard[0], ChoseCard[1], ChoseCard[2], ChoseCard[3]);
                    //Three or more continuous couple
                    else
                        if (ChoseCard.Count >= 6)
                            return CheckX_Con_Coup(ChoseCard);
                }

                //Play more than one Boss card
                else
                {
                    //Play Couple Boss card
                    if (PreMoveCards.Count == 2)
                        //Four or more continuous couple
                        if (ChoseCard.Count >= 8)
                            return CheckX_Con_Coup(ChoseCard);
                }
            }

            #endregion

            #region Opponent play normal card
            //ALGORITHM: CHECK WHAT card TYPE the OPPONENT PLAYED (single card/couple/triple...)
            //THEN CHECK if the PLAYER CHOSE THE SAME TYPE, JUST EXAMINE THE LAGEST CARD

            //Opponent played normal card, so, player must play card amount equal to opponent
            if (ChoseCard.Count == PreMoveCards.Count)
            {
                //Check opponent play single card, couple, triple .... 
                int i = PreMoveCards.Count();

                //chose only one card
                if (i == 1)
                    return CheckSingle(ChoseCard[0],PreMoveCards[0]);

                //chose three cards with the same NameOfCard
                if (i == 3)
                    if (CheckTriple(PreMoveCards[0], PreMoveCards[1], PreMoveCards[2]))
                        if (CheckTriple(ChoseCard[0], ChoseCard[1], ChoseCard[2]))
                            if (CheckSingle(ChoseCard[ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                                return true;
                //chose even cards
                if (i % 2 == 0)
                {
                    //chose two cards
                    if (i == 2)
                    {
                        //Couple
                        if (CheckCouple(PreMoveCards[0], PreMoveCards[1]))
                            if (CheckCouple(ChoseCard[0], ChoseCard[1]))
                                if (CheckSingle(ChoseCard[ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                                    return true;
                    }

                    //chose four cards with the same NameOCard
                    if (i == 4)
                        //if same four card
                        if (CheckSameFour(PreMoveCards[0], PreMoveCards[1], PreMoveCards[2], PreMoveCards[3]))
                            if (CheckSameFour(ChoseCard[0], ChoseCard[1], ChoseCard[2], ChoseCard[3]))
                                if (CheckSingle(ChoseCard[ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                                    return true;

                    //chose triple/tetra/penta/hexa-continuous-couple
                    if (i >= 6)
                        if (CheckX_Con_Coup(PreMoveCards))
                            if (CheckX_Con_Coup(ChoseCard))
                                if (CheckSingle(ChoseCard[ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]))
                                    return true;
                }//end of even card

                //chose continuous cards
                if (CheckListContinuous(PreMoveCards)&& PreMoveCards.Count >2)
                    if (CheckListContinuous(ChoseCard))
                        return CheckSingle(ChoseCard[ChoseCard.Count - 1], PreMoveCards[PreMoveCards.Count - 1]);
            }
            #endregion

            return false;
        }

        #endregion

        #region [   -   AI   -   ]

        //Methods for Player if Player is Computer
        public void FindValidCardtoPlay(List<Card> PreMoveCards)
        {
            Random rnd = new Random();

            // Nếu không có bài trước (nghĩa là lượt đầu tiên)
            if (PreMoveCards.Count() == 0)
            {
                // Random chọn kiểu bài để đánh
                int option = rnd.Next(0, 4); // 0: Đánh lẻ, 1: Đánh đôi, 2: Đánh sảnh, 3: Đánh bộ đặc biệt

                switch (option)
                {
                    case 0: // Đánh lẻ
                        for (int i = 0; i < PlayerCard.Count(); i++)
                        {
                            ChoseCard.Add(PlayerCard[i]);
                            return; // Chọn ngẫu nhiên 1 lá bài
                        }
                        break;

                    case 1: // Đánh đôi
                        for (int i = 0; i < PlayerCard.Count() - 1; i++)
                        {
                            if (CheckCouple(PlayerCard[i], PlayerCard[i + 1]))
                            {
                                ChoseCard.Add(PlayerCard[i]);
                                ChoseCard.Add(PlayerCard[i + 1]);
                                return;
                            }
                        }
                        break;

                    case 2: // Đánh sảnh
                        for (int i = 0; i < PlayerCard.Count() - 2; i++) // Phải có ít nhất 3 lá bài liên tiếp
                        {
                            if (CheckListContinuous(new List<Card> { PlayerCard[i], PlayerCard[i + 1], PlayerCard[i + 2] }))
                            {
                                ChoseCard.Add(PlayerCard[i]);
                                ChoseCard.Add(PlayerCard[i + 1]);
                                ChoseCard.Add(PlayerCard[i + 2]);
                                return;
                            }
                        }
                        break;

                    case 3: // Đánh bộ đặc biệt (bộ tứ)
                        for (int i = 0; i < PlayerCard.Count() - 3; i++)
                        {
                            if (CheckSameFour(PlayerCard[i], PlayerCard[i + 1], PlayerCard[i + 2], PlayerCard[i + 3]))
                            {
                                ChoseCard.Add(PlayerCard[i]);
                                ChoseCard.Add(PlayerCard[i + 1]);
                                ChoseCard.Add(PlayerCard[i + 2]);
                                ChoseCard.Add(PlayerCard[i + 3]);
                                return;
                            }
                        }
                        break;
                }
            }
            else // Nếu có bài của đối thủ, kiểm tra bài hợp lệ và chọn ngẫu nhiên
            {
                for (int i = 0; i < PlayerCard.Count() - PreMoveCards.Count(); i++)
                {
                    ChoseCard.Clear();
                    int k = i;
                    for (int j = 0; j < PreMoveCards.Count(); j++)
                    {
                        ChoseCard.Add(PlayerCard[k]);
                        k++;
                    }

                    if (DoCheck(PreMoveCards)) // Kiểm tra nếu bài hợp lệ
                    {
                        return;
                    }
                }
            }

            // Nếu không tìm được bài hợp lệ
            ChoseCard.Clear();
        }

        //UNDERCONSTRUCTION
        //if computer start round
        public void FindFirstValidCardstoPlay()
        {
            //Highest priority is Longest Continuos Cards
            for(int i = 0; i < PlayerCard.Count -2; i++ )// i < PlayerCard.Count -2 'cause shortest continuous list is 3
            {
                ChoseCard.Add(PlayerCard[i]);
                for (int j = i + 1; j < PlayerCard.Count - 1; j++)
                {
                    if (CheckContinuous(ChoseCard[ChoseCard.Count - 1], PlayerCard[j]))
                    {
                        ChoseCard.Add(PlayerCard[j]);
                        i = j;
                        j = i;
                        break;
                    }
                }
                ChoseCard.Clear();
            }
        }

        #endregion

    }
}