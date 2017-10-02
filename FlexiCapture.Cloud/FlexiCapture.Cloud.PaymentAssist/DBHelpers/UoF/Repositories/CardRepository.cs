using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.PaymentAssist.DB;
using FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Interfaces;

namespace FlexiCapture.Cloud.PaymentAssist.DBHelpers.UoF.Repositories
{
    public class CardRepository : IRepository<Card>
    {
        private DB.PaymentAgentEntities Db;

        public CardRepository(DB.PaymentAgentEntities context)
        {
            this.Db = context;
        }

        public IEnumerable<Card> GetAll()
        {
            return Db.Card;
        }

        public Card Get(int id)
        {
            return Db.Card.Find(id);
        }

        public Card Create(Card card)
        {
            Db.Card.Add(card);
            Db.SaveChanges();
            
            return card;
        }

        public void Update(Card card)
        {
           // Db.Entry(card).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Card card = Db.Card.Find(id);
            if (card != null)
                Db.Card.Remove(card);
        }
    }
}
