using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.DTOs
{
    public class GetCharacterDto
    {
        public int Id { get; set; } 
        public string Name { get; set;} = "Frodo";
        public string Strength { get; set;} = "100";
        public string Hitpoints { get; set;} = "10";
        public string Defence { get; set;} = "10";
        public string Intelligence { get; set;} = "10";
        public RpgClass Class { get; set;} = RpgClass.Knight;
        
    }
}