
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("getallCharacters")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok( await _characterService.GetAllCharacters());
        }

        [HttpGet("GetCharacterById")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int Id)
        {
            return Ok(await _characterService.GetCharacterById(Id));
        }

        [HttpPost("AddCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter){
                   
            return Ok(await _characterService.AddCharacters(newCharacter));

        }
        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<ServiceResponse<UpdateCharacterDto>>> UpdatedCharacter(UpdateCharacterDto updatedCharacterDto){
            var response = await _characterService.UpdateCharacters(updatedCharacterDto);
            if(response.Data is null){
                return NotFound(response);
            }       
            return Ok(response);

        }
        [HttpDelete("DeleteCharacterById")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter (int Id)
        {
            var response = await _characterService.DeleteCharacters(Id);
            if(response.Data is null){
                return NotFound(response);
            }       
            return Ok(response);
        }

    }
}