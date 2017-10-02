using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent;
using FlexiCapture.Cloud.Portal.Api.DBHelpers.UoF.Interfaces;
using FlexiCapture.Cloud.Portal.Api.DB_PaymentAgent;

namespace FlexiCapture.Cloud.Portal.Api.DBHelpers.UoF.Repositories
{
    public class CardRepository : IRepository<Card>
    {
        private DB_PaymentAgent.PaymentAgentEntities Db;

        public CardRepository(DB_PaymentAgent.PaymentAgentEntities context)
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
