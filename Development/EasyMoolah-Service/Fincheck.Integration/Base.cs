﻿using EasyMoolah.Model.Database.Fincheck;
using EasyMoolah.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fincheck.Integration
{
    public class Base
    {
        public void AddApiLog(Apilog apiLog)
        {
            FinCheckRepository fincheckRepository = new FinCheckRepository();
            fincheckRepository.AddApiLog(apiLog);
        }        
    }
}
