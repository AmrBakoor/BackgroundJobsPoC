using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PoC.BL.AbstractProducts;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.Dtos.Dtos;
using PoC.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.BL.ConcreteProducts
{
    public class CalculationJobProcessor : IJobProcessor
    {
        private readonly IPoCUnitOfWork _poCUnitOfWork;
        private readonly INumberRangeGenerator _numberRangeGenerator;
        private readonly INumberValidator _numberValidator;

        public CalculationJobProcessor(IPoCUnitOfWork poCUnitOfWork, INumberRangeGenerator numberRangeGenerator, INumberValidator numberValidator)
        {
            _poCUnitOfWork = poCUnitOfWork;
            _numberRangeGenerator = numberRangeGenerator;
            _numberValidator = numberValidator;
        }
        public async Task<bool> ExecuteAsync(Guid batchJobId)
        {          
            var jobRepository = _poCUnitOfWork.JobRepository;
            var job = await jobRepository.GetByJobIdAsync(batchJobId);
            var serializerSettings = new JsonSerializerSettings();
            var hasErrors = false;
            if (job != null)
            {
                try
                {
                    var rand = new Random();
                    var forceToFail = rand.NextDouble() >= 0.5;
                    if (forceToFail)
                    {
                        job.StatusEnum = JobStatusEnum.FAILED;
                        job.CompletedAt = DateTime.UtcNow;
                        job.Result = JsonConvert.SerializeObject(new List<JobResultDto>() { new JobResultDto() { Number = 0, Message = "I was told to fail.I'm not to blame" } }, serializerSettings);
                        await _poCUnitOfWork.SaveChangesAsync();
                        hasErrors = true;
                        return hasErrors;
                    }
                    var arrayOfOrderedNumbers = _numberRangeGenerator.GenerateArrayOfNumbers(1, 100);
                    List<JobResultDto> jobResult = new();

                    for (int i = 0; i < arrayOfOrderedNumbers.Length; i++)
                    {
                        var numberState = _numberValidator.Validate(arrayOfOrderedNumbers[i]);

                        if (numberState.DivisibleByThree && !numberState.DivisibleByFive)
                            jobResult.Add(new JobResultDto() { Number = arrayOfOrderedNumbers[i], Message = job.FirstName });

                        if (numberState.DivisibleByFive && !numberState.DivisibleByThree)
                            jobResult.Add(new JobResultDto() { Number = arrayOfOrderedNumbers[i], Message = job.LastName });

                        if (NumberState.IsDivisibleByThreeAndFive(numberState))
                            jobResult.Add(new JobResultDto() { Number = arrayOfOrderedNumbers[i], Message = job.FirstName + " " + job.LastName });

                        if (NumberState.IsNeutral(numberState))
                            jobResult.Add(new JobResultDto() { Number = arrayOfOrderedNumbers[i], Message = arrayOfOrderedNumbers[i].ToString() });

                        if (i % 10 == 0)
                        {
                            job.Progress += 10;
                            await _poCUnitOfWork.SaveChangesAsync();
                        }
                       
                        // Laat me een beetje te salpen of krijgt voor me een lekker kopje koffie :)
                       await Task.Delay(200);
                    }

                    job.StatusEnum = JobStatusEnum.SUCCESS;
                    job.CompletedAt = DateTime.UtcNow;
                    job.Result = JsonConvert.SerializeObject(jobResult, serializerSettings);
                }
                catch (Exception ex)
                {
                    job.StatusEnum = JobStatusEnum.FAILED;
                    job.CompletedAt = DateTime.UtcNow;
                    job.Result = JsonConvert.SerializeObject(ex, serializerSettings);
                    hasErrors = true;
                }

                await _poCUnitOfWork.SaveChangesAsync();
            }
            return hasErrors;
        }


        #region Private Behvaiour
       
        #endregion
    }
}
