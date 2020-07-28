using BrightCare.Entity.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrightCare.Repository.Interface.Agency.MasterService
{
    public interface IMasterServicesRepository: IRepositoryBase<MasterServices>
    {            
        MasterServices AddMasterService(MasterServices masterServiceCode);       
    }
}
