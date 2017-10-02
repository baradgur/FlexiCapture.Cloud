using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.PaymentAssist.DB;

namespace FlexiCapture.Cloud.PaymentAssist.DBHelpers
{
    public static class CardHelper
    {
        public static IEnumerable<Card> GetAllCards()
        {
            try
            {
                using (PaymentAgentEntities db = new PaymentAgentEntities())
                {
                    return db.Card;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Card GetCardById(int cardId)
        {
            try
            {
                using (PaymentAgentEntities db = new PaymentAgentEntities())
                {
                    return db.Card
                        .SingleOrDefault(x => 
                           x.CardID.Equals(cardId));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
