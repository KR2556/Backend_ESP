using ESP.Context;
using ESP.Models;
using ESP.Repository;
using ESP.Request;
using ESP.Response;
using Microsoft.EntityFrameworkCore;

namespace ESP.Services
{
    public class CheckService
    {

        const int FAILED = 0;
        private IRepository<CheckBlock> _checkBlockRepository { get; set; }
        private IRepository<CheckCode> _checkCodeRepository { get; set; }
        private IRepository<SubjectType> _subjectTypeRepository { get; set; }

        private IRepository<Block> _blockRepository { get; set; }
        private IRepository<ClientType> _clientTypeRepository { get; set; }
        private IRepository<SystemType> _systemTypeRepository { get; set; }
        private IRepository<SystemBlock> _systemBlockRepository { get; set; }
        private IRepository<Models.Route> _routeRepository { get; set; }


        private ApplicationContext _context = null!;

        public CheckService(ApplicationContext appContext)
        {
            _context = appContext;
            _checkBlockRepository = new CheckBlockRepository(appContext);
            _checkCodeRepository = new CheckCodeRepository(appContext);
            _subjectTypeRepository = new SubjectTypeRepository(appContext);
            _blockRepository = new BlockRepository(appContext);
            _clientTypeRepository = new ClientTypeRepository(appContext);
            _systemTypeRepository = new SystemTypeRepository(appContext);
            _systemBlockRepository = new SystemBlockRepository(appContext);
            _routeRepository = new RouteRepository(appContext);

        }

        public HttpCheckBlockResponse AddCheckBlock(HttpCheckBlockRequest httpCheckBlockRequest)
        {
            var checkBlockToAdd = new CheckBlock();
            checkBlockToAdd.ShortName = httpCheckBlockRequest.ShortName;
            checkBlockToAdd.Block = _blockRepository.GetById(httpCheckBlockRequest.BlockId);
            var subjects = _subjectTypeRepository.GetAll().Where(x => httpCheckBlockRequest.Subjects.Contains(x.Name)).ToList();

            if(subjects.Count != 0)
            {
                checkBlockToAdd.SubjectTypes.AddRange(subjects);
            }

            var checkCodes = _checkCodeRepository.GetAll().Where(x => httpCheckBlockRequest.CheckCodeIds.Contains(x.Id)).ToList();

            if(checkCodes.Count != 0)
            {
                checkBlockToAdd.CheckCodes.AddRange(checkCodes);
            }

            var result = _checkBlockRepository.Add(checkBlockToAdd);

            if (result == FAILED)
            {
                throw new Exception("Не удалось добавить блок проверки");
            }

            return new HttpCheckBlockResponse() 
            {
                Message = "Блок проверки успешно добавлен" 
            };
        }

        public HttpCheckBlockResponse GetCheckBlockById(int id) 
        {
            var checkBlock = _checkBlockRepository.GetById(id);
            return new HttpCheckBlockResponse()
            {
                Body = new {
                    Id = checkBlock.Id,
                    Block = checkBlock.BlockId,
                    ShortName = checkBlock?.ShortName,
                    CheckCodes = checkBlock?.CheckCodes
                    .Select(x => new {
                        id = x.Id,
                        name = x.Name,
                        isActive = x.IsActive,
                        prohibitionCodes = x.ProhibitionCodes
                    }),
                    Subjects = checkBlock?.SubjectTypes.Select(x => x.Name)
                }
            };
        }

        public HttpCheckBlockResponse GetCheckBlocks() 
        {
             
            var result = _checkBlockRepository.GetAll().Include(x => x.Block).Include(x => x.SubjectTypes)
                                              .Select(x => new
                                              {
                                                  Id = x.Id,
                                                  BlockName = x.Block != null ? x.Block.Name : String.Empty,
                                                  ShortName = x.ShortName,
                                                  Subjects = x.SubjectTypes,
                                                  CheckCodes = x.CheckCodes.Select(code => new 
                                                  {
                                                      Id = code.Id,
                                                      isActive = code.IsActive,
                                                      Name = code.Name
                                                  })
                                              }).ToList();

            if(result.Count() == FAILED) 
            {
                throw new Exception("Не удалось загрузить блоки проверок");
            }

            return new HttpCheckBlockResponse() 
            {
                Body = result
            };
        }

