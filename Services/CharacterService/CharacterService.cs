using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public CharacterService(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _context= context;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacters(AddCharacterDto newCharacter)
        {   
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            
            _context.Character.Add(character);
            await _context.SaveChangesAsync();
            ServiceResponse.Data =await  _context.Character.Select( c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return ServiceResponse;
        }

        

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Character.ToListAsync();
            ServiceResponse.Data = dbCharacters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int Id)
        {   
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Character.FirstOrDefaultAsync(c=>c.Id==Id);
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return ServiceResponse;
           
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacters(UpdateCharacterDto updatedCharacter)
        {   
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            try{ 
            var character = await _context.Character.FirstOrDefaultAsync(c=>c.Id == updatedCharacter.Id);
            if(character is null)
            throw new Exception($"Character with Id'{updatedCharacter.Id} not found");

            _mapper.Map(updatedCharacter,character);

            character.Name = updatedCharacter.Name;
            character.Hitpoints = updatedCharacter.Hitpoints;
            character.Strength = updatedCharacter.Strength;
            character.Defence = updatedCharacter.Defence;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            await _context.SaveChangesAsync();
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex){

                ServiceResponse.Success=false;
                ServiceResponse.Message=ex.Message;

            }
            return ServiceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacters(int Id)
        {
             var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try{ 
            var character = await _context.Character.FirstOrDefaultAsync(c=>c.Id == Id);
            if(character is null)
            throw new Exception($"Character with Id'{Id} not found");

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();


        
            ServiceResponse.Data = await _context.Character.Select( c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch(Exception ex){

                ServiceResponse.Success=false;
                ServiceResponse.Message=ex.Message;

            }
            return ServiceResponse;
        }    
        
    }
}