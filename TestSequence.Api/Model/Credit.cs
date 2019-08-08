using System;

namespace TestSequence.Api.Model
{
    public class Credit
    {
        public Guid Id { get; private set; }
        public string StoreId { get; private set; }
        public long CreditNumber { get; private set; }

        public Credit(Guid id, string storeId, long creditNumber)
        {
            Id = id;
            StoreId = storeId;
            CreditNumber = creditNumber;
        }
    }
}