        public HttpCheckBlockResponse UpdateCheckBlock(HttpCheckBlockRequest request) 
        {
            
            var checkBlockToUpdate = _checkBlockRepository.GetById(request.Id);
            checkBlockToUpdate.ShortName = request.ShortName;
            
            if(checkBlockToUpdate.BlockId != request.BlockId)
            {
                checkBlockToUpdate.Block = _blockRepository.GetById(request.BlockId);
            }

            

            if(request.Subjects.Count > 0)
            {
                var checkBlockSubjects = checkBlockToUpdate.SubjectTypes.Select(x => x.Name);

                var newSubjects = request.Subjects.Where(x => !checkBlockSubjects.Contains(x));

                var subjectsToAdd = _subjectTypeRepository.GetAll().Where(subject => newSubjects.Contains(subject.Name));

                checkBlockToUpdate.SubjectTypes.AddRange(subjectsToAdd);
                checkBlockToUpdate.SubjectTypes.RemoveAll(x => !request.Subjects.Contains(x.Name));
            }
            
            if(request.CheckCodeIds.Count != 0)
            {
                var checkBlockCodes = checkBlockToUpdate.CheckCodes.Select(x => x.Id);
                
                var newCodes = request.CheckCodeIds.Where(code => !checkBlockCodes.Contains(code));
               
                var checkCodesToAdd = _checkCodeRepository.GetAll().Where(checkCode => newCodes.Contains(checkCode.Id));
                
                checkBlockToUpdate.CheckCodes.AddRange(checkCodesToAdd);
                
                checkBlockToUpdate.CheckCodes.RemoveAll(x => !request.CheckCodeIds.Contains(x.Id));
            }
            else
            {
                checkBlockToUpdate.CheckCodes.Clear();
            }


            var result = _checkBlockRepository.Update(checkBlockToUpdate);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось обновить блок проверки");
            }

            return new HttpCheckBlockResponse() 
            {
                Message = "Блок проверки обновлен" 
            };
        }

        public HttpCheckBlockResponse DeleteCheckBlock(int id) 
        {

            var result = _checkBlockRepository.Delete(id);

            if (result == FAILED)
            {
                throw new Exception("Не удалось удалить блок проверки");
            }

            return new HttpCheckBlockResponse()
            {
                Message = "Блок проверки удален"
            };
        }

        public HttpBlockResponse AddBlock(Block block) 
        {
            var result = _blockRepository.Add(block);

            if (result == FAILED)
            {
                throw new Exception("Не удалось добавить блок");
            }

            return new HttpBlockResponse()
            {
                Message = "Блок успешно добавлен"
            };
        }

        public HttpBlockResponse GetBlockId(int id)
        {
            var result = _blockRepository.GetById(id);
            return new HttpBlockResponse()
            {
                Body = result
            };
        }

        public HttpBlockResponse GetBlocks() 
        {
            var result = _blockRepository.GetAll();

            if (result.Count() == FAILED)
            {
                throw new Exception("Не удалось загрузить блоки");
            }

            return new HttpBlockResponse()
            {
                Body = result
            };
        }

