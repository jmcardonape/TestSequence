using System;

namespace TestSequence.Api.Model
{
    public class Sequence
    {
        public Guid Id { get; private set; }

        public long LastNumber { get; private set; }

        public string StoreId { get; private set; }

        public string Type { get; private set; }

        protected Sequence()
        {
        }

        public Sequence(Guid id, long lastNumber, string storeId, string type)
        {
            Id = id;
            LastNumber = lastNumber;
            StoreId = storeId;
            Type = type;
        }

        public void SetLastNumber(long lastNumber)
        {
            LastNumber = lastNumber;
        }
    }
}