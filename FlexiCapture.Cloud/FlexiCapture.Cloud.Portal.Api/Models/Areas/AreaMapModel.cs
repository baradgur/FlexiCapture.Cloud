using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khingal.Models.Areas
{
    public class AreaMapModel
    {
        /// <summary>
        /// Id зоны
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя зоны
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// отображение зоны
        /// </summary>
        public bool AreaShow { get; set; }

        /// <summary>
        /// роль пользователя
        /// </summary>
        public int UserRoleId { get; set; }
    }
}