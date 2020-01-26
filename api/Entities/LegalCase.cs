using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class LegalCase
    {
        public long Id { get; set; }
        public long CaseId { get; set; }
        public long FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
