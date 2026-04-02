using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.dto;


namespace CloudSpritzers1.src.view.faq
{

    public class FAQNavigationData
    {

        public int CurrentPersonId { get; set; }
        public bool IsEmployee { get; set; }
        public FAQEntryDTO? FAQEntry { get; set; }
    }
}
