using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Web.Models.Comments
{
    public class LoadCommentsInputModel
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int PostId { get; set; }
    }
}