        public HttpBlockResponse UpdateBlock(HttpBlockRequest httpBlockUpdateRequest) 
        {
            var blockToUpdate = new Block()
            {
                Id = httpBlockUpdateRequest.Id,
                Name = httpBlockUpdateRequest.Name
            };
            var result = _blockRepository.Update(blockToUpdate);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось обновить блок");
            }
            return new HttpBlockResponse()
            {
                Message = "Блок обновлен"
            };
        }

        public HttpBlockResponse DeleteBlock(int id) 
        {
            var result = _blockRepository.Delete(id);

            if (result == FAILED)
            {
                throw new Exception("Не удалось удалить блок");
            }

            return new HttpBlockResponse()
            {
                Message = "Блок удален"
            };
        }

        public HttpCheckCodeResponse AddCheckCode(HttpCheckCodeRequest httpCheckCodeRequest) 
        {
            var checkCodeToAdd = new CheckCode()
            {
                Name = httpCheckCodeRequest.Name,
                Title = httpCheckCodeRequest.Title,
                IsActive = httpCheckCodeRequest.IsActive
            };

            var result = _checkCodeRepository.Add(checkCodeToAdd);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось добавить код проверки");
            }

            return new HttpCheckCodeResponse()
            {
                Message = "Код проверки успешно добавлен"
            };
        }

        public HttpCheckCodeResponse GetCheckCodeById(int id) 
        {
            var result = _checkCodeRepository.GetById(id);

            return new HttpCheckCodeResponse()
            {
                Body = result
            };
        }

        public HttpCheckCodeResponse GetCheckCodes(bool isOnlyFreeCheckCodes) 
        {
            
            var result = _checkCodeRepository.GetAll();
            
            if(isOnlyFreeCheckCodes)
            {
                result = result.Include(x => x.CheckBlocks).Where(x => x.CheckBlocks.Count == 0);
            }

            if (result.Count() == FAILED)
            {
                throw new Exception("Не удалось загрузить коды проверок");
            }

            return new HttpCheckCodeResponse()
            {
                Body = result.ToList()
            };
        }

        public HttpCheckCodeResponse UpdateCheckCode(HttpCheckCodeRequest httpUpdateCheckCodeRequest) 
        {
            var checkCodeToUpdate = _checkCodeRepository.GetById(httpUpdateCheckCodeRequest.Id);
            checkCodeToUpdate.Title = httpUpdateCheckCodeRequest.Title;
            checkCodeToUpdate.Name = httpUpdateCheckCodeRequest.Name;
            checkCodeToUpdate.IsActive = httpUpdateCheckCodeRequest.IsActive;

            var result = _checkCodeRepository.Update(checkCodeToUpdate);
            
            if(result == FAILED) 
            {
               throw new Exception("Не удалось обновить код проверки");
            }

            return new HttpCheckCodeResponse()
            {
                 Message = "Код проверки успешно обновлен"
            };
        }

        public HttpCheckCodeResponse DeleteCheckCode(int id) 
        {
            var result = _checkCodeRepository.Delete(id);

            if (result == FAILED) 
            {
                throw new Exception("Не удалось удалить код проверки");
            }

            return new HttpCheckCodeResponse()
            {
                Message = "Код проверки успешно удален"
            };
        }

        public HttpSubjectTypeResponse AddSubjectType(HttpSubjectTypeRequest request) 
        {
            var subjectTypeToAdd = new SubjectType()
            {
                Name = request.Name.ToUpperInvariant()
            };

            var result = _subjectTypeRepository.Add(subjectTypeToAdd);

            if (result == FAILED)
            {
                throw new Exception("Не удалось добавить тип субъекта");
            }

            return new HttpSubjectTypeResponse()
            {
                Message = "Тип субъекта успешно добавлен"
            };
        }

        public HttpSubjectTypeResponse GetSubjectTypeById(int id) 
        {
            var result = _subjectTypeRepository.GetById(id);

            return new HttpSubjectTypeResponse()
            {
                Body = result
            };
        }

        public HttpSubjectTypeResponse GetSubjectTypes() 
        {
            var result = _subjectTypeRepository.GetAll().ToList();


            if (result.Count() == FAILED)
            {
                throw new Exception("Не удалось загрузить типы субъектов");
            }

            return new HttpSubjectTypeResponse()
            {
                Body = result
            };
        }

        public HttpSubjectTypeResponse UpdateSubjectType(HttpSubjectTypeRequest request) 
        {
            
            var subjectToUpdate = _subjectTypeRepository.GetById(request.Id);
            subjectToUpdate.Name = request.Name;

            var result = _subjectTypeRepository.Update(subjectToUpdate);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось обновить тип субъекта");
            }

            return new HttpSubjectTypeResponse()
            {
                Message = "Тип субъекта успешно обновлен"
            };
        }

        public HttpSubjectTypeResponse DeleteSubjectType(int id) 
        {
            var result = _subjectTypeRepository.Delete(id);

            if(result == FAILED) 
            {
                throw new Exception("Не удалось удалить тип субъекта");
            }

            return new HttpSubjectTypeResponse()
            {
                Message = "Тип субъекта успешно удален"
            };
        }
        public HttpProhibitionCodeResponse AddProhibitionCode(HttpProhibitonCodeRequest request)
        {
            var checkCode = _checkCodeRepository.GetById(request.CheckCodeId);
            var prohibitionCodeToAdd = new ProhibitionCode
            {
                Name = request.Name,
                IsActive = request.IsActive,
                StartDate = DateTime.Parse(request.StartDate),
                EndDate = request.EndDate != null ? DateTime.Parse(request.EndDate) : null,
                CheckCode = checkCode,
            };
            _context.ProhibitionCodes.Add(prohibitionCodeToAdd);

            var result = _context.SaveChanges();
            
            if(result == 0)
            {
                throw new Exception("Не удалось добавить код запрета");
            }

            return new HttpProhibitionCodeResponse()
            {
                Body = prohibitionCodeToAdd,
                Message = "Код запрета успешно добавлен"
            };
        }

        public HttpProhibitionCodeResponse UpdateProhibitionCode(HttpProhibitonCodeRequest request)
        {
            var prohibitionCodeToUpdate = _context.ProhibitionCodes.Find(request.Id);
            
            if(prohibitionCodeToUpdate == null)
            {
                return new HttpProhibitionCodeResponse();
            }

            prohibitionCodeToUpdate.Name = request.Name;
            prohibitionCodeToUpdate.IsActive = request.IsActive;
            prohibitionCodeToUpdate.StartDate = DateTime.Parse(request.StartDate);
            prohibitionCodeToUpdate.EndDate =  request.EndDate != null ? DateTime.Parse(request.EndDate) : null;

            var result = _context.SaveChanges();

            if (result == 0)
            {
                throw new Exception("Не удалось обновить код запрета");
            }

            return new HttpProhibitionCodeResponse()
            {
                Body= prohibitionCodeToUpdate,
                Message = "Код запрета успешно обновлен"
            };
        }

        public HttpProhibitionCodeResponse DeleteProhibitionCode(int id)
        {
            var prohibitionCodeToDelete = _context.ProhibitionCodes.FirstOrDefault(x => x.Id == id);

            if(prohibitionCodeToDelete != null)
            {
                _context.ProhibitionCodes.Remove(prohibitionCodeToDelete);
            }

            var result = _context.SaveChanges();

            return new HttpProhibitionCodeResponse()
            {
                Message = "Код запрета успешно удален"
            };
        }

        public HttpProhibitionCodeResponse GetProhibitionCodes(string? filter)
        {
            var prohibitionCode = new List<ProhibitionCode>();

            if(!string.IsNullOrEmpty(filter))
            {
                prohibitionCode = _context.ProhibitionCodes.Include(x => x.CheckCode).Where(x => x.Name == filter).ToList();
            }
            else
            {
                prohibitionCode = _context.ProhibitionCodes.Include(x => x.CheckCode).ToList();
            }
            
            if(prohibitionCode.Count == 0)
            {
                throw new Exception("Нет удалось загрузить коды запрета");
            }

            return new HttpProhibitionCodeResponse
            {
                Body = prohibitionCode.OrderBy(x => x.Name)
            };
        }

        public HttpClientTypeResponse GetClientTypeById(int id)
        {
            var response = _clientTypeRepository.GetById(id);

            return new HttpClientTypeResponse()
            {
                Body = response
            };
        }

        public HttpClientTypeResponse GetClientTypes() 
        {
            var response = _clientTypeRepository.GetAll().Include(x => x.SubjectTypes).ToList();

            if(response.Count == 0)
            {
                throw new Exception("Не удалось извлечь тип клиента процесса");
            }


            return new HttpClientTypeResponse
            {
                Body = response
            };
        }
       
        public HttpClientTypeResponse SaveClientType(HttpClientTypeRequest request) 
        {
            var clientType = _clientTypeRepository.GetById(request.Id);
            clientType.Code = request.Code;
            clientType.Name = request.Name;

            if (request.ClientTypeIds.Count != 0)
            {
                clientType.SubjectTypes.AddRange(_context.SubjectTypes.Where(x => request.ClientTypeIds.Contains(x.Id)));
                clientType.SubjectTypes.RemoveAll(x => !request.ClientTypeIds.Contains(x.Id));
            }
            else
            {
                clientType.SubjectTypes.Clear();
            }

            var state = _context.Attach(clientType).State;

            if (state == EntityState.Added) 
            {
                _clientTypeRepository.Add(clientType);                
            }

            if(state == EntityState.Unchanged) 
            {
               _clientTypeRepository.Update(clientType);
            }

            return new HttpClientTypeResponse()
            {
                Body = clientType,
                Message = "Тип клиента успешно сохранен"
            };
        }

        public HttpClientTypeResponse DeleteClientType(int id)
        {
            var response = _clientTypeRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить тип клиента");
            }

            return new HttpClientTypeResponse()
            {
                Body = response
            };
        }


        public HttpSystemTypeResponse GetSystemTypeById(int id)
        {
            var response = _systemTypeRepository.GetById(id);

            return new HttpSystemTypeResponse()
            {
                Body = response
            };
        }

        public HttpSystemTypeResponse GetSystemTypes()
        {
            var response = _systemTypeRepository.GetAll().ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь тип системы");
            }


            return new HttpSystemTypeResponse
            {
                Body = response
            };
        }

        public HttpSystemTypeResponse SaveSystemType(HttpSystemTypeRequest request)
        {
            var systemType = _systemTypeRepository.GetById(request.Id);
            systemType.Code = request.Code;
            systemType.Name = request.Name;

            var state = _context.Attach(systemType).State;

            if (state == EntityState.Added)
            {
                _systemTypeRepository.Add(systemType);
            }

            if (state == EntityState.Unchanged)
            {
                _systemTypeRepository.Update(systemType);
            }

            return new HttpSystemTypeResponse
            {
                Body = systemType,
                Message = "Тип системы успешно сохранен"
            };
        }

        public HttpSystemTypeResponse DeleteSystemType(int id)
        {
            var response = _systemTypeRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить тип систеы процесса");
            }

            return new HttpSystemTypeResponse
            {
                Body = response
            };
        }


        public HttpSystemBlockResponse GetSystemBlockById(int id)
        {
            var response = _systemBlockRepository.GetById(id);

            return new HttpSystemBlockResponse()
            {
                Body = response
            };
        }

        public HttpSystemBlockResponse GetSystemBlocks()
        {
            var response = _systemBlockRepository.GetAll().ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь блок системы");
            }


            return new HttpSystemBlockResponse
            {
                Body = response
            };
        }

        public HttpSystemBlockResponse SaveSystemBlock(HttpSystemBlockRequest request)
        {
            var systemBlock = _systemBlockRepository.GetById(request.Id);
            systemBlock.Code = request.Code;
            systemBlock.Name = request.Name;

            var state = _context.Attach(systemBlock).State;

            if (state == EntityState.Added)
            {
                _systemBlockRepository.Add(systemBlock);
            }

            if (state == EntityState.Unchanged)
            {
                _systemBlockRepository.Update(systemBlock);
            }

            return new HttpSystemBlockResponse
            {
                Body = systemBlock,
                Message = "Блок системы успешно сохранен"
            };
        }

        public HttpSystemBlockResponse DeleteSystemBlock(int id)
        {
            var response = _systemBlockRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить блок системы");
            }

            return new HttpSystemBlockResponse
            {
                Body = response
            };
        }

        public HttpRouteResponse GetRouteById(int id)
        {
            var response = _routeRepository.GetById(id);

            return new HttpRouteResponse()
            {
                Body = response
            };
        }

        public HttpRouteResponse GetRoutes()
        {
            var response = _routeRepository.GetAll().Select(x => new
            {

                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ProhibitionCodes = x.ProhibitionCodes.Select(x => x.Name),
                CheckCodes = x.CheckCodes.Select(x => x.Name)

            }).ToList();

            if (response.Count == 0)
            {
                throw new Exception("Не удалось извлечь маршрут");
            }


            return new HttpRouteResponse
            {
                Body = response
            };
        }

        public HttpRouteResponse SaveRoute(HttpRouteRequest request)
        {
            var route = _routeRepository.GetById(request.Id);
            route.Code = request.Code;
            route.Name = request.Name;
   

            var state = _context.Attach(route).State;

            if(request.ProhibitionCodeIds.Count !=0 )
            {
                route.ProhibitionCodes.AddRange(_context.ProhibitionCodes.Where(x => request.ProhibitionCodeIds.Contains(x.Id)));
                route.ProhibitionCodes.RemoveAll(x => !request.ProhibitionCodeIds.Contains(x.Id));
            }
            else
            {
                route.ProhibitionCodes.Clear();
            }

            if(request.CheckCodeIds.Count != 0) 
            {
                route.CheckCodes.AddRange(_context.CheckCodes.Where(x => request.CheckCodeIds.Contains(x.Id)));
                route.CheckCodes.RemoveAll(x => !request.CheckCodeIds.Contains(x.Id));
            }
            else
            {
                route.CheckCodes.Clear();
            }

            if (state == EntityState.Added)
            {
                _routeRepository.Add(route);
            }

            if (state == EntityState.Unchanged)
            {
                _routeRepository.Update(route);
            }

            return new HttpRouteResponse
            {
                Body = route,
                Message = "Маршрут успешно сохранен"
            };
        }

        public HttpRouteResponse DeleteRoute(int id)
        {
            var response = _routeRepository.Delete(id);

            if (response == 0)
            {
                throw new Exception("Не удалось удалить маршрут");
            }

            return new HttpRouteResponse
            {
                Body = response
            };
        }

    }
}
