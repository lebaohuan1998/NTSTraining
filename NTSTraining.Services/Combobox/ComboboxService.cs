using NTSTraining.Models.Entities;
using NTSTraining.Models.Models.Combobox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.Combobox
{
    public class ComboboxService: IComboboxService
    {
        private readonly NTSTrainingContext sqlContext;

        public ComboboxService(NTSTrainingContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }
    }
}
