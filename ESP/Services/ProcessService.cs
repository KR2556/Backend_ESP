using ESP.Context;
using ESP.Models;
using ESP.Request;
using ESP.Response;
using Microsoft.EntityFrameworkCore;

namespace ESP.Services
{
    public class ProcessService
    {

        private readonly ApplicationContext _context;

        public ProcessService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<HttpSubjectTypeResponse> GetFilteredSubjectsAsync(int blockId)
        {
            var result = await _context.SubjectTypes.Include(x => x.CheckBlocks.Where(x => x.BlockId == blockId))
                                                    .Where(x => x.CheckBlocks.Any(x => x.BlockId == blockId))
                                                    .ToListAsync();

            if(result.Count == 0)
            {
                throw new Exception("Не удалось загрузить субъекты");
            }

            return new HttpSubjectTypeResponse()
            {
                Body = result
            };
        }

        public async Task<HttpProcessResponse> GetProcessesAsync() 
        {

            var response = await _context.Processes
                                         .Include(x => x.CheckBlocks)
                                         .Include(x => x.CheckCodes)
                                         .Include(x => x.SubjectTypes)
                                         .Include(x => x.ProhibitionCodes).ToListAsync();

            
            if (response.Count == 0) throw new Exception("Не удлаось излвечь процессы");

            return new HttpProcessResponse()
            {
                Body = response
            };

        }
        public async Task<HttpCheckResponse> GetChecksAsync(HttpCheckRequest request) 
        {
            List<CheckBlock> checks = new List<CheckBlock>();

            var checkBlocks = _context.CheckBlocks
                                       .Include(x => x.Block)
                                       .Include(x => x.SubjectTypes.Where(x => request.Subjects.Contains(x.Id)));                      
            
            if(request.IsNewClient != null) {
                
                var result = await checkBlocks.Include(x => x.CheckCodes.Where(x => x.IsActive == request.IsNewClient))
                                              .ThenInclude(x => x.ProhibitionCodes)
                                              .ToListAsync();
                
                foreach(var item in result)
                {
                    if(item.Block != null && item.SubjectTypes.Count != 0 && item.CheckCodes.Count !=0)
                    {
                        checks.Add(item);
                    }
                }
            }
            else
            {
                var result = await checkBlocks.Include(x => x.CheckCodes)
                                              .ThenInclude(x => x.ProhibitionCodes)
                                              .ToListAsync();

                foreach (var item in result)
                {
                    if (item.SubjectTypes.Count != 0 && item.CheckCodes.Count > 0  && item.CheckCodes.Count != 1)
                    {
                        checks.Add(item);
                    }
                }
            }
          
          
            if(checks.Count == 0)
            {
                throw new Exception("Не удалось извлечь проверки");
            }
           

       
            return new HttpCheckResponse()
            {
                Body = checks
            };
        }
        public async Task<HttpProcessResponse> GetProcessByIdAsync(int id)
        {
            var process = await _context.Processes.Include(x => x.CheckBlocks)
                                                  .Include(x => x.CheckCodes)
                                                  .Include(x => x.SubjectTypes)
                                                  .Include(x => x.ProhibitionCodes).FirstOrDefaultAsync(x => x.Id == id);


            var checkBlocks = process?.CheckBlocks.ConvertAll(x => x.Id);
            var checkCodes = process?.CheckCodes?.ConvertAll(x => x.Id);
            var subjectTypes = process?.SubjectTypes.ConvertAll(x => x.Id);
            var prohibitionCodes = process?.ProhibitionCodes.ConvertAll(x => x.Id);



            if (checkBlocks == null || checkCodes == null || subjectTypes == null || prohibitionCodes == null) 
            {
                return new HttpProcessResponse();
            }

            var checks = _context.CheckBlocks.Include(x => x.Block).Include(x => x.SubjectTypes.Where(x => subjectTypes.Contains(x.Id)))
                                             .Include(x => x.CheckCodes.Where(x => checkCodes.Contains(x.Id)))
                                             .ThenInclude(x => x.ProhibitionCodes)
                                             .Where(x => checkBlocks.Contains(x.Id));
            if(checks.Count() == 0)
            {
                throw new Exception("Не удалось извлечь проверки");
            }

            return new HttpProcessResponse() 
            {
                Body = new { Name = process?.Name, Checks = checks,ProhibitionCodes = prohibitionCodes }
            };
       

        }

        public async Task<HttpProcessResponse> AddProcessAsync(HttpProcessRequest request) 
        {
            
            var processToCreate = new Process()
            {
                Name = request.Name
            };



            processToCreate.CheckBlocks.AddRange(_context.CheckBlocks.Where(x => request.CheckBlockIds.Contains(x.Id)));
            processToCreate.CheckCodes.AddRange(_context.CheckCodes.Where(x => request.CheckCodeIds.Contains(x.Id)));
            processToCreate.SubjectTypes.AddRange(_context.SubjectTypes.Where(x => request.SubjectIds.Contains(x.Id)));
            processToCreate.ProhibitionCodes.AddRange(_context.ProhibitionCodes.Where(x => request.ProhibitionCodeIds.Contains(x.Id)));
            _context.Processes.Add(processToCreate);

            int executed =  await _context.SaveChangesAsync();

            if(executed == 0)
            {
                throw new Exception("Не удалось сохранить процесс");
            }

            return new HttpProcessResponse()
            {
                Message = "Процесс успешно сохранен"
            };
        }

        public async Task<HttpProcessResponse> UpdateProcessAsync(HttpProcessRequest request)
        {
            return await Task.FromResult(new HttpProcessResponse());
        }

        public async Task<HttpProcessResponse> DeleteProcessAsync(int id)
        {

            _context.Processes.Remove(_context.Processes.Find(id) ?? new Process());
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("Не удалось удалить процесс");
            }

            return new HttpProcessResponse()
            {
                Message = "Процесс успешно удален"
            };
        }

      
    }
}
