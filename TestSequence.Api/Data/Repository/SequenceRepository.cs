using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TestSequence.Api.Model;
using TestSequence.Api.Model.Gateway;

namespace TestSequence.Api.Data.Repository
{
    public class SequenceRepository : ISequenceRepository
    {
        private readonly CreditsContext _creditsContext;

        private static readonly object _syncLock = new object();

        public SequenceRepository(CreditsContext creditsContext)
        {
            _creditsContext = creditsContext;
        }

        public long GetNextSequenceEF(string storeId, string type)
        {
            lock (_syncLock)
            {
                var currentSequence =
                    _creditsContext.Set<Sequence>().Where(s => s.StoreId == storeId && s.Type == type).SingleOrDefault()
                    ??
                    new Sequence(Guid.NewGuid(), 0, storeId, type);

                currentSequence.SetLastNumber(currentSequence.LastNumber + 1);

                if (currentSequence.LastNumber == 1)
                {
                    _creditsContext.Add(currentSequence);
                }
                else
                {
                    _creditsContext.Update(currentSequence);
                }

                _creditsContext.SaveChanges();

                return currentSequence.LastNumber;
            }
        }

        public long GetNextSequenceSQL(string storeId, string type)
        {
            var connection = _creditsContext.Database.GetDbConnection();
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "DECLARE @LastNumber BIGINT; " +
                    "SET @LastNumber = ISNULL((SELECT TOP 1 LastNumber FROM Sequences WHERE StoreId = @StoreId AND Type = @Type), 0) + 1; " +
                    "IF(@LastNumber = 1) " +
                    "   BEGIN " +
                    "       INSERT INTO Sequences (Id, LastNumber, StoreId, Type) " +
                    "       VALUES (NEWID(), @LastNumber, @StoreId, @Type) " +
                    "   END " +
                    "ELSE " +
                    "   BEGIN " +
                    "       UPDATE Sequences SET LastNumber = @LastNumber WHERE StoreId = @StoreId AND Type = @Type; " +
                    "   END " +
                    "SELECT @LastNumber ";

                var storeIdParameter = command.CreateParameter();
                storeIdParameter.ParameterName = "StoreId";
                storeIdParameter.Value = storeId;
                command.Parameters.Add(storeIdParameter);

                var typeParameter = command.CreateParameter();
                typeParameter.ParameterName = "Type";
                typeParameter.Value = type;
                command.Parameters.Add(typeParameter);

                lock (_syncLock)
                {
                    long result = (long)command.ExecuteScalar();
                    return result;
                }
            }
        }
    }
}