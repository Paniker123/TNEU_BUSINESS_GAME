using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Common.DTO.GameDTO
{
     public class GameParamse
     {
         /// <summary>
         /// order by ascending/descending
         /// </summary>
        public OrderBy Asc { get; set; } = OrderBy.Descending;
         /// <summary>
         /// order by sorted type
         /// </summary>
        public GameSortType SorteType { get; set; } = GameSortType.PlayDate;
         /// <summary>
         /// order by name of game
         /// </summary>
        public string Name { get; set; } = string.Empty;
         /// <summary>
         /// order by player name
         /// </summary>
        public string WinnerPlayer { get; set; } = string.Empty;
         /// <summary>
         /// skip for item to start search range
         /// </summary>
        public int Skip { get; set; } = 0;
         /// <summary>
         /// show items whith this range
         /// </summary>
         [Range(1, int.MaxValue)]
         public int Range { get; set; } = 10;
    }
}
