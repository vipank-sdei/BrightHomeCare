using BrightCare.Entity.Agency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Repository.Interface.Agency.MasterServiceTypes
{
    public interface IMasterServiceTypeRepository : IRepositoryBase<MasterServiceType>
    {
        MasterServiceType AddMasterServiceType(MasterServiceType masterServiceType);
    }
}
