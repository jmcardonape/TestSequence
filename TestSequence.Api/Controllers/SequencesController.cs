using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestSequence.Api.Model;
using TestSequence.Api.Model.Gateway;

namespace TestSequence.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SequencesController : ControllerBase
    {
        private readonly ISequenceRepository _sequenceRepository;
        private readonly ICreditRepository _creditRepository;

        public SequencesController(ISequenceRepository sequenceRepository,
            ICreditRepository creditRepository)
        {
            _sequenceRepository = sequenceRepository;
            _creditRepository = creditRepository;
        }

        [HttpGet]
        public IActionResult SequencesEF()
        {
            DateTime startDate = DateTime.Now;

            var result = CreateCreditsEF();

            DateTime endDate = DateTime.Now;

            TimeSpan time = endDate - startDate;

            return Ok(new
            {
                time = time.TotalSeconds,
                result
            });
        }

        public IActionResult SequencesSQL()
        {
            DateTime startDate = DateTime.Now;

            var result = CreateCreditsSQL();

            DateTime endDate = DateTime.Now;

            TimeSpan time = endDate - startDate;

            return Ok(new
            {
                time = time.TotalSeconds,
                result
            });
        }

        private Dictionary<int, string> CreateCreditsEF()
        {
            var returnDictionary = new Dictionary<int, string>();
            string storeId = "123456";
            string type = "Credit";
            for (int i = 0; i < 3000; i++)
            {
                try
                {
                    long creditNumber = _sequenceRepository.GetNextSequenceEF(storeId, type);
                    var credit = new Credit(Guid.NewGuid(), storeId, creditNumber);
                    _creditRepository.Add(credit);
                }
                catch (Exception ex)
                {
                    returnDictionary.Add(i, ex.ToString());
                }
            }

            return returnDictionary;
        }

        private Dictionary<int, string> CreateCreditsSQL()
        {
            var returnDictionary = new Dictionary<int, string>();
            string storeId = "654321";
            string type = "Credit";
            for (int i = 0; i < 3000; i++)
            {
                try
                {
                    long creditNumber = _sequenceRepository.GetNextSequenceSQL(storeId, type);
                    var credit = new Credit(Guid.NewGuid(), storeId, creditNumber);
                    _creditRepository.Add(credit);
                }
                catch (Exception ex)
                {
                    returnDictionary.Add(i, ex.ToString());
                }
            }

            return returnDictionary;
        }
    }
